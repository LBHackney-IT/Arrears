using ArrearsApi.V1.Domain;
using ArrearsApi.V1.Infrastructure;

namespace ArrearsApi.V1.Factories
{
    public static class ArrearsFactory
    {
        public static Arrears ToDomain(this ArrearsDbEntity databaseEntity)
        {
            //TODO: Map the rest of the fields in the domain object.
            // More information on this can be found here https://github.com/LBHackney-IT/lbh-base-api/wiki/Factory-object-mappings

            return new Arrears
            {
                Id =  databaseEntity.Id,
                CreatedAt = databaseEntity.CreatedAt,
                CurrentBalance = databaseEntity.CurrentBalance,
                TargetId = databaseEntity.TargetId,
                TargetType = databaseEntity.TargetType,
                TotalCharged = databaseEntity.TotalCharged,
                TotalPaid = databaseEntity.TotalPaid
            };
        }

        public static ArrearsDbEntity ToDatabase(this Arrears entity)
        {
            //TODO: Map the rest of the fields in the database object.

            return new ArrearsDbEntity
            {
                Id = entity.Id,
                CreatedAt = entity.CreatedAt,
                CurrentBalance = entity.CurrentBalance,
                TargetId = entity.TargetId,
                TargetType= entity.TargetType,
                TotalCharged = entity.TotalCharged,
                TotalPaid = entity.TotalPaid
            };
        }
    }
}
