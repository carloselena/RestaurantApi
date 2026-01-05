using AutoMapper;
using RestaurantApi.Core.Application.DTOs.Ingredient;
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
        }
    }
}
