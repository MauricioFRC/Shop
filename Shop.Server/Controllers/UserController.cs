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
    private readonly IValidator<UpdateUserRoleDto> _updateUserRoleValidation;

    public UserController(
        IUserService userService, 
        IValidator<CreateUserRequest> createUserValidation, 
        IValidator<UserUpdateDTO> updateUserValidation, 
        IValidator<UpdateUserRoleDto> updateUserRoleValidation
        )
    {
        _userService = userService;
        _createUserValidation = createUserValidation;
        _updateUserValidation = updateUserValidation;
        _updateUserRoleValidation = updateUserRoleValidation;
    }

    [HttpPost("create-user")]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest createUserRequest, CancellationToken cancellationToken)
    {
        var result = await _createUserValidation.ValidateAsync(createUserRequest, cancellationToken);
        if (!result.IsValid) return BadRequest(result.Errors.Select(x => new { x.PropertyName, x.ErrorMessage }));

        return Ok(await _userService.CreateUser(createUserRequest, cancellationToken));
    }

    [HttpGet("search-user/{id}")]
    public async Task<IActionResult> SearchUser([FromRoute] int id, CancellationToken cancellationToken)
    {
        return Ok(await _userService.SearchUser(id, cancellationToken));
    }

    [HttpGet("list-users")]
    public async Task<IActionResult> ListUsers()
    {
        return Ok(await _userService.ListUsers());
    }

    [HttpDelete("delete-user/{id}")]
    public async Task<IActionResult> DeleteUser([FromRoute] int id, CancellationToken cancellationToken)
    {
        return Ok(await _userService.DeleteUser(id, cancellationToken));
    }

    [HttpPut("update-user/{id}")]
    public async Task<IActionResult> UpdateUser([FromRoute] int id, [FromBody] UserUpdateDTO userUpdateDTO, CancellationToken cancellationToken)
    {
        var result = await _updateUserValidation.ValidateAsync(userUpdateDTO, cancellationToken);
        if (!result.IsValid) return BadRequest(result.Errors.Select(x => new { x.PropertyName, x.ErrorMessage }));

        return Ok(await _userService.UpdateUser(id, userUpdateDTO, cancellationToken));
    }

    [HttpPut("change-roles/{userId}")]
    public async Task<IActionResult> UpdateUserRole([FromRoute] int userId, [FromBody] UpdateUserRoleDto updateUserRoleDto, CancellationToken cancellationToken)
    {
        var result = await _updateUserRoleValidation.ValidateAsync(updateUserRoleDto, cancellationToken);
        if (!result.IsValid) return BadRequest(result.Errors.Select(x => new { x.PropertyName, x.ErrorMessage }));

        return Ok(await _userService.UpdateUserRole(userId, updateUserRoleDto, cancellationToken));
    }
}
