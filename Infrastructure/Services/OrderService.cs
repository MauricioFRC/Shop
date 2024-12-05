using Core.DTOs.Order;
using Core.Interfaces.Repository;
using Core.Interfaces.Service;
using Core.Request;
using Infrastructure.Context;
using Mapster;

namespace Infrastructure.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;

    public OrderService(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<OrderResponseDTO> CreateOrder(CreateOrderRequest createOrderRequest)
    {
        var createOrder = await _orderRepository.CreateOrder(createOrderRequest) 
            ?? throw new NullReferenceException("No se pudo crear la petición.");

        return createOrder;
    }

    public async Task<OrderResponseDTO> DeleteOrder(int id)
    {
        ValidateId(id);
        var deletedOrder = await _orderRepository.DeleteOrder(id) 
            ?? throw new NullReferenceException("No se pudo eliminar el usuario.");

        return deletedOrder;
    }

    public async Task<List<OrderResponseDTO>> ListOrders()
    {
        var orderList = await _orderRepository.ListOrders()
            ?? throw new NullReferenceException("No hay ninguna Orden");

        return orderList.ToList();
    }

    public async Task<OrderResponseDTO> SearchOrder(int id)
    {
        ValidateId(id);
        var searchedOrder = await _orderRepository.SearchOrder(id)
            ?? throw new NullReferenceException($"No se encontró el order con id {id}");

        return searchedOrder;
    }

    public async Task<OrderResponseDTO> UpdateOrder(int id, UpdateOrderDTO updateOrderDTO)
    {
        ValidateId(id);
        var updatedOrder = await _orderRepository.UpdateOrder(id, updateOrderDTO)
            ?? throw new NullReferenceException("No se pudó actualizar la orden.");

        return updatedOrder;
    }

    private static void ValidateId(int id)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id);
    }
}
