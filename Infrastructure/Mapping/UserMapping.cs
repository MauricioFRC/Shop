using BCrypt.Net;
using Core.DTOs.User;
using Core.Entities;
using Core.Request;
using Mapster;

namespace Infrastructure.Mapping;

public class UserMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<User, UserResponseDTO>()
            .Map(dest => dest.UserId, src => src.Id)
            .Map(dest => dest.UserName, src => src.Name)
            .Map(dest => dest.Email, src => src.Email)
            .Map(dest => dest.Role, src => src.Role);

        config.NewConfig<CreateUserRequest, User>()
            .Map(dest => dest.Name, src => src.UserName)
            .Map(dest => dest.Password, src => BCrypt.Net.BCrypt.HashPassword(src.Password))
            .Map(dest => dest.Email, src => src.Email)
            .Map(dest => dest.Role, src => "Customer");

        config.NewConfig<UserUpdateDTO, User>()
            .Map(dest => dest.Name, src => src.UserName)
            .Map(dest => dest.Password, src => BCrypt.Net.BCrypt.HashPassword(src.Password))
            .Map(dest => dest.Email, src => src.Email);
    }
}
