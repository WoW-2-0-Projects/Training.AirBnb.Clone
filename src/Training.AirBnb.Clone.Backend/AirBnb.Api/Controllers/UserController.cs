using Backend_Project.Application.Entity;
using Backend_Project.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AirBnb.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IEntityBaseService<User> _userService;

        public UserController(IEntityBaseService<User> userService)
        {
            _userService = userService;
        }

        [HttpGet("users")]
        public IActionResult GetAllUsers()
        {
            var users = _userService.Get(_ => true).ToList();
            return Ok(users);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] User user)
        {
            var newUser = await _userService.CreateAsync(user);
            return Ok(newUser);
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] User user)
        {
            var updatedUser = await _userService.UpdateAsync(user);
            return Ok(updatedUser);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var deletedUser = await _userService.DeleteAsync(id);
            return Ok(deletedUser);
        }
    }
}