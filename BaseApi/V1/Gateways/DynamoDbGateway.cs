using Amazon.DynamoDBv2.DataModel;
using ArrearsApi.V1.Domain;
using ArrearsApi.V1.Factories;
using ArrearsApi.V1.Infrastructure;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace ArrearsApi.V1.Gateways
{
    public class DynamoDbGateway : IArrearsApiGateway
    {
        private readonly IDynamoDBContext _dynamoDbContext;

        public DynamoDbGateway(IDynamoDBContext dynamoDbContext)
        {
            _dynamoDbContext = dynamoDbContext;
        }

        public async Task<List<Arrears>> GetAllAsync(string targettype, int count)
        {
            var scanConditions = new List<ScanCondition>();
            if (targettype != null)
                scanConditions.Add(new ScanCondition("TargetType", Amazon.DynamoDBv2.DocumentModel.ScanOperator.Equal, Enum.Parse(typeof(TargetType), targettype.ToLower())));
            
            var data = await _dynamoDbContext.ScanAsync<ArrearsDbEntity>(scanConditions).GetRemainingAsync().ConfigureAwait(false);
            return data.Select(p => p.ToDomain()).OrderByDescending(x => x.CurrentBalance).Take(count).ToList();
        }

        public async Task<Arrears> GetEntityByIdAsync(Guid id)
        {
            var result =await _dynamoDbContext.LoadAsync<ArrearsDbEntity>(id).ConfigureAwait(false);
            return result?.ToDomain();
        }
        public void Add(Arrears arrears)
        {
            _dynamoDbContext.SaveAsync<ArrearsDbEntity>(arrears.ToDatabase());
        }
        public async Task AddAsync(Arrears arrears)
        {
            await _dynamoDbContext.SaveAsync<ArrearsDbEntity>(arrears.ToDatabase()).ConfigureAwait(false);
            var result = _dynamoDbContext.LoadAsync<ArrearsDbEntity>(arrears.Id);
        }
        
    }
}
