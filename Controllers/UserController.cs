using Microsoft.AspNetCore.Mvc;
using TodoAPI.AppDataContext;
using TodoAPI.Models;

namespace TodoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly TodoDbContext _dbContext;

        public UserController(TodoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var user = new User
                {
                    Id = Guid.NewGuid(),
                    UserName = request.UserName
                };

                await _dbContext.Users.AddAsync(user);
                await _dbContext.SaveChangesAsync();

                return Ok(new { message = "User successfully created", data = new { user.Id, user.UserName } });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while creating the user", error = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            try
            {
                var users = await Task.FromResult(_dbContext.Users.ToList());
                if (users == null || !users.Any())
                {
                    return Ok(new { message = "No users found" });
                }
                return Ok(new { message = "Successfully retrieved all users", data = users });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving all users", error = ex.Message });
            }
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetUserByIdAsync(Guid id)
        {
            try
            {
                var user = await Task.FromResult(_dbContext.Users.FirstOrDefault(u => u.Id == id));
                if (user == null)
                {
                    return NotFound(new { message = $"No user with id {id} found" });
                }
                return Ok(new { message = $"Successfully retrieved user with id {id}", data = user });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"An error occurred while retrieving user with id {id}", error = ex.Message });
            }
        }
    }

    public class CreateUserRequest
    {
        public required string UserName { get; set; }
    }
}
