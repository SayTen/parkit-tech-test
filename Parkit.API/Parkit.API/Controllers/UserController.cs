using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Parkit.Core.DAL;
using Parkit.Core.Models;
using System;
using System.Threading.Tasks;

namespace Parkit.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private IUserRepository _userRepository;

        public UserController(ILogger<UserController> logger, IUserRepository userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        [HttpGet("{entityId}")]
        public async Task<ActionResult> Get([FromRoute] Guid entityId)
        {
            var user = await _userRepository.GetUserById(entityId);

            if (user is null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost()]
        public async Task<ActionResult> Add([FromBody] User user)
        {
            _userRepository.InsertUser(user);
            await _userRepository.Save();
            return Ok(user);
        }

        [HttpDelete("{entityId}")]
        public async Task<ActionResult> Delete([FromRoute] Guid entityId)
        {
            _userRepository.DeleteUser(entityId);
            await _userRepository.Save();
            return Ok();
        }

        [HttpPatch("{entityId}")]
        public async Task<ActionResult> Update([FromRoute] Guid entityId, [FromBody] User user)
        {
            if (user.Id != entityId)
            {
                return StatusCode(400);
            }

            _userRepository.UpdateUser(user);
            await _userRepository.Save();
            return Ok(user);
        }
    }
}
