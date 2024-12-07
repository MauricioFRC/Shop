using Core.DTOs.Payment;
using Core.Interfaces.Service;
using Core.Request;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Shop.Server.Controllers;

public class PaymentController : BaseApiController
{
    private readonly IPaymentService _paymentService;
    private readonly IValidator<CreatePaymentRequest> _createPaymentRequestValidator;
    private readonly IValidator<UpdatePaymentDTO> _updatePaymentDTOValidator;

    public PaymentController(
        IPaymentService paymentService,
        IValidator<CreatePaymentRequest> createPaymentRequestValidator,
        IValidator<UpdatePaymentDTO> updatePaymentDTOValidator
        )
    {
        _paymentService = paymentService;
        _createPaymentRequestValidator = createPaymentRequestValidator;
        _updatePaymentDTOValidator = updatePaymentDTOValidator;
    }

    [HttpPost("create-payment")]
    public async Task<IActionResult> CreatePayment([FromBody] CreatePaymentRequest createPaymentRequest)
    {
        var result = await _createPaymentRequestValidator.ValidateAsync(createPaymentRequest);
        if (!result.IsValid) return BadRequest(result.Errors.Select(x => new
        {
            x.PropertyName, x.ErrorMessage
        }));

        return Ok(await _paymentService.CreatePayment(createPaymentRequest));
    }

    [HttpGet("list-payments")]
    public async Task<IActionResult> ListPayments()
    {
        return Ok(await _paymentService.ListPayments());
    }

    [HttpGet("search-payments/{id}")]
    public async Task<IActionResult> SearchPayment([FromRoute] int id)
    {
        return Ok(await _paymentService.SearchPayment(id));
    }

    [HttpPut("update-payment/{id}")]
    public async Task<IActionResult> UpdatePayment([FromRoute] int id, [FromBody] UpdatePaymentDTO updatePaymentDTO)
    {
        var result = await _updatePaymentDTOValidator.ValidateAsync(updatePaymentDTO);
        if (!result.IsValid) return BadRequest(result.Errors.Select(x => new
        {
            x.PropertyName,
            x.ErrorMessage
        }));

        return Ok(await _paymentService.UpdatePayment(id, updatePaymentDTO));
    }
}
