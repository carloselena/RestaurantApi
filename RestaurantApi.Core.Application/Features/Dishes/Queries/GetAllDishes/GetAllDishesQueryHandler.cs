using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RestaurantApi.Core.Application.Interfaces.Repositories;
using RestaurantApi.Core.Application.Wrappers;

namespace RestaurantApi.Core.Application.Features.Dishes.Queries.GetAllDishes
{
    public class GetAllDishesQueryHandler : IRequestHandler<GetAllDishesQuery, Response<List<DishDto>>>
    {
        private readonly IDishRepository _dishRepository;
        private readonly IMapper _mapper;

        public GetAllDishesQueryHandler(IDishRepository dishRepository, IMapper mapper)
        {
            _dishRepository = dishRepository;
            _mapper = mapper;
        }
        public async Task<Response<List<DishDto>>> Handle(GetAllDishesQuery request, CancellationToken cancellationToken)
        {
            var dishes = await _dishRepository
                                .GetAllAsync(q => q.Include(d => d.Ingredients)
                                                    .ThenInclude(di => di.Ingredient));
            if (dishes == null || dishes.Count == 0)
                return Response<List<DishDto>>.Fail("No hay platos");

            var response = _mapper.Map<List<DishDto>>(dishes);
            return Response<List<DishDto>>.Success(response);
        }
    }
}
