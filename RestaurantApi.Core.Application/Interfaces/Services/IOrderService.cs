using RestaurantApi.Core.Application.DTOs.Order;
using RestaurantApi.Core.Domain.Entities;

namespace RestaurantApi.Core.Application.Interfaces.Services
{
    public interface IOrderService : IGenericService<OrderDTO, AddOrderDTO, UpdateOrderDTO, Order>
    {
        Task<ChangeOrderStatusDTO> ChangeStatus(int id, ChangeOrderStatusDTO orderStatusDTO);
        Task<TableOrdersDTO> GetAllTableOrders(int tableId);
    }
}
