using AutoMapper;
using RestaurantApi.Core.Application.DTOs.Dish;
using RestaurantApi.Core.Application.Interfaces.Repositories;
using RestaurantApi.Core.Domain.Entities;

namespace RestaurantApi.Core.Application.Services
{
    public class DishService : GenericService<DishDTO, AddDishDTO, UpdateDishDTO, Dish>
    {
        private readonly IDishRepository _dishRepository;
        private readonly IMapper _mapper;

        public DishService(IDishRepository dishRepository, IMapper mapper) : base(dishRepository, mapper)
        {
            _dishRepository = dishRepository;
            _mapper = mapper;
        }
    }
}
