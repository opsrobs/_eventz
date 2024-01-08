using AutoMapper;
using eventz.DTOs;
using eventz.Models;
using eventz.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace eventz.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : Controller
    {
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _environment;


        public EventController(IMapper mapper, IEventRepository eventRepository, IWebHostEnvironment environment)
        {
            _mapper = mapper;
            _eventRepository = eventRepository;
            _environment = environment;
        }

        [HttpPost]
        [Route("create")]
        public async Task<ActionResult<Event>> Create([FromForm] EventDtoRequest event_req)
        {
            var uniqueFileName = string.Empty;

            try
            {
                if (event_req.ImageFile != null)
                {
                    // Verificar a extensão do arquivo para garantir que seja uma imagem
                    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                    var extension = Path.GetExtension(event_req.ImageFile.FileName).ToLower();

                    if (!allowedExtensions.Contains(extension))
                    {
                        return BadRequest(new { error = "O arquivo enviado não é uma imagem válida." });
                    }

                    var uploadPath = Path.Combine(_environment.WebRootPath ?? "", "uploads");
                    Directory.CreateDirectory(uploadPath);

                    uniqueFileName = $"{Guid.NewGuid().ToString()}_{event_req.ImageFile.FileName}";
                    var filePath = Path.Combine(uploadPath, uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        event_req.ImageFile.CopyTo(stream);
                    }
                }

                Event @event = _mapper.Map<Event>(event_req);
                @event.ImageUrl = $"/uploads/{uniqueFileName}";

                await _eventRepository.Create(@event);
                return Ok(event_req);
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                return StatusCode(500, new { error = "Internal Server Error." });
            }
        }


        [HttpGet]
        public async Task<ActionResult<List<Event>>> GetAllEvents()
        {
            List<Event> events = await _eventRepository.GetAll();
            //List<UserToDtoList> userDtos = _mapper.Map<List<UserToDtoList>>(users);
            return Ok(events);
        }

        [HttpPost]
        [Route("getAll")]
        public async Task<ActionResult<List<Event>>> GetAll([FromBody] LocalizationDto localization)
        {
            List<EventWithLocalization> eventsWithLocalization = await _eventRepository.GetEventByLocalization(localization);

            // Agora você precisa mapear apenas a propriedade Event da nova classe para obter uma lista de Event
            List<Event> events = eventsWithLocalization.Select(e => e.Event).ToList();

            return Ok(events);
        }


    }
}
