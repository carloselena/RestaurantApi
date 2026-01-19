using MediatR;
using RestaurantApi.Core.Application.Enums;
using RestaurantApi.Core.Application.Wrappers;
using System.Text.Json.Serialization;

namespace RestaurantApi.Core.Application.Features.Tables.Commands.ChangeTableStatus
{
    public class ChangeTableStatusCommand : IRequest<Response<int>>
    {
        [JsonIgnore]
        public int Id { get; set; }
        public TableStatus Status { get; set; }
    }
}
