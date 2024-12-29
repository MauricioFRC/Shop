﻿using Core.DTOs.Payment;
using Core.Request;

namespace Core.Interfaces.Service;

public interface IPaymentService
{
    public Task<PaymentResponseDTO> CreatePayment(CreatePaymentRequest createPaymentRequest);
    public Task<List<PaymentResponseDTO>> ListPayments();
    public Task<PaymentResponseDTO> SearchPayment(int id);
    public Task<PaymentResponseDTO> UpdatePayment(int id, UpdatePaymentDTO updatePaymentDTO);
}