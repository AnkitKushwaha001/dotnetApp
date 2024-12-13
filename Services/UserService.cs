using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class UserService : IUserService
{
    private readonly IMongoCollection<User> _usersCollection;

    public UserService(IMongoDatabase database)
    {
        _usersCollection = database.GetCollection<User>("Users");
    }

    // Create a new user dynamically using UserDTO
    public async Task<UserDTO> CreateUserAsync(UserDTO userDto)
    {
        var user = new User
        {
            firstName = userDto.FirstName,
            lastName = userDto.LastName,
            email = userDto.email,
            guid = userDto.Guid,
            address = userDto.Address,
            created_user = userDto.CreatedUser,
            created_date = DateTime.UtcNow // Dynamically set the created date
        };

        await _usersCollection.InsertOneAsync(user);

        // Return DTO to the client
        return new UserDTO
        {
            FirstName = user.firstName,
            LastName = user.lastName,
            email = user.email,
            Guid = user.guid,
            Address = user.address,
            CreatedUser = user.created_user
        };
    }

    // Get a user dynamically by ID
    public async Task<UserDTO> GetUserAsync(string id)
    {
        var user = await _usersCollection.Find(u => u.Id == new ObjectId(id)).FirstOrDefaultAsync();
        if (user == null)
        {
            return null; // Return null if user is not found
        }

        return new UserDTO
        {
            FirstName = user.firstName,
            LastName = user.lastName,
            email = user.email,
            Guid = user.guid,
            Address = user.address,
            CreatedUser = user.created_user
        };
    }

    // Get all users dynamically
    public async Task<List<User>> GetAllUsersAsync() {
// first
        return await _usersCollection.Find(_ => true).ToListAsync();

// second
        // var userDtos = new List<UserDTO>();
        // foreach (var user in users)
        // {
        //     userDtos.Add(new UserDTO
        //     {
        //         Id = user.Id.ToString(),
        //         FirstName = user.firstName,
        //         LastName = user.lastName,
        //         Guid = user.guid,
        //         Address = user.address,
        //         CreatedUser = user.created_user
        //     });
        // }

// third
        // var userDtos = users.Select(user => new UserDTO
        // {
        //     Id = user.Id.ToString(),
        //     FirstName = user.firstName,
        //     LastName = user.lastName,
        //     Guid = user.guid,
        //     Address = user.address,
        //     CreatedUser = user.created_user
        // }).ToList();

        // return userDtos;
    }

    // Update a user dynamically
    public async Task UpdateUserAsync(string id, UserDTO userDto)
    {
        var user = new User
        {
            Id = new ObjectId(id),
            firstName = userDto.FirstName,
            lastName = userDto.LastName,
            email = userDto.email,
            guid = userDto.Guid,
            address = userDto.Address,
            created_user = userDto.CreatedUser,
            created_date = DateTime.UtcNow // Dynamically update the created date if necessary
        };

        await _usersCollection.ReplaceOneAsync(u => u.Id == new ObjectId(id), user);
    }

    // Delete a user dynamically
    public async Task DeleteUserAsync(string id)
    {
        await _usersCollection.DeleteOneAsync(u => u.Id == new ObjectId(id));
    }
}
