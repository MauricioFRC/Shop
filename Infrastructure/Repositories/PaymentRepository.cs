using Core.DTOs.Payment;
using Core.Entities;
using Core.Interfaces.Repository;
using Core.Request;
using Infrastructure.Context;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class PaymentRepository : IPaymentRepository
{
    private readonly AplicationDbContext _context;

    public PaymentRepository(AplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PaymentResponseDTO> CreatePayment(CreatePaymentRequest createPaymentRequest)
    {
        var createPayment = createPaymentRequest.Adapt<Payment>();

        _context.Payments.Add(createPayment);
        await _context.SaveChangesAsync();

        return createPayment.Adapt<PaymentResponseDTO>();
    }

    public async Task<List<PaymentResponseDTO>> ListPayments()
    {
        var paymentList = await _context.Payments
            .Include(x => x.Order)
                .ThenInclude(x => x.User)
            .ToListAsync();

        return paymentList.Adapt<List<PaymentResponseDTO>>();
    }

    public async Task<PaymentResponseDTO> SearchPayment(int id)
    {
        var searchedPayment = await _context.Payments
            .Include(x => x.Order)
                .ThenInclude(x => x.User)
            .FirstOrDefaultAsync(x => x.Id == id);

        return searchedPayment.Adapt<PaymentResponseDTO>();
    }

    public async Task<PaymentResponseDTO> UpdatePayment(int id, UpdatePaymentDTO updatePaymentDTO)
    {
        var updatedPayment = await _context.Payments
            .Include(x => x.Order)
                .ThenInclude(x => x.User)
            .FirstOrDefaultAsync(x => x.Id == id);

        updatePaymentDTO.Adapt(updatedPayment);

        _context.Payments.Update(updatedPayment!);
        await _context.SaveChangesAsync();

        return updatedPayment.Adapt<PaymentResponseDTO>();
    }
}
