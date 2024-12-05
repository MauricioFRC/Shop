using Core.DTOs.Order;
using Core.Entities;
using Core.Interfaces.Repository;
using Core.Request;
using Infrastructure.Context;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly AplicationDbContext _context;

    public OrderRepository(AplicationDbContext context)
    {
        _context = context;
    }

    public async Task<OrderResponseDTO> CreateOrder(CreateOrderRequest createOrderRequest)
    {
        var order = createOrderRequest.Adapt<Order>();

        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        return order.Adapt<OrderResponseDTO>();
    }

    public async Task<OrderResponseDTO> DeleteOrder(int id)
    {
        var deletedOrder = await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);

        _context.Orders.Remove(deletedOrder!);
        await _context.SaveChangesAsync();

        return deletedOrder.Adapt<OrderResponseDTO>();
    }

    public async Task<List<OrderResponseDTO>> ListOrders()
    {
        var orders = await _context.Orders.OrderBy(x => x.Id).ToListAsync();
        return orders.Adapt<List<OrderResponseDTO>>();
    }

    public async Task<OrderResponseDTO> SearchOrder(int id)
    {
        var searchOrder = await _context.Orders
            .FirstOrDefaultAsync(so => so.Id == id);

        return searchOrder.Adapt<OrderResponseDTO>();
    }

    public async Task<OrderResponseDTO> UpdateOrder(int id, UpdateOrderDTO updateOrderDTO)
    {
        var updateOrder = await _context.Orders.FirstOrDefaultAsync(up => up.Id == id);

        updateOrderDTO.Adapt(updateOrder);

        _context.Orders.Update(updateOrder!);
        await _context.SaveChangesAsync();

        return updateOrder.Adapt<OrderResponseDTO>();
    }
}
