using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantApi.Core.Application.DTOs.Dish;
using RestaurantApi.Core.Application.Enums;
using RestaurantApi.Core.Application.Features.Dishes.Commands.CreateDish;
using RestaurantApi.Core.Application.Features.Dishes.Commands.UpdateDish;
using RestaurantApi.Core.Application.Features.Dishes.Queries.GetAllDishes;
using RestaurantApi.Core.Application.Features.Dishes.Queries.GetDishById;
using RestaurantApi.Core.Application.Interfaces.Services;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace RestaurantApi.Controllers.v1
{
    [Authorize(Roles = nameof(Roles.ADMIN))]
    [ApiVersion("1.0")]
    [SwaggerTag("Mantenimiento de platos")]
    public class DishController : BaseApiController
    {

        [HttpGet]
        [ProducesResponseType(typeof(DishDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Listado de platos",
            Description = "Obtiene el listado de todos los platos en formato json. Los platos vienen con los ingredientes que incluye cada uno"
        )]
        public async Task<IActionResult> Get()
        {
            var response = await Mediator.Send(new GetAllDishesQuery());
            if (!response.Succeeded)
                return NoContent();

            return Ok(response.Data);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(DishDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Buscar plato",
            Description = "Obtiene el plato cuyo id corresponda con el id enviado, este viene con sus ingredientes"
        )]
        public async Task<IActionResult> Get([FromRoute] GetDishByIdQuery query)
        {
            var response = await Mediator.Send(query);
            if (!response.Succeeded)
                return NotFound();

            return Ok(response.Data);
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(AddDishDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Creación de plato",
            Description = "Recibe las propiedades necesarias para crear un plato. La categoría del plato solo puede ser ENTRADA, PLATO_FUERTE, POSTRE o BEBIDA"
        )]
        public async Task<IActionResult> Create([FromBody] CreateDishCommand createDishCommand)
        {
            var result = await Mediator.Send(createDishCommand);
            if (!result.Succeeded)
                return BadRequest(result.Message);

            return Ok(result.Data);
        }

        [HttpPut]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(UpdateDishDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Actualización de plato",
            Description = "Recibe las propiedades necesarias para actualizar un plato"
        )]
        public async Task<IActionResult> Update([FromBody] UpdateDishCommand command)
        {
            try
            {
                var response = await Mediator.Send(command);
                if (!response.Succeeded)
                    return BadRequest(response.Errors);

                return Ok(response.Data);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
