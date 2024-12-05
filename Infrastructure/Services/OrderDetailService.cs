using Core.DTOs.OrderDetail;
using Core.Interfaces.Repository;
using Core.Interfaces.Service;
using Core.Request;

namespace Infrastructure.Services;

public class OrderDetailService : IOrderDetailService
{
    private readonly IOrderDetailRepository _createOrderRepository;

    public OrderDetailService(IOrderDetailRepository createOrderRepository)
    {
        _createOrderRepository = createOrderRepository;
    }

    public async Task<OrderDetailResponseDTO> CreateOrderDetail(CreateOrderDetailRequest createOrderDetailRequest)
    {
        var createOrder = await _createOrderRepository.CreateOrderDetail(createOrderDetailRequest)
            ?? throw new NullReferenceException("No se pudo crear el order detail");
        return createOrder;
    }

    public Task<OrderDetailResponseDTO> DeleteOrderDetail(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<OrderDetailResponseDTO>> ListOrderDetails()
    {
        // var orderDetailList = await _createOrderRepository.ListOrderDetails();
        return await _createOrderRepository.ListOrderDetails();
    }

    public async Task<OrderDetailResponseDTO> SearchOrderDetail(int id)
    {
        ValidateId(id);
        return await _createOrderRepository.SearchOrderDetail(id);
    }

    public Task<OrderDetailResponseDTO> UpdateOrderDetail(int id, UpdateOrderDetailDTO updateOrderDetailDTO)
    {
        throw new NotImplementedException();
    }

    private static void ValidateId(int id)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id);
    }
}
