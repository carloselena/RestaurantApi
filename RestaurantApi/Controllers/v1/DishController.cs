using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantApi.Core.Application.DTOs.Dish;
using RestaurantApi.Core.Application.Interfaces.Services;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace RestaurantApi.Controllers.v1
{
    [ApiVersion("1.0")]
    [SwaggerTag("Mantenimiento de platos")]
    public class DishController : BaseApiController
    {
        private readonly IDishService _dishService;

        public DishController(IDishService dishService)
        {
            _dishService = dishService;
        }

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
            var dishes = await _dishService.GetAll(q => q.Include(d => d.Ingredients)
                                                         .ThenInclude(di => di.Ingredient));

            if (dishes == null || dishes.Count == 0)
                return NoContent();

            return Ok(dishes);
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
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            if (id <= 0)
                return BadRequest();

            var dish = await _dishService.GetById(id, q => q.Include(d => d.Ingredients)
                                                         .ThenInclude(di => di.Ingredient));

            if (dish == null)
                return NotFound();

            return Ok(dish);
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
        public async Task<IActionResult> Create([FromBody] AddDishDTO addDishDTO)
        {
            var result = await _dishService.Add(addDishDTO);
            return StatusCode(StatusCodes.Status201Created, result);
        }

        [HttpPut("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(UpdateDishDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Actualización de ingrediente",
            Description = "Recibe las propiedades necesarias para actualizar un ingrediente"
        )]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateDishDTO updateDishDTO)
        {
            if (id <= 0)
                return BadRequest();

            try
            {
                return Ok(await _dishService.Update(id, updateDishDTO));
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
