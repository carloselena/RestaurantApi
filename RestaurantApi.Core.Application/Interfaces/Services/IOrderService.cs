using RestaurantApi.Core.Application.DTOs.Order;
using RestaurantApi.Core.Domain.Entities;

namespace RestaurantApi.Core.Application.Interfaces.Services
{
    public interface IOrderService : IGenericService<OrderDTO, AddOrderDTO, UpdateOrderDTO, Order>
    {
        Task ChangeStatus(int id);
        Task<TableOrdersDTO> GetAllTableOrders(int tableId);
    }
}
