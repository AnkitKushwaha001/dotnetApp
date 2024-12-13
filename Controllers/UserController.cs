using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/user")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    // POST: api/users
    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] UserDTO userDto)
    {
        if (userDto == null)
        {
            return BadRequest("User data is null");
        }

        var createdUserDto = await _userService.CreateUserAsync(userDto);
        return CreatedAtAction(nameof(GetUser), new { id = createdUserDto.Guid }, createdUserDto);
    }

    // GET: api/users/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser(string id)
    {
        var userDto = await _userService.GetUserAsync(id);
        if (userDto == null)
        {
            return NotFound();
        }

        return Ok(userDto);
    }

    // GET: api/users
    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var userDtos = await _userService.GetAllUsersAsync();
        return Ok(userDtos);
    }

    // PUT: api/users/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(string id, [FromBody] UserDTO userDto)
    {
        if (userDto == null)
        {
            return BadRequest("User data is null");
        }

        var existingUser = await _userService.GetUserAsync(id);
        if (existingUser == null)
        {
            return NotFound();
        }

        await _userService.UpdateUserAsync(id, userDto);
        return NoContent();
    }

    // DELETE: api/users/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(string id)
    {
        var user = await _userService.GetUserAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        await _userService.DeleteUserAsync(id);
        return NoContent();
    }
}
