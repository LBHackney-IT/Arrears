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
    /// <summary>
    /// Only for Unit testing purpose
    /// </summary>
    public class DynamoDbContextWrapper
    {
        public virtual Task<List<ArrearsDbEntity>> ScanAsync(IDynamoDBContext context, IEnumerable<ScanCondition> conditions, DynamoDBOperationConfig operationConfig = null)
        {
            return context.ScanAsync<ArrearsDbEntity>(conditions, operationConfig).GetRemainingAsync();
        }
        public virtual Task<ArrearsDbEntity> LoadAsync(IDynamoDBContext context, Guid Id, DynamoDBOperationConfig operationConfig = null)
        {
            return context.LoadAsync<ArrearsDbEntity>(Id);
        }
    }

    public class DynamoDbGateway : IArrearsApiGateway
    {
        private readonly DynamoDbContextWrapper _wrapper;
        private readonly IDynamoDBContext _dynamoDbContext;

        public DynamoDbGateway(IDynamoDBContext dynamoDbContext, DynamoDbContextWrapper wrapper)
        {
            _dynamoDbContext = dynamoDbContext;
            _wrapper = wrapper;
        }

        public async Task<List<Arrears>> GetAllAsync(string targettype, int count)
        {
            var scanConditions = new List<ScanCondition>();
            TargetType targetTypeVal;
            if (!string.IsNullOrEmpty(targettype) &&  Enum.TryParse(targettype.ToLower(), out targetTypeVal))
            {
                scanConditions.Add(new ScanCondition("TargetType", Amazon.DynamoDBv2.DocumentModel.ScanOperator.Equal,
                    targetTypeVal));
            }
            
            var data = await _wrapper.ScanAsync(_dynamoDbContext, scanConditions).ConfigureAwait(false);
            return data.Select(p => p.ToDomain()).OrderByDescending(x => x.CurrentBalance).Take(count).ToList();
        }

        public async Task<Arrears> GetEntityByIdAsync(Guid id)
        {
            var result =await _wrapper.LoadAsync(_dynamoDbContext, id).ConfigureAwait(false);
            return result?.ToDomain();
        }
        public bool Add(Arrears arrears)
        {
            _dynamoDbContext.SaveAsync<ArrearsDbEntity>(arrears.ToDatabase());
            var result = _dynamoDbContext.LoadAsync<ArrearsDbEntity>(arrears.Id);
            if (result != null)
            {
                return true;
            }
            return false;
        }
        public async Task<bool> AddAsync(Arrears arrears)
        {
            await _dynamoDbContext.SaveAsync<ArrearsDbEntity>(arrears.ToDatabase()).ConfigureAwait(false);
            var result = await _wrapper.LoadAsync(_dynamoDbContext, arrears.Id).ConfigureAwait(false);
            if (result != null)
            {
                return true;
            }
            return false;
        }
        
    }
}
