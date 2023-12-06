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
    public class HomeController: Controller
    {
        private readonly IHomeRepository _homeRepository;
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;

        public HomeController(IHomeRepository home, IMapper mapper, IEventRepository eventRepository)
        {
            _homeRepository = home;
            _mapper = mapper;
            _eventRepository = eventRepository;
        }

        [HttpPost]
        [Route("Events")]
        public async Task<ActionResult<Event>> Create([FromBody] EventDtoRequest event_req)
        {
            if (event_req == null)
            {

            }

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

    }
}
