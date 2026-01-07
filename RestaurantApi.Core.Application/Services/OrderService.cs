using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RestaurantApi.Core.Application.DTOs.Order;
using RestaurantApi.Core.Application.Enums;
using RestaurantApi.Core.Application.Interfaces.Repositories;
using RestaurantApi.Core.Application.Interfaces.Services;
using RestaurantApi.Core.Domain.Entities;

namespace RestaurantApi.Core.Application.Services
{
    public class OrderService : GenericService<OrderDTO, AddOrderDTO, UpdateOrderDTO, Order>, IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IDishRepository _dishRepository;
        private readonly ITableRepository _tableRepository;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository orderRepository, IDishRepository dishRepository, ITableRepository tableRepository, IMapper mapper) : base(orderRepository, mapper)
        {
            _orderRepository = orderRepository;
            _dishRepository = dishRepository;
            _tableRepository = tableRepository;
            _mapper = mapper;
        }

        public override async Task<AddOrderDTO> Add(AddOrderDTO orderDTO)
        {
            await ValidateTable(orderDTO.TableId);
            await ValidateDishes(orderDTO.DishesIds);

            return await base.Add(orderDTO);
        }

        public override async Task<UpdateOrderDTO> Update(int id, UpdateOrderDTO orderDTO, Func<IQueryable<Order>, IQueryable<Order>>? includes = null)
        {
            await ValidateDishes(orderDTO.DishesIds);

            var order = await _orderRepository.GetByIdAsync(id, q => q.Include(o => o.Dishes));

            if (order == null)
                throw new KeyNotFoundException($"No hay orden con id {id}");

            SyncDishes(order, orderDTO.DishesIds);

            await _orderRepository.UpdateAsync(order);

            return _mapper.Map<UpdateOrderDTO>(order);
        }

        public async Task ChangeStatus(int id)
        {
            Order order = await _orderRepository.GetByIdAsync(id, q => q.Include(o => o.Dishes)
                                                                        .ThenInclude(od => od.Dish));
            if (order == null)
                throw new KeyNotFoundException($"No hay orden con id {id}");

            order.Status = OrderStatus.COMPLETADA.ToString();
            await _orderRepository.UpdateAsync(order);
        }

        public async Task<TableOrdersDTO> GetAllTableOrders(int tableId)
        {
            await ValidateTable(tableId);

            TableOrdersDTO tableOrders = new()
            {
                TableId = tableId
            };

            var orders = await _orderRepository.GetAllTableOrdersAsync(tableId);
            if (orders != null && orders.Count > 0)
                tableOrders.Orders = _mapper.Map<List<OrderDTO>>(orders.Where(o => o.Status == "EN_PROCESO"));

            return tableOrders;
        }

        private async Task ValidateTable(int tableId)
        {
            var table = await _tableRepository.GetByIdAsync(tableId);
            if (table == null)
                throw new KeyNotFoundException("Debe asegurarse de que la mesa exista");
        }
        private async Task ValidateDishes(List<int> dishesIds)
        {
            var dishesDB = await _dishRepository.GetAllAsync();
            dishesDB = dishesDB.Where(d => dishesIds.Contains(d.Id)).ToList();

            if (dishesDB.Count != dishesIds.Count)
                throw new KeyNotFoundException("Debe asegurarse de que los platos existan");
        }

        private static void SyncDishes(Order order, List<int> newDishesIds)
        {
            var currentIds = order.Dishes.Select(d => d.DishId).ToList();

            var toAdd = newDishesIds.Except(currentIds);
            foreach (var id in toAdd)
            {
                order.Dishes.Add(new OrderDishes
                {
                    DishId = id,
                    OrderId = order.Id
                });
            }

            var toRemove = order.Dishes.Where(d => !newDishesIds.Contains(d.DishId)).ToList();
            foreach (var d in toRemove)
                order.Dishes.Remove(d);
        }
    }
}
