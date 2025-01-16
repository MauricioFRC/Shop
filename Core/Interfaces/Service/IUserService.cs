using Core.DTOs.User;
using Core.Request;

namespace Core.Interfaces.Service;

public interface IUserService
{
    public Task<UserResponseDTO> CreateUser(CreateUserRequest createUserRequest, CancellationToken cancellationToken);
    public Task<UserResponseDTO> UpdateUser(int id, UserUpdateDTO userUpdateDTO, CancellationToken cancellationToken);
    public Task<UserResponseDTO> DeleteUser(int id, CancellationToken cancellationToken);
    public Task<UserResponseDTO> SearchUser(int id, CancellationToken cancellationToken);
    public Task<List<UserResponseDTO>> ListUsers();
    public Task<UserResponseDTO> UpdateUserRole(int id, UpdateUserRoleDto roleUpdateDTO, CancellationToken cancellationToken);
}
