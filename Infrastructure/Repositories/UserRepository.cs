using Core.DTOs.User;
using Core.Entities;
using Core.Interfaces.Repository;
using Core.Request;
using Infrastructure.Context;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository : IUserRespository
{
    private readonly AplicationDbContext _context;

    public UserRepository(AplicationDbContext context)
    {
        _context = context;
    }

    public async Task<UserResponseDTO> CreateUser(CreateUserRequest createUserRequest, CancellationToken cancellationToken)
    {
        var createdUser = createUserRequest.Adapt<User>();

        _context.Users.Add(createdUser);
        await _context.SaveChangesAsync(cancellationToken);

        return createdUser.Adapt<UserResponseDTO>();
    }

    public async Task<UserResponseDTO> DeleteUser(int id, CancellationToken cancellationToken)
    {
        var deletedUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == id, cancellationToken);

        _context.Users.Remove(deletedUser!);
        await _context.SaveChangesAsync(cancellationToken);

        return deletedUser.Adapt<UserResponseDTO>();
    }

    public async Task<List<UserResponseDTO>> ListUsers()
    {
        var users = await _context.Users.ToListAsync();
        return users.Adapt<List<UserResponseDTO>>();
    }

    public async Task<UserResponseDTO> SearchUser(int id, CancellationToken cancellationToken)
    {
        var searchUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        return searchUser.Adapt<UserResponseDTO>();
    }

    public async Task<UserResponseDTO> UpdateUser(int id, UserUpdateDTO userUpdateDTO, CancellationToken cancellationToken)
    {
        var searchUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        userUpdateDTO.Adapt(searchUser);

        _context.Users.Update(searchUser!);
        await _context.SaveChangesAsync(cancellationToken);

        return searchUser.Adapt<UserResponseDTO>();
    }

    public async Task<UserResponseDTO> UpdateUserRole(int id, UpdateUserRoleDto userRoleDTO, CancellationToken cancellationToken)
    {
        var searchUsertoUpdate = await _context.Users.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        userRoleDTO.Adapt(searchUsertoUpdate);

        _context.Users.Update(searchUsertoUpdate!);
        await _context.SaveChangesAsync(cancellationToken);

        return searchUsertoUpdate.Adapt<UserResponseDTO>();
    }
}
