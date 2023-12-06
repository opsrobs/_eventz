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

        public EventController(IMapper mapper, IEventRepository eventRepository)
        {
            _mapper = mapper;
            _eventRepository = eventRepository;
        }

        [HttpPost]
        [Route("Events")]
        public async Task<ActionResult<Event>> Create([FromBody] EventDtoRequest event_req)
        {
            Event @event = new Event();
            @event = _mapper.Map<Event>(event_req);

            try
            {

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

    }
}
