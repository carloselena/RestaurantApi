using AutoMapper;
using Restaurant.Core.Application.Features.Orders.Queries;
using RestaurantApi.Core.Application.Enums;
using RestaurantApi.Core.Application.Features.Dishes.Commands.CreateDish;
using RestaurantApi.Core.Application.Features.Dishes.Queries;
using RestaurantApi.Core.Application.Features.Ingredients.Commands;
using RestaurantApi.Core.Application.Features.Ingredients.Commands.CreateIngredient;
using RestaurantApi.Core.Application.Features.Ingredients.Commands.UpdateIngredient;
using RestaurantApi.Core.Application.Features.Ingredients.Queries;
using RestaurantApi.Core.Application.Features.Orders.Commands.CreateOrder;
using RestaurantApi.Core.Application.Features.Orders.Commands.UpdateOrder;
using RestaurantApi.Core.Application.Features.Tables.Commands.ChangeTableStatus;
using RestaurantApi.Core.Application.Features.Tables.Commands.CreateTable;
using RestaurantApi.Core.Application.Features.Tables.Commands.UpdateTable;
using RestaurantApi.Core.Application.Features.Tables.Queries;
using RestaurantApi.Core.Domain.Entities;

namespace RestaurantApi.Core.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            #region CQRS

            #region Ingredient
            CreateMap<CreateIngredientCommand, Ingredient>();

            CreateMap<UpdateIngredientCommand, Ingredient>();

            CreateMap<Ingredient, SaveIngredientResponse>();

            CreateMap<Ingredient, IngredientDto>();
            #endregion

            #region Dish
            CreateMap<CreateDishCommand, Dish>()
                .ForMember(d => d.Category, opt => opt.MapFrom(cmd => cmd.Category.ToString()))
                .ForMember(d => d.MaxPeopleQuantity, opt => opt.MapFrom(cmd => cmd.EnoughFor))
                .ForMember(d => d.Ingredients,
                           opt => opt.MapFrom(cmd =>
                           cmd.IngredientsIds.Select(id =>
                           new DishIngredients { IngredientId = id })));

            CreateMap<Dish, DishDto>()
                .ForMember(dto => dto.Category, opt => opt.MapFrom(src => Enum.Parse<DishCategories>(src.Category)))
                .ForMember(dto => dto.EnoughFor, opt => opt.MapFrom(src => src.MaxPeopleQuantity))
                .ForMember(dto => dto.Ingredients, opt => opt.MapFrom(src => src.Ingredients.Select(di => di.Ingredient)));
            #endregion

            #region Table
            CreateMap<CreateTableCommand, Table>()
                .ForMember(t => t.Status, opt => opt.MapFrom(_ => TableStatus.DISPONIBLE.ToString()));

            CreateMap<UpdateTableCommand, Table>();

            CreateMap<ChangeTableStatusCommand, Table>();

            CreateMap<Table, TableDto>()
                .ForMember(dto => dto.Status, opt => opt.MapFrom(src => Enum.Parse<TableStatus>(src.Status)));
            #endregion

            #region Order
            CreateMap<CreateOrderCommand, Order>()
                .ForMember(o => o.Status, opt => opt.MapFrom(_ => OrderStatus.EN_PROCESO.ToString()))
                .ForMember(o => o.Dishes,
                           opt => opt.MapFrom(cmd =>
                           cmd.DishesIds.Select(id =>
                           new OrderDishes { DishId = id})));

            CreateMap<UpdateOrderCommand, Order>()
                .ForMember(o => o.Dishes,
                            opt => opt.MapFrom(cmd =>
                            cmd.DishesIds.Select(id =>
                            new OrderDishes { DishId = id })));

            CreateMap<Order, OrderDto>()
                .ForMember(dto => dto.Status, opt => opt.MapFrom(src => Enum.Parse<OrderStatus>(src.Status)))
                .ForMember(dto => dto.Dishes, opt => opt.MapFrom(src => src.Dishes.Select(od => od.Dish)));
            #endregion

            #endregion

        }
    }
}
