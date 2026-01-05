using AutoMapper;
using RestaurantApi.Core.Application.DTOs.Dish;
using RestaurantApi.Core.Application.DTOs.Ingredient;
using RestaurantApi.Core.Application.DTOs.Table;
using RestaurantApi.Core.Application.Enums;
using RestaurantApi.Core.Domain.Entities;

namespace RestaurantApi.Core.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            #region Ingredient
            CreateMap<Ingredient, IngredientDTO>();

            CreateMap<Ingredient, SaveIngredientDTO>()
                .ReverseMap();
            #endregion

            #region Dish
            CreateMap<Dish, DishDTO>()
                .ForMember(dto => dto.Category, opt => opt.MapFrom(src => Enum.Parse<DishCategories>(src.Category)))
                .ForMember(dto => dto.Ingredients, opt => opt.MapFrom(src => src.Ingredients.Select(di => di.Ingredient)));

            CreateMap<Dish, AddDishDTO>()
                .ForMember(dto => dto.Category, opt => opt.MapFrom(src => Enum.Parse<DishCategories>(src.Category)))
                .ForMember(dto => dto.IngredientsIds, opt => opt.MapFrom(src => src.Ingredients.Select(di => di.IngredientId)))
                .ReverseMap()
                .ForMember(d => d.Category, opt => opt.MapFrom(src => src.Category.ToString()))
                .ForMember(d => d.Ingredients, opt => opt.MapFrom(src => src.IngredientsIds.Select(id => new DishIngredients { IngredientId = id })));

            CreateMap<Dish, UpdateDishDTO>()
                .ReverseMap();
            #endregion

            #region Table
            CreateMap<Table, TableDTO>()
                .ForMember(dto => dto.Status, opt => opt.MapFrom(src => Enum.Parse<TableStatus>(src.Status)));

            CreateMap<Table, AddTableDTO>()
                .ForMember(dto => dto.Status, opt => opt.MapFrom(src => Enum.Parse<TableStatus>(src.Status)))
                .ReverseMap()
                .ForMember(t => t.Status, opt => opt.MapFrom(src => src.Status.ToString()));

            CreateMap<Table, UpdateTableDTO>()
                .ReverseMap();

            CreateMap<Table, ChangeTableStatusDTO>()
                .ForMember(dto => dto.Status, opt => opt.MapFrom(src => Enum.Parse<TableStatus>(src.Status)))
                .ReverseMap()
                .ForMember(t => t.Status, opt => opt.MapFrom(src => src.Status.ToString()));
            #endregion
        }
    }
}
