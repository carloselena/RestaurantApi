using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantApi.Core.Application.DTOs.Order;
using RestaurantApi.Core.Application.Enums;
using RestaurantApi.Core.Application.Interfaces.Services;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace RestaurantApi.Controllers.v1
{
    [Authorize(Roles = nameof(Roles.MESERO))]
    [ApiVersion("1.0")]
    [SwaggerTag("Mantenimiento de órdenes")]
    public class OrderController : BaseApiController
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(OrderDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Listado de órdenes",
            Description = "Obtiene el listado de todas las órdenes en formato json. Las órdenes vienen con los platos de cada una"
        )]
        public async Task<IActionResult> Get()
        {
            var orders = await _orderService.GetAll(q => q.Include(o => o.Dishes)
                                                          .ThenInclude(od => od.Dish));

            if (orders == null || orders.Count == 0)
                return NoContent();

            return Ok(orders);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(OrderDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Buscar orden",
            Description = "Obtiene la orden cuyo id corresponda al id enviado, esta viene con sus platos"
        )]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            if (id <= 0)
                return BadRequest();

            var order = await _orderService.GetById(id, q => q.Include(o => o.Dishes)
                                                              .ThenInclude(od => od.Dish));

            if (order == null)
                return NotFound();

            return Ok(order);
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(AddOrderDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Creación de orden",
            Description = "Recibe las propiedades necesarias para crear una orden, esta se crea con estado EN_PROCESO"
        )]
        public async Task<IActionResult> Create([FromBody] AddOrderDTO addOrderDTO)
        {
            try
            {
                var result = await _orderService.Add(addOrderDTO);
                return StatusCode(StatusCodes.Status201Created, result);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPut("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(UpdateOrderDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Actualización de orden",
            Description = "Recibe las propiedades necesarias para actualizar una orden, solo se pueden actualizar los platos de la orden"
        )]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateOrderDTO updateOrderDTO)
        {
            if (id <= 0)
                return BadRequest();

            try
            {
                return Ok(await _orderService.Update(id, updateOrderDTO));
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPatch("{id}/status")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Cambiar estado de una orden",
            Description = "Recibe el id de la orden cuyo estado se va a actualizar y la actualiza a estado COMPLETADA"
        )]
        public async Task<IActionResult> ChangeStatus([FromRoute] int id)
        {
            if (id <= 0)
                return BadRequest();

            try
            {
                await _orderService.ChangeStatus(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Eliminar una orden",
            Description = "Recibe el id de la orden y la elimina"
        )]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (id <= 0)
                return BadRequest();

            try
            {
                await _orderService.Delete(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
