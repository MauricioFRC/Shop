using Core.DTOs.OrderDetail;
using Core.Interfaces.Service;
using Core.Request;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Shop.Server.Controllers;

public class OrderDetailController : BaseApiController
{
    private readonly IOrderDetailService _orderDetailService;
    private readonly IValidator<CreateOrderDetailRequest> _createOrderDetailValidator;
    private readonly IValidator<UpdateOrderDetailDTO> _updateOrderDetailValidator;

    public OrderDetailController(
        IOrderDetailService orderDetailService,
        IValidator<CreateOrderDetailRequest> createOrderDetailValidator,
        IValidator<UpdateOrderDetailDTO> updateOrderDetailValidator)
    {
        _orderDetailService = orderDetailService;
        _createOrderDetailValidator = createOrderDetailValidator;
        _updateOrderDetailValidator = updateOrderDetailValidator;
    }

    [HttpPost("create-order-detail")]
    public async Task<IActionResult> CreateOrderDetail([FromBody] CreateOrderDetailRequest createOrderDetailRequest)
    {
        var result = await _createOrderDetailValidator.ValidateAsync(createOrderDetailRequest);
        if (!result.IsValid) return BadRequest(result.Errors.Select(x => new
        {
            x.PropertyName, x.ErrorMessage
        }));

        return Ok(await _orderDetailService.CreateOrderDetail(createOrderDetailRequest));
    }

    [HttpGet("list-order-detail")]
    public async Task<IActionResult> ListOrderDetail()
    {
        return Ok(await _orderDetailService.ListOrderDetails());
    }

    [HttpGet("search-order-detail/{id}")]
    public async Task<IActionResult> SearchOrderDetail([FromRoute] int id)
    {
        return Ok(await _orderDetailService.SearchOrderDetail(id));
    }

    [HttpPut("update-order-detail/{id}")]
    public async Task<IActionResult> UpdateOrderDetail([FromRoute] int id, [FromBody] UpdateOrderDetailDTO updateOrderDetailDTO)
    {
        var result = await _updateOrderDetailValidator.ValidateAsync(updateOrderDetailDTO);
        if (!result.IsValid) return BadRequest(result.Errors.Select(x => new
        {
            x.PropertyName, x.ErrorMessage
        }));

        return Ok(await _orderDetailService.UpdateOrderDetail(id, updateOrderDetailDTO));
    }

    [HttpDelete("delete-order-detail/{id}")]
    public async Task<IActionResult> DeleteOrderDetail([FromRoute] int id)
    {
        return Ok(await _orderDetailService.DeleteOrderDetail(id));
    }
}
