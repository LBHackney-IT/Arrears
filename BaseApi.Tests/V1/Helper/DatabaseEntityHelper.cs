using AutoFixture;
using ArrearsApi.V1.Domain;
using ArrearsApi.V1.Infrastructure;

namespace ArrearsApi.Tests.V1.Helper
{
    public static class DatabaseEntityHelper
    {
        public static ArrearsDbEntity CreateDatabaseEntity()
        {
            var entity = new Fixture().Create<Arrears>();

            return CreateDatabaseEntityFrom(entity);
        }

        public static ArrearsDbEntity CreateDatabaseEntityFrom(Arrears entity)
        {
            return new ArrearsDbEntity
            {
                Id = entity.Id,
                CreatedAt = entity.CreatedAt,
            };
        }
    }
}
