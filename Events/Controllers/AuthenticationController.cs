using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Events.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IAuthenticationManager _authManager;

        public AuthenticationController(IMapper mapper, UserManager<User> userManager, 
            IAuthenticationManager authManager)
        {
            _mapper = mapper;
            _userManager = userManager;
            _authManager = authManager;
        }

        /// <summary>
        /// register
        /// </summary>
        /// <param name="userForRegistration"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto
            userForRegistration)
        {
            if(userForRegistration == null)
            {
                return BadRequest("UserForRegistrationDto object is null");
            }

            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }

            var user = _mapper.Map<User>(userForRegistration);

            var result = await _userManager.CreateAsync(user, userForRegistration.Password);
            if (!result.Succeeded)
            {
                foreach(var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }

                return BadRequest();
            }

            return StatusCode(201);
        }

        /// <summary>
        /// authentication
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<IActionResult> Authenticate([FromBody] UserForAuthenticationDto user)
        {
            if (user == null)
            {
                return BadRequest("UserForAuthDto object is null");
            }

            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }

            if(!await _authManager.ValidateUser(user))
            {
                return Unauthorized();
            }

            return Ok(new { Token = await _authManager.CreateToken() });
        }
    }
}
