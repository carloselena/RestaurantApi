using MediatR;
using RestaurantApi.Core.Application.Enums;
using RestaurantApi.Core.Application.Wrappers;
using System.Text.Json.Serialization;

namespace RestaurantApi.Core.Application.Features.Tables.Commands.ChangeTableStatus
{
    public record ChangeTableStatusCommand(
        [property: JsonIgnore] int Id,
        TableStatus Status
    ) : IRequest<Response<int>>;
}
