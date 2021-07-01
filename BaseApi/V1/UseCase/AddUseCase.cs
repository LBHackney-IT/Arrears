using ArrearsApi.V1.Boundary.Response;
using ArrearsApi.V1.Domain;
using ArrearsApi.V1.Factories;
using ArrearsApi.V1.Gateways;
using ArrearsApi.V1.UseCase.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArrearsApi.V1.UseCase
{
    public class AddUseCase : IAddUseCase
    {
        private readonly IArrearsApiGateway _gateway;
        public AddUseCase(IArrearsApiGateway gateway)
        {
            _gateway = gateway;
        }

        public ArrearsResponseObject Execute(Arrears arrears)
        {
            _gateway.Add(arrears);
            return arrears.ToResponse();
        }

        public async Task<ArrearsResponseObject> ExecuteAsync(Arrears arrears)
        {
            var result = await _gateway.AddAsync(arrears).ConfigureAwait(false);
            if (result)
            {
                return arrears.ToResponse();
            }
            return null;
        }
    }
}
