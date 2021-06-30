using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ArrearsApi.V1.Domain;

namespace ArrearsApi.V1.Gateways
{
    public interface IArrearsApiGateway
    {
        Task<Arrears> GetEntityByIdAsync(Guid id);

        Task<List<Arrears>> GetAllAsync(string assettype, int count);

        void Add(Arrears arrears);

        Task AddAsync(Arrears arrears);
    }
}
