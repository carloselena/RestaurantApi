using RestaurantApi.Core.Application.DTOs.Table;
using RestaurantApi.Core.Domain.Entities;

namespace RestaurantApi.Core.Application.Interfaces.Services
{
    public interface ITableService : IGenericService<TableDTO, AddTableDTO, UpdateTableDTO, Table>
    {
        Task ChangeStatus(int id, ChangeTableStatusDTO tableStatusDTO);
    }
}
