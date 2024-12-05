﻿using Core.DTOs.Order;
using Core.Request;

namespace Core.Interfaces.Repository;

public interface IOrderRepository
{
    public Task<OrderResponseDTO> CreateOrder(CreateOrderRequest createOrderRequest);
    public Task<OrderResponseDTO> UpdateOrder(int id, UpdateOrderDTO updateOrderDTO);
    public Task<OrderResponseDTO> DeleteOrder(int id);
    public Task<OrderResponseDTO> SearchOrder(int id);
    public Task<List<OrderResponseDTO>> ListOrders();
}