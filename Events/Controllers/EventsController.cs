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
    [Authorize]
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

        /// <summary>
        ///     Get all events
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetEvents()
        {
            var events = await _repositoryManager.Event.GetEventsAsync();

            var eventsDto = _mapper.Map<IEnumerable<EventDto>>(events);

            return Ok(eventsDto);
        }


        /// <summary>
        ///  Get event by guid
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "EventById")]
        public async Task<IActionResult> GetEvent(Guid id)
        {
            var customEvent = await _repositoryManager.Event.GetEventAsync(id);
            if (customEvent == null)
            {
                return NotFound();
            }

            var eventDto = _mapper.Map<EventDto>(customEvent);

            return Ok(eventDto);
        }

        /// <summary>
        /// Create event
        /// </summary>
        /// <param name="customEvent"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateEvent([FromBody] EventCreationDto customEvent)
        {
            if (customEvent == null)
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

        /// <summary>
        /// Update event
        /// </summary>
        /// <param name="id"></param>
        /// <param name="customEvent"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEvent(Guid id,
            [FromBody] EventUpdatingDto customEvent)
        {
            if (customEvent == null)
            {
                return BadRequest("EventForUpdatingDto object is null");
            }

            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }

            var eventEntity = await _repositoryManager.Event.GetEventAsync(id, true);
            if (eventEntity == null)
            {
                return NotFound();
            }

            _mapper.Map(customEvent, eventEntity);
            await _repositoryManager.SaveAsync();

            return NoContent();
        }

        /// <summary>
        /// deelete event by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(Guid id)
        {
            var customEvent = await _repositoryManager.Event.GetEventAsync(id);
            if (customEvent == null)
            {
                return NotFound();
            }

            _repositoryManager.Event.DeleteEvent(customEvent);
            await _repositoryManager.SaveAsync();

            return NoContent();
        }
    }
}
