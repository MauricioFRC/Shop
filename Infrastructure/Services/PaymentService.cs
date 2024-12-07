using Core.DTOs.Payment;
using Core.Interfaces.Repository;
using Core.Interfaces.Service;
using Core.Request;

namespace Infrastructure.Services;

public class PaymentService : IPaymentService
{
    private readonly IPaymentRepository _paymentRepository;

    public PaymentService(IPaymentRepository paymentRepository)
    {
        _paymentRepository = paymentRepository;
    }

    public async Task<PaymentResponseDTO> CreatePayment(CreatePaymentRequest createPaymentRequest)
    {
        var createPayment = await _paymentRepository.CreatePayment(createPaymentRequest)
            ?? throw new NullReferenceException("No se pudo realizar el pago.");

        return createPayment;
    }

    public async Task<List<PaymentResponseDTO>> ListPayments()
    {
        var paymentList = await _paymentRepository.ListPayments();

        return paymentList;
    }

    public async Task<PaymentResponseDTO> SearchPayment(int id)
    {
        ValidateId(id);
        var searchPayment = await _paymentRepository.SearchPayment(id)
            ?? throw new Exception($"No se encontró el pago con id: {id}");

        return searchPayment;
    }

    public async Task<PaymentResponseDTO> UpdatePayment(int id, UpdatePaymentDTO updatePaymentDTO)
    {
        ValidateId(id);
        var updatedPayment = await _paymentRepository.UpdatePayment(id, updatePaymentDTO)
            ?? throw new NullReferenceException("No se pudo actualizar el pago realizado.");

        return updatedPayment;
    }

    private static void ValidateId(int id)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id);
    }
}
