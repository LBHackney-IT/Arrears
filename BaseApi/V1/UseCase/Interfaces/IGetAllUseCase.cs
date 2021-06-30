using ArrearsApi.V1.Boundary.Response;
using System.Threading.Tasks;

namespace ArrearsApi.V1.UseCase.Interfaces
{
    public interface IGetAllUseCase
    {
        Task<ArrearsResponseObjectList> ExecuteAsync(string targettype, int count);
    }
}
