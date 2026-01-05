using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using RestaurantApi.Core.Application.DTOs.Ingredient;
using RestaurantApi.Core.Application.Interfaces.Services;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace RestaurantApi.Controllers.v1
{
    [ApiVersion("1.0")]
    [SwaggerTag("Mantenimiento de ingredientes")]
    public class IngredientController : BaseApiController
    {
        private readonly IIngredientService _ingredientService;
        
        public IngredientController(IIngredientService ingredientService)
        {
            _ingredientService = ingredientService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IngredientDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Listado de ingredientes",
            Description = "Obtiene el listado de todos los ingredientes en formato json"
        )]
        public async Task<IActionResult> Get()
        {
            var ingredients = await _ingredientService.GetAll();
            if (ingredients == null || ingredients.Count == 0)
                return NoContent();

            return Ok(ingredients);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(IngredientDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Buscar ingrediente",
            Description = "Obtiene el ingrediente cuyo id corresponda con el id enviado"
        )]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            if (id <= 0)
                return BadRequest();

            var ingredient = await _ingredientService.GetById(id);
            if (ingredient == null)
                return NotFound();

            return Ok(ingredient);
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
        public async Task<IActionResult> Create([FromBody] SaveIngredientDTO ingredientDTO)
        {
            var result = await _ingredientService.Add(ingredientDTO);
            return StatusCode(StatusCodes.Status201Created, result);
        }

        [HttpPut("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(SaveIngredientDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Actualización de ingrediente",
            Description = "Recibe las propiedades necesarias para actualizar un ingrediente"
        )]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] SaveIngredientDTO ingredientDTO)
        {
            if (id <= 0)
                return BadRequest();

            try
            {
                return Ok(await _ingredientService.Update(id, ingredientDTO));
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
