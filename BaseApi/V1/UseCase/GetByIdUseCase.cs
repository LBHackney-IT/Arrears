using ArrearsApi.V1.Boundary.Response;
using ArrearsApi.V1.Factories;
using ArrearsApi.V1.Gateways;
using ArrearsApi.V1.UseCase.Interfaces;
using System;
using System.Threading.Tasks;

namespace ArrearsApi.V1.UseCase
{
    public class GetByIdUseCase : IGetByIdUseCase
    {
        private IArrearsApiGateway _gateway;
        public GetByIdUseCase(IArrearsApiGateway gateway)
        {
            _gateway = gateway;
        }

        public async Task<ArrearsResponseObject> ExecuteAsync(Guid id)
        {
            var arrear = await _gateway.GetEntityByIdAsync(id).ConfigureAwait(false);
            return arrear?.ToResponse();
        }
    }
}
