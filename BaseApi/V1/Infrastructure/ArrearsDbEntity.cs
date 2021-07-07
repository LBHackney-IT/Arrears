using Amazon.DynamoDBv2.DataModel;
using ArrearsApi.V1.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArrearsApi.V1.Infrastructure
{
    
    [DynamoDBTable("arrears", LowerCamelCaseProperties = true)]
    public class ArrearsDbEntity
    {
        [DynamoDBHashKey]
        public Guid Id { get; set; }

        [DynamoDBProperty(AttributeName = "target_id")]
        public Guid TargetId { get; set; }

        [DynamoDBProperty(AttributeName = "target_type", Converter = typeof(DynamoDbEnumConverter<TargetType>))]
        public TargetType TargetType { get; set; }

        [DynamoDBProperty(AttributeName = "total_charged")]
        public decimal TotalCharged { get; set; }

        [DynamoDBProperty(AttributeName = "total_paid")]
        public decimal TotalPaid { get; set; }

        [DynamoDBProperty(AttributeName = "current_balance")]
        public decimal CurrentBalance { get; set; }

        [DynamoDBProperty(AttributeName = "created_at")]
        public DateTime CreatedAt { get; set; }

        [DynamoDBProperty(AttributeName = "asset_address", Converter = (typeof(DynamoDbObjectConverter<AssetAddress>)))]
        public AssetAddress AssetAddress { get; set; }

        [DynamoDBProperty(AttributeName = "person_details", Converter = (typeof(DynamoDbObjectConverter<Person>)))]
        public Person Person { get; set; }
    }
}
