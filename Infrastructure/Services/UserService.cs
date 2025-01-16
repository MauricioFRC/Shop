using Core.DTOs.User;
using Core.Interfaces.Repository;
using Core.Interfaces.Service;
using Core.Request;
using Mapster;

namespace Infrastructure.Services;

public class UserService : IUserService
{
    private readonly IUserRespository _userRespository;

    public UserService(IUserRespository userRespository)
    {
        _userRespository = userRespository;
    }

    public async Task<UserResponseDTO> CreateUser(CreateUserRequest createUserRequest, CancellationToken cancellationToken)
    {
        var createdUser = await _userRespository.CreateUser(createUserRequest, cancellationToken) 
            ?? throw new InvalidOperationException("No se pudo crear el usuario.");

        return createdUser;
    }

    public async Task<UserResponseDTO> DeleteUser(int id, CancellationToken cancellationToken)
    {
        ValidateId(id);
        var deletedUser = await _userRespository.DeleteUser(id, cancellationToken)
            ?? throw new InvalidOperationException("No se encontró el usuario.");

        return deletedUser;
    }

    public async Task<List<UserResponseDTO>> ListUsers()
    {
        var listUsers = await _userRespository.ListUsers();

        if (listUsers == null || listUsers.Count == 0)
            throw new InvalidOperationException("La lista no contiene ningun usuario.");

        return listUsers;
    }

    public async Task<UserResponseDTO> SearchUser(int id, CancellationToken cancellationToken)
    {
        ValidateId(id);
        var searchUser = await _userRespository.SearchUser(id, cancellationToken)
            ?? throw new InvalidOperationException("No se encontró el usuario.");

        return searchUser;
    }

    public async Task<UserResponseDTO> UpdateUser(int id, UserUpdateDTO userUpdateDTO, CancellationToken cancellationToken)
    {
        ValidateId(id);
        var updatedUser = await _userRespository.UpdateUser(id, userUpdateDTO, cancellationToken)
            ?? throw new InvalidOperationException("No se encontró el usuario.");

        return updatedUser;
    }

    public async Task<UserResponseDTO> UpdateUserRole(int id, UpdateUserRoleDto roleUpdateDTO, CancellationToken cancellationToken)
    {
        ValidateId(id);
        var updateUserRole = await _userRespository.UpdateUserRole(id, roleUpdateDTO, cancellationToken)
            ?? throw new ArgumentNullException($"No se encontró el usuario con el Id: {id}");

        return updateUserRole;
    }

    private static void ValidateId(int id)
    {
        if (id <= 0)
            throw new Exception("El id no puede ser negativo.");
    }
}
