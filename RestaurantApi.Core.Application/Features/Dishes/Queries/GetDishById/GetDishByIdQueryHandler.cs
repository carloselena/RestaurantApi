using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RestaurantApi.Core.Application.Interfaces.Repositories;
using RestaurantApi.Core.Application.Wrappers;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace RestaurantApi.Core.Application.Features.Dishes.Queries.GetDishById
{
    public class GetDishByIdQueryHandler : IRequestHandler<GetDishByIdQuery, Response<DishDto>>
    {
        private readonly IDishRepository _dishRepository;
        private readonly IMapper _mapper;

        public GetDishByIdQueryHandler(IDishRepository dishRepository, IMapper mapper)
        {
            _dishRepository = dishRepository;
            _mapper = mapper;
        }

        public async Task<Response<DishDto>> Handle(GetDishByIdQuery request, CancellationToken cancellationToken)
        {
            var dish = await _dishRepository
                              .GetByIdAsync(request.Id, q => q.Include(d => d.Ingredients)
                                                            .ThenInclude(di => di.Ingredient));
            if (dish == null)
                return Response<DishDto>.Fail($"No existe plato con id {request.Id}");

            var response = _mapper.Map<DishDto>(dish);
            return Response<DishDto>.Success(response);
        }
    }
}
