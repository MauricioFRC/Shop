using Core.DTOs.OrderDetail;
using Core.Entities;
using Core.Interfaces.Repository;
using Core.Request;
using Infrastructure.Context;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class OrderDetailRepository : IOrderDetailRepository
{
    private readonly AplicationDbContext _context;

    public OrderDetailRepository(AplicationDbContext context)
    {
        _context = context;
    }

    public async Task<OrderDetailResponseDTO> CreateOrderDetail(CreateOrderDetailRequest createOrderDetailRequest)
    {
        var createOrderDetail = createOrderDetailRequest.Adapt<OrderDetail>();
        
        _context.OrderDetails.Add(createOrderDetail);
        await _context.SaveChangesAsync();

        return createOrderDetail.Adapt<OrderDetailResponseDTO>();
    }

    public Task<OrderDetailResponseDTO> DeleteOrderDetail(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<OrderDetailResponseDTO>> ListOrderDetails()
    {
        var listOrderDetails = await _context.OrderDetails
            .Include(x => x.Order)
                .ThenInclude(x => x.User)
            .OrderBy(x => x.Id)
            .ToListAsync();

        return listOrderDetails.Adapt<List<OrderDetailResponseDTO>>();
    }

    public async Task<OrderDetailResponseDTO> SearchOrderDetail(int id)
    {
        var searchOrderDetail = await _context.OrderDetails
            .Include(x => x.Order)
                .ThenInclude(x => x.User)
            .FirstOrDefaultAsync(x => x.Id == id);

        return searchOrderDetail.Adapt<OrderDetailResponseDTO>();
    }

    public Task<OrderDetailResponseDTO> UpdateOrderDetail(int id, UpdateOrderDetailDTO updateOrderDetailDTO)
    {
        throw new NotImplementedException();
    }
}
