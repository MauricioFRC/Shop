﻿using Core.DTOs.OrderDetail;
using Core.Request;

namespace Core.Interfaces.Repository;

public interface IOrderDetailRepository
{
    public Task<OrderDetailResponseDTO> CreateOrderDetail(CreateOrderDetailRequest createOrderDetailRequest);
    public Task<OrderDetailResponseDTO> UpdateOrderDetail(int id, UpdateOrderDetailDTO updateOrderDetailDTO);
    public Task<OrderDetailResponseDTO> DeleteOrderDetail(int id);
    public Task<OrderDetailResponseDTO> SearchOrderDetail(int id);
    public Task<List<OrderDetailResponseDTO>> ListOrderDetails();
}