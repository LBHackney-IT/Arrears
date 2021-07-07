using ArrearsApi.V1.Boundary.Response;
using ArrearsApi.V1.Factories;
using ArrearsApi.V1.Gateways;
using ArrearsApi.V1.UseCase.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace ArrearsApi.V1.UseCase
{
    public class GetAllUseCase : IGetAllUseCase
    {
        private readonly IArrearsApiGateway _gateway;
        public GetAllUseCase(IArrearsApiGateway gateway)
        {
            _gateway = gateway;
        }

        public async Task<ArrearsResponseObjectList> ExecuteAsync(string targettype, int count)
        {
            var arrearsResponseObjectList = new ArrearsResponseObjectList();
            var data = await _gateway.GetAllAsync(targettype, count).ConfigureAwait(false);

            arrearsResponseObjectList.ArrearsResponseObjects = data?.Select(p => p.ToResponse()).ToList();

            return arrearsResponseObjectList;
           
        }
    }
}
