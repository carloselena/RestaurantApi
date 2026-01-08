using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantApi.Core.Application.DTOs.Ingredient;
using RestaurantApi.Core.Application.Enums;
using RestaurantApi.Core.Application.Features.Ingredients.Commands.CreateIngredient;
using RestaurantApi.Core.Application.Features.Ingredients.Commands.UpdateIngredient;
using RestaurantApi.Core.Application.Features.Ingredients.Queries;
using RestaurantApi.Core.Application.Features.Ingredients.Queries.GetAllIngredients;
using RestaurantApi.Core.Application.Features.Ingredients.Queries.GetIngredientById;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace RestaurantApi.Controllers.v1
{
    [Authorize(Roles = nameof(Roles.ADMIN))]
    [ApiVersion("1.0")]
    [SwaggerTag("Mantenimiento de ingredientes")]
    public class IngredientController : BaseApiController
    {

        [HttpGet]
        [ProducesResponseType(typeof(IngredientDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Listado de ingredientes",
            Description = "Obtiene el listado de todos los ingredientes en formato json"
        )]
        public async Task<IActionResult> Get()
        {
            var response = await Mediator.Send(new GetAllIngredientsQuery());
            if (!response.Succeeded)
                return NoContent();

            return Ok(response.Data);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(IngredientDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Buscar ingrediente",
            Description = "Obtiene el ingrediente cuyo id corresponda con el id enviado"
        )]
        public async Task<IActionResult> Get([FromRoute] GetIngredientByIdQuery query)
        {
            var response = await Mediator.Send(query);
            if (!response.Succeeded)
                return NotFound(response.Message);

            return Ok(response.Data);
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(SaveIngredientDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Creación de ingrediente",
            Description = "Recibe las propiedades necesarias para crear un ingrediente"
        )]
        public async Task<IActionResult> Create([FromBody] CreateIngredientCommand command)
        {
            var response = await Mediator.Send(command);
            return StatusCode(StatusCodes.Status201Created, response.Data);
        }

        [HttpPut]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(SaveIngredientDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Actualización de ingrediente",
            Description = "Recibe las propiedades necesarias para actualizar un ingrediente"
        )]
        public async Task<IActionResult> Update([FromBody] UpdateIngredientCommand command)
        {
            var response = await Mediator.Send(command);
            if (!response.Succeeded)
                return NotFound(response.Message);

            return Ok(response.Data);
        }
    }
}
