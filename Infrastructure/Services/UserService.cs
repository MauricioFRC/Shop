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

    public async Task<UserResponseDTO> CreateUser(CreateUserRequest createUserRequest)
    {
        var createdUser = await _userRespository.CreateUser(createUserRequest) 
            ?? throw new InvalidOperationException("No se pudo crear el usuario.");

        return createdUser.Adapt<UserResponseDTO>();
    }

    public async Task<UserResponseDTO> DeleteUser(int id)
    {
        ValidateId(id);
        var deletedUser = await _userRespository.DeleteUser(id)
            ?? throw new InvalidOperationException("No se encontró el usuario.");

        return deletedUser.Adapt<UserResponseDTO>();
    }

    public async Task<List<UserResponseDTO>> ListUsers()
    {
        var listUsers = await _userRespository.ListUsers();

        if (listUsers == null || listUsers.Count == 0)
            throw new InvalidOperationException("La lista no contiene ningun usuario.");

        return listUsers.Adapt<List<UserResponseDTO>>();
    }

    public async Task<UserResponseDTO> SearchUser(int id)
    {
        ValidateId(id);
        var searchUser = await _userRespository.SearchUser(id)
            ?? throw new InvalidOperationException("No se encontró el usuario.");

        return searchUser.Adapt<UserResponseDTO>();
    }

    public async Task<UserResponseDTO> UpdateUser(int id, UserUpdateDTO userUpdateDTO)
    {
        ValidateId(id);
        var updatedUser = await _userRespository.UpdateUser(id, userUpdateDTO)
            ?? throw new InvalidOperationException("No se encontró el usuario.");

        return updatedUser.Adapt<UserResponseDTO>();
    }

    private static void ValidateId(int id)
    {
        if (id <= 0)
            throw new Exception("El id no puede ser negativo.");
    }
}
