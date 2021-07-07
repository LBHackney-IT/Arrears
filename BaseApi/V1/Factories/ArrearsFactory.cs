using ArrearsApi.V1.Domain;
using ArrearsApi.V1.Infrastructure;

namespace ArrearsApi.V1.Factories
{
    public static class ArrearsFactory
    {
        public static Arrears ToDomain(this ArrearsDbEntity databaseEntity)
        {
            return new Arrears
            {
                Id =  databaseEntity.Id,
                CreatedAt = databaseEntity.CreatedAt,
                CurrentBalance = databaseEntity.CurrentBalance,
                TargetId = databaseEntity.TargetId,
                TargetType = databaseEntity.TargetType,
                TotalCharged = databaseEntity.TotalCharged,
                TotalPaid = databaseEntity.TotalPaid,
                AssetAddress = databaseEntity.AssetAddress,
                Person = databaseEntity.Person
            };
        }

        public static ArrearsDbEntity ToDatabase(this Arrears entity)
        {
            return new ArrearsDbEntity
            {
                Id = entity.Id,
                CreatedAt = entity.CreatedAt,
                CurrentBalance = entity.CurrentBalance,
                TargetId = entity.TargetId,
                TargetType= entity.TargetType,
                TotalCharged = entity.TotalCharged,
                TotalPaid = entity.TotalPaid,
                AssetAddress = entity.AssetAddress,
                Person = entity.Person
            };
        }
    }
}
