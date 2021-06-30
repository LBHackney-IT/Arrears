using Amazon.DynamoDBv2.DataModel;
using ArrearsApi.V1.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArrearsApi.V1.Infrastructure
{
    //TODO: rename table and add needed fields relating to the table columns.
    // There's an example of this in the wiki https://github.com/LBHackney-IT/lbh-base-api/wiki/DatabaseContext

    //TODO: Pick the attributes for the required data source, delete the others as appropriate
    // Postgres will use the "Table" and "Column" attributes
    // DynamoDB will use the "DynamoDBTable", "DynamoDBHashKey" and "DynamoDBProperty" attributes

    //TODO: For DynamoDB...
    // * The table name must match that specified in the terraform step that provisions the DynamoDb resource
    // * The name of the hash key property must match that specified in the terraform step that provisions the DynamoDb resource
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
    }
}
