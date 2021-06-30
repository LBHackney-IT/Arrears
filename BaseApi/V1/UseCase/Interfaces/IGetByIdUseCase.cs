using ArrearsApi.V1.Boundary.Response;
using System;
using System.Threading.Tasks;

namespace ArrearsApi.V1.UseCase.Interfaces
{
    public interface IGetByIdUseCase
    {
        Task<ArrearsResponseObject> ExecuteAsync(Guid id);
    }
}
