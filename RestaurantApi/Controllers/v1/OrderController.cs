using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantApi.Core.Application.DTOs.Order;
using RestaurantApi.Core.Application.Enums;
using RestaurantApi.Core.Application.Features.Orders.Commands.ChangeOrderStatus;
using RestaurantApi.Core.Application.Features.Orders.Commands.CreateOrder;
using RestaurantApi.Core.Application.Features.Orders.Commands.DeleteOrder;
using RestaurantApi.Core.Application.Features.Orders.Commands.UpdateOrder;
using RestaurantApi.Core.Application.Features.Orders.Queries.GetAllOrders;
using RestaurantApi.Core.Application.Features.Orders.Queries.GetOrderById;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace RestaurantApi.Controllers.v1
{
    [Authorize(Roles = nameof(Roles.MESERO))]
    [ApiVersion("1.0")]
    [SwaggerTag("Mantenimiento de órdenes")]
    public class OrderController : BaseApiController
    {

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
            var response = await Mediator.Send(new GetAllOrdersQuery());
            if (response?.Data?.Count == 0)
                return NoContent();

            return Ok(response?.Data);
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
        public async Task<IActionResult> Get([FromRoute] GetOrderByIdQuery query)
        {
            var response = await Mediator.Send(query);
            return Ok(response.Data);
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
        public async Task<IActionResult> Create([FromBody] CreateOrderCommand command)
        {
            var response = await Mediator.Send(command);
            return StatusCode(StatusCodes.Status201Created, response.Data);
        }

        [HttpPut("{id:int:min(1)}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(UpdateOrderDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Actualización de orden",
            Description = "Recibe las propiedades necesarias para actualizar una orden, solo se pueden actualizar los platos de la orden"
        )]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateOrderCommand command)
        {
            var response = await Mediator.Send(command with { Id = id});
            return Ok(response.Data);
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
        public async Task<IActionResult> ChangeStatus([FromRoute] ChangeOrderStatusCommand command)
        {
            var response = await Mediator.Send(command);
            return NoContent();
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
        public async Task<IActionResult> Delete([FromRoute] DeleteOrderCommand command)
        {
            var response = await Mediator.Send(command);
            return NoContent();
        }
    }
}
