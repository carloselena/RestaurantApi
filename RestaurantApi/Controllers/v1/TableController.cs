using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantApi.Core.Application.DTOs.Order;
using RestaurantApi.Core.Application.DTOs.Table;
using RestaurantApi.Core.Application.Enums;
using RestaurantApi.Core.Application.Features.Orders.Queries.GetAllTableOrders;
using RestaurantApi.Core.Application.Features.Tables.Commands.ChangeTableStatus;
using RestaurantApi.Core.Application.Features.Tables.Commands.CreateTable;
using RestaurantApi.Core.Application.Features.Tables.Commands.UpdateTable;
using RestaurantApi.Core.Application.Features.Tables.Queries.GetAllTables;
using RestaurantApi.Core.Application.Features.Tables.Queries.GetTableById;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace RestaurantApi.Controllers.v1
{
    [ApiVersion("1.0")]
    [SwaggerTag("Mantenimiento de mesas")]
    public class TableController : BaseApiController
    {

        [Authorize(Roles = $"{nameof(Roles.ADMIN)}, {nameof(Roles.MESERO)}")]
        [HttpGet]
        [ProducesResponseType(typeof(TableDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Listado de mesas",
            Description = "Obtiene el listado de todas las mesas en formato json"
        )]
        public async Task<IActionResult> Get()
        {
            var response = await Mediator.Send(new GetAllTablesQuery());
            if (response?.Data?.Count == 0)
                return NoContent();

            return Ok(response?.Data);
        }

        [Authorize(Roles = $"{nameof(Roles.ADMIN)}, {nameof(Roles.MESERO)}")]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(TableDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Buscar mesa",
            Description = "Obtiene la mesa cuyo id corresponda al id enviado"
        )]
        public async Task<IActionResult> Get([FromRoute] GetTableByIdQuery query)
        {
            var response = await Mediator.Send(query);
            return Ok(response.Data);
        }

        [Authorize(Roles = nameof(Roles.ADMIN))]
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(AddTableDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Creación de mesa",
            Description = "Recibe las propiedades necesarias para crear una mesa, las mesas se crean con estado DISPONIBLE"
        )]
        public async Task<IActionResult> Create([FromBody] CreateTableCommand command)
        {
            var response = await Mediator.Send(command);
            return StatusCode(StatusCodes.Status201Created, response.Data);
        }

        [Authorize(Roles = nameof(Roles.ADMIN))]
        [HttpPut("{id:int:min(1)}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(UpdateTableDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Actualización de mesa",
            Description = "Recibe las propiedades necesarias para actualizar un ingrediente, solo se puede actualizar la descripción y la cantidad de personas para la que alcanza la mesa"
        )]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateTableCommand command)
        {
            var response = await Mediator.Send(command with { Id = id});
            return Ok(response.Data);
        }

        [Authorize(Roles = nameof(Roles.MESERO))]
        [HttpPatch("{id:int:min(1)}/status")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Cambiar estado de una mesa",
            Description = "Recibe el nuevo estado de la mesa y lo actualiza a este, los estados solo pueden ser DISPONIBLE, EN_PROCESO o ATENDIDA"
        )]
        public async Task<IActionResult> ChangeStatus([FromRoute] int id,
            [FromBody] ChangeTableStatusCommand command)
        {
            var response = await Mediator.Send(command with { Id = id});
            return Ok(response.Data);
        }
        
        [Authorize(Roles = nameof(Roles.MESERO))]
        [HttpGet("{tableId}/orders")]
        [ProducesResponseType(typeof(TableOrdersDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Buscar órdenes de una mesa",
            Description = "Recibe el id de la mesa y devuelve un objeto con el id de la mesa y el listado de sus órdenes pendientes"
        )]
        public async Task<IActionResult> GetTableOrders([FromRoute] GetAllTableOrdersQuery query)
        {
            var response = await Mediator.Send(query);
            return Ok(response.Data);
        }
    }
}
