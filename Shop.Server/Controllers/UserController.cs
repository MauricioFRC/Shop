using Core.DTOs.User;
using Core.Interfaces.Service;
using Core.Request;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Shop.Server.Controllers;

public class UserController : BaseApiController
{
    private readonly IUserService _userService;
    private readonly IValidator<CreateUserRequest> _createUserValidation;
    private readonly IValidator<UserUpdateDTO> _updateUserValidation;

    public UserController(
        IUserService userService, 
        IValidator<UserUpdateDTO> updateUserValidation, 
        IValidator<CreateUserRequest> createUserValidation
        )
    {
        _userService = userService;
        _createUserValidation = createUserValidation;
        _updateUserValidation = updateUserValidation;
    }

    [HttpPost("create-user")]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest createUserRequest)
    {
        var result = await _createUserValidation.ValidateAsync(createUserRequest);
        if (!result.IsValid) return BadRequest(result.Errors.Select(x => new
        {
            x.PropertyName, x.ErrorMessage
        }));

        return Ok(await _userService.CreateUser(createUserRequest));
    }

    [HttpGet("search-user/{id}")]
    public async Task<IActionResult> SearchUser([FromRoute] int id)
    {
        return Ok(await _userService.SearchUser(id));
    }

    [HttpGet("list-users")]
    public async Task<IActionResult> ListUsers()
    {
        return Ok(await _userService.ListUsers());
    }

    [HttpDelete("delete-user/{id}")]
    public async Task<IActionResult> DeleteUser([FromRoute] int id)
    {
        return Ok(await _userService.DeleteUser(id));
    }

    [HttpPut("update-user/{id}")]
    public async Task<IActionResult> UpdateUser([FromRoute] int id, [FromBody] UserUpdateDTO userUpdateDTO)
    {
        var result = await _updateUserValidation.ValidateAsync(userUpdateDTO);
        if (!result.IsValid) return BadRequest(result.Errors.Select(x => new
        {
            x.PropertyName, x.ErrorMessage
        }));

        return Ok(await _userService.UpdateUser(id, userUpdateDTO));
    }
}
