using ArrearsApi.V1.Boundary.Response;
using ArrearsApi.V1.Factories;
using ArrearsApi.V1.Gateways;
using ArrearsApi.V1.UseCase.Interfaces;
using System;
using System.Threading.Tasks;

namespace ArrearsApi.V1.UseCase
{
    //TODO: Rename class name and interface name to reflect the entity they are representing eg. GetClaimantByIdUseCase
    public class GetByIdUseCase : IGetByIdUseCase
    {
        private IArrearsApiGateway _gateway;
        public GetByIdUseCase(IArrearsApiGateway gateway)
        {
            _gateway = gateway;
        }

        //TODO: rename id to the name of the identifier that will be used for this API, the type may also need to change
        public async Task<ArrearsResponseObject> ExecuteAsync(Guid id)
        {
            var arrear = await _gateway.GetEntityByIdAsync(id).ConfigureAwait(false);
            return arrear?.ToResponse();
        }
    }
}
