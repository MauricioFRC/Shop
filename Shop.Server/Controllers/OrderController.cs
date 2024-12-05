using Core.DTOs.Order;
using Core.Interfaces.Service;
using Core.Request;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Shop.Server.Controllers;

public class OrderController : BaseApiController
{
    private readonly IOrderService _orderService;
    private readonly IValidator<CreateOrderRequest> _createOrderValidator;
    private readonly IValidator<UpdateOrderDTO> _updateOrderValidator;

    public OrderController(
        IOrderService orderService,
        IValidator<CreateOrderRequest> createOrderValidator,
        IValidator<UpdateOrderDTO> updateOrderValidator
        )
    {
        _orderService = orderService;
        _createOrderValidator = createOrderValidator;
        _updateOrderValidator = updateOrderValidator;
    }

    [HttpPost("create-order")]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest createOrderRequest)
    {
        var result = await _createOrderValidator.ValidateAsync(createOrderRequest);
        if (!result.IsValid) return BadRequest(result.Errors.Select(x => new
        {
            x.PropertyName, x.ErrorMessage
        }));

        return Ok(await _orderService.CreateOrder(createOrderRequest));
    }

    [HttpGet("search-order/{id}")]
    public async Task<IActionResult> SearchOrder([FromRoute] int id)
    {
        return Ok(await _orderService.SearchOrder(id));
    }

    [HttpGet("list-orders")]
    public async Task<IActionResult> ListOrders()
    {
        return Ok(await _orderService.ListOrders());
    }

    [HttpPut("update-order/{id}")]
    public async Task<IActionResult> UpdateOrder([FromRoute] int id, [FromBody] UpdateOrderDTO updateOrderDTO)
    {
        var result = await _updateOrderValidator.ValidateAsync(updateOrderDTO);
        if (!result.IsValid) return BadRequest(result.Errors.Select(x => new
        {
            x.PropertyName,
            x.ErrorMessage
        }));

        return Ok(await _orderService.UpdateOrder(id, updateOrderDTO));
    }

    [HttpDelete("delete-order/{id}")]
    public async Task<IActionResult> DeleteOrder([FromRoute] int id)
    {
        return Ok(await _orderService.DeleteOrder(id));
    }
}
