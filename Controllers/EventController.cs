using AutoMapper;
using eventz.DTOs;
using eventz.Models;
using eventz.Repositories.Interfaces;
using eventz.Utils;
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
        private readonly string baseApiUrl;

        public EventController(IMapper mapper, IEventRepository eventRepository, IWebHostEnvironment environment)
        {
            _mapper = mapper;
            _eventRepository = eventRepository;
            _environment = environment;
            baseApiUrl = Messages.UrlImage;
        }

        [HttpPost]
        [Route("Events")]
        public async Task<ActionResult<Event>> Create([FromForm] EventDtoRequest event_req)
        {
            var uniqueFileName = string.Empty;

            try
            {
                if (event_req.ImageFile != null)
                {
                    var uploadPath = Path.Combine(_environment.WebRootPath ?? "", "uploads");
                    Directory.CreateDirectory(uploadPath);

                    uniqueFileName = $"{Guid.NewGuid().ToString()}_{event_req.ImageFile.FileName}";
                    var filePath = Path.Combine(uploadPath, uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        event_req.ImageFile.CopyTo(stream);
                    }


                }
                // Mapear o DTO para o objeto Event
                Event @event = _mapper.Map<Event>(event_req);
                @event.ImageUrl = $"{baseApiUrl}/{uniqueFileName}";


                await _eventRepository.Create(@event);
                return Ok(event_req);
            }
            catch (Exception ex)
            {
                // Considere logar o erro ex para fins de depuração.
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
        [HttpGet]
        [Route("images/{imageName}")]
        public IActionResult GetImage(string imageName)
        {
            if (_environment.WebRootPath == null)
            {
                Console.WriteLine(_environment.ContentRootPath + $"{_environment.WebRootPath}/out");
                return BadRequest("WebRootPath não configurado corretamente.");
            }

            var imagePath = Path.Combine(_environment.WebRootPath, "uploads", imageName);

            if (System.IO.File.Exists(imagePath))
            {
                var imageBytes = System.IO.File.ReadAllBytes(imagePath);
                return File(imageBytes, "image/jpeg");  // ou "image/png", dependendo do formato da imagem
            }

            return NotFound(); // Ou BadRequest(), dependendo da sua lógica de tratamento de erro
        }



    }
}
