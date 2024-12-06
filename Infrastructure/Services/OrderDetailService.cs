using Core.DTOs.OrderDetail;
using Core.Interfaces.Repository;
using Core.Interfaces.Service;
using Core.Request;

namespace Infrastructure.Services;

public class OrderDetailService : IOrderDetailService
{
    private readonly IOrderDetailRepository _orderDetailRepository;

    public OrderDetailService(IOrderDetailRepository orderDetailRepository)
    {
        _orderDetailRepository = orderDetailRepository;
    }

    public async Task<OrderDetailResponseDTO> CreateOrderDetail(CreateOrderDetailRequest createOrderDetailRequest)
    {
        var createOrder = await _orderDetailRepository.CreateOrderDetail(createOrderDetailRequest)
            ?? throw new NullReferenceException("No se pudo crear el order detail");
        return createOrder;
    }

    public async Task<OrderDetailResponseDTO> DeleteOrderDetail(int id)
    {
        ValidateId(id);
        var deletedOrderDetail = await _orderDetailRepository
            .DeleteOrderDetail(id)
            ?? throw new NullReferenceException("No se pudo eliminar el usuario.");

        return deletedOrderDetail;
    }

    public async Task<List<OrderDetailResponseDTO>> ListOrderDetails()
    {
        return await _orderDetailRepository.ListOrderDetails();
    }

    public async Task<OrderDetailResponseDTO> SearchOrderDetail(int id)
    {
        ValidateId(id);
        return await _orderDetailRepository.SearchOrderDetail(id);
    }

    public Task<OrderDetailResponseDTO> UpdateOrderDetail(int id, UpdateOrderDetailDTO updateOrderDetailDTO)
    {
        ValidateId(id);
        var updatedOrderDetail = _orderDetailRepository
            .UpdateOrderDetail(id, updateOrderDetailDTO)
            ?? throw new NullReferenceException("No se encontró al usuario.");

        return updatedOrderDetail;
    }

    private static void ValidateId(int id)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id);
    }
}
