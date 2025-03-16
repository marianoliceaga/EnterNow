using EnterNow.API.Models;
using EnterNow.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EnterNow.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ApplicationUser>>> GetUsers()
        {
            return Ok(await _userService.GetUsers());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApplicationUser>> GetUserById(string id)
        {
            var user = await _userService.GetUserById(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
        {
            var user = new ApplicationUser
            {
                UserName = request.Username,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                MembershipExpiry = request.MembershipExpiry,
                IsPaymentCurrent = request.IsPaymentCurrent
            };

            var result = await _userService.CreateUser(user, request.Password);
            if (!result) return BadRequest("User creation failed");

            return Ok("User created successfully");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] UpdateUserRequest request)
        {
            var user = await _userService.GetUserById(id);
            if (user == null) return NotFound();

            user.Email = request.Email;
            user.PhoneNumber = request.PhoneNumber;
            user.MembershipExpiry = request.MembershipExpiry;
            user.IsPaymentCurrent = request.IsPaymentCurrent;

            var result = await _userService.UpdateUser(user);
            if (!result) return BadRequest("Update failed");

            return Ok("User updated successfully");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var result = await _userService.DeleteUser(id);
            if (!result) return NotFound("User not found");

            return Ok("User deleted successfully");
        }
    }

    public class CreateUserRequest
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public DateTime MembershipExpiry { get; set; }
        public bool IsPaymentCurrent { get; set; }
    }

    public class UpdateUserRequest
    {
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime MembershipExpiry { get; set; }
        public bool IsPaymentCurrent { get; set; }
    }
}
