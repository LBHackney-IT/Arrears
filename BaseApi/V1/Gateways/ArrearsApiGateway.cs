using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArrearsApi.V1.Domain;
using ArrearsApi.V1.Factories;
using ArrearsApi.V1.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace ArrearsApi.V1.Gateways
{
    //TODO: Rename to match the data source that is being accessed in the gateway eg. MosaicGateway
    public class ArrearsApiGateway : IArrearsApiGateway
    {
        private readonly ArrearsContext _arrearsContext;

        public ArrearsApiGateway(ArrearsContext databaseContext)
        {
            _arrearsContext = databaseContext;
        }

        public async Task<Arrears> GetEntityByIdAsync(Guid id)
        {
            var result =await _arrearsContext.Arrears.FindAsync(id).ConfigureAwait(false);

            return result?.ToDomain();
        }

        public async Task<List<Arrears>> GetAllAsync(string targettype, int count)
        {
            TargetType targetType;
            if (Enum.TryParse(targettype, out targetType))
            {
                IQueryable<ArrearsDbEntity> data = _arrearsContext.Arrears.Where(x => x.TargetType == targetType).OrderByDescending(x => x.CurrentBalance).Take(count);
                return await data.Select(s => s.ToDomain()).ToListAsync().ConfigureAwait(false);
            }
            else
                throw new ArgumentException("Invalid type");

        }
        public void Add(Arrears arrears)
        {
             _arrearsContext.Arrears.Add(arrears.ToDatabase());
        }
        public async Task AddAsync(Arrears arrears)
        {
            await _arrearsContext.AddAsync(arrears).ConfigureAwait(false);
        }
    }
}
