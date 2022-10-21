using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Events.Controllers
{
    [Route("api/events")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    public class EventsController : ControllerBase
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public EventsController(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        [HttpGet(Name = "GetEvents"), Authorize]
        public async Task<IActionResult> GetEvents()
        {
            var events = await _repositoryManager.Event.GetEventsAsync(trackChanges: false);

            var eventsDto = _mapper.Map<IEnumerable<EventDto>>(events);

            return Ok(eventsDto);
        }

        [HttpGet("{id}", Name = "EventById")]
        public async Task<IActionResult> GetEvent(Guid id)
        {
            var customEvent = await _repositoryManager.Event.GetEventAsync(id, trackChanges: false);
            if(customEvent == null)
            {
                return NotFound();
            }
            else
            {
                var eventDto = _mapper.Map<EventDto>(customEvent);
                return Ok(eventDto);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvent([FromBody]EventCreationDto customEvent)
        {
            if(customEvent == null)
            {
                return BadRequest("EventForCreationDto object is null");
            }

            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }

            var eventEntity = _mapper.Map<Event>(customEvent);

            _repositoryManager.Event.CreateEvent(eventEntity);
            await _repositoryManager.SaveAsync();

            var eventToReturn = _mapper.Map<EventDto>(eventEntity);

            return CreatedAtRoute("EventById", new { id = eventToReturn.Id }, eventToReturn);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEvent(Guid id, 
            [FromBody]EventUpdatingDto customEvent)
        {
            if (customEvent == null)
            {
                return BadRequest("EventForUpdatingDto object is null");
            }

            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }

            var eventEntity = await _repositoryManager.Event.GetEventAsync(id, trackChanges: true);
            if(eventEntity == null)
            {
                return NotFound();
            }

            _mapper.Map(customEvent, eventEntity);
            await _repositoryManager.SaveAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(Guid id)
        {
            var customEvent = await _repositoryManager.Event.GetEventAsync(id, trackChanges: false);
            if(customEvent == null)
            {
                return NotFound();
            }

            _repositoryManager.Event.DeleteEvent(customEvent);
            await _repositoryManager.SaveAsync();

            return NoContent();
        }
    }
}
