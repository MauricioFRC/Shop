using Core.DTOs.User;
using Core.Request;

namespace Core.Interfaces.Service;

public interface IUserService
{
    public Task<UserResponseDTO> CreateUser(CreateUserRequest createUserRequest);
    public Task<UserResponseDTO> UpdateUser(int id, UserUpdateDTO userUpdateDTO);
    public Task<UserResponseDTO> DeleteUser(int id);
    public Task<UserResponseDTO> SearchUser(int id);
    public Task<List<UserResponseDTO>> ListUsers();
}
