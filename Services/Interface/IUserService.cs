using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserService
{
    Task<UserDTO> CreateUserAsync(UserDTO userDto);
    Task<UserDTO> GetUserAsync(string id);
    Task<List<User>> GetAllUsersAsync();
    // Task<List<UserDTO>> GetAllUsersAsync();
    Task UpdateUserAsync(string id, UserDTO userDto);
    Task DeleteUserAsync(string id);
}
