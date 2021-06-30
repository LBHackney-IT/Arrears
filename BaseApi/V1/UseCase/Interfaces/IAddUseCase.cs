using ArrearsApi.V1.Boundary.Response;
using ArrearsApi.V1.Domain;
using System.Threading.Tasks;

namespace ArrearsApi.V1.UseCase.Interfaces
{
    public interface IAddUseCase
    {
        public ArrearsResponseObject Execute(Arrears arrears);
        public Task<ArrearsResponseObject> ExecuteAsync(Arrears arrears);
    }
}
