using AutoFixture;
using ArrearsApi.V1.Domain;
using ArrearsApi.V1.Factories;
using ArrearsApi.V1.Infrastructure;
using FluentAssertions;
using NUnit.Framework;

namespace ArrearsApi.Tests.V1.Factories
{
    [TestFixture]
    public class ArrearsFactoryTest
    {
        private readonly Fixture _fixture = new Fixture();

        //TODO: add assertions for all the fields being mapped in `EntityFactory.ToDomain()`. Also be sure to add test cases for
        // any edge cases that might exist.
        [Test]
        public void CanMapADatabaseEntityToADomainObject()
        {
            var databaseEntity = _fixture.Create<ArrearsDbEntity>();
            var entity = databaseEntity.ToDomain();

            databaseEntity.Id.Should().Be(entity.Id);
            databaseEntity.CreatedAt.Should().BeSameDateAs(entity.CreatedAt);
            databaseEntity.TargetType.Should().Be(entity.TargetType);
            databaseEntity.TotalCharged.Should().Be(entity.TotalCharged);
            databaseEntity.TotalPaid.Should().Be(entity.TotalPaid);
            databaseEntity.CurrentBalance.Should().Be(entity.CurrentBalance);
            databaseEntity.AssetAddress.AddressLine1.Should().Be(entity.AssetAddress.AddressLine1);
            databaseEntity.AssetAddress.AddressLine2.Should().Be(entity.AssetAddress.AddressLine2);
            databaseEntity.AssetAddress.AddressLine3.Should().Be(entity.AssetAddress.AddressLine3);
            databaseEntity.AssetAddress.AddressLine4.Should().Be(entity.AssetAddress.AddressLine4);
            databaseEntity.AssetAddress.PostCode.Should().Be(entity.AssetAddress.PostCode);
            databaseEntity.Person.Title.Should().Be(entity.Person.Title);
            databaseEntity.Person.FirstName.Should().Be(entity.Person.FirstName);
            databaseEntity.Person.LastName.Should().Be(entity.Person.LastName);
            
        }
        

        //TODO: add assertions for all the fields being mapped in `EntityFactory.ToDatabase()`. Also be sure to add test cases for
        // any edge cases that might exist.
        [Test]
        public void CanMapADomainEntityToADatabaseObject()
        {
            var entity = _fixture.Create<Arrears>();
            var databaseEntity = entity.ToDatabase();

            entity.Id.Should().Be(databaseEntity.Id);
            entity.CreatedAt.Should().BeSameDateAs(databaseEntity.CreatedAt);
            entity.TargetType.Should().Be(databaseEntity.TargetType);
            entity.TotalCharged.Should().Be(databaseEntity.TotalCharged);
            entity.TotalPaid.Should().Be(databaseEntity.TotalPaid);
            entity.CurrentBalance.Should().Be(databaseEntity.CurrentBalance);
            entity.AssetAddress.AddressLine1.Should().Be(databaseEntity.AssetAddress.AddressLine1);
            entity.AssetAddress.AddressLine2.Should().Be(databaseEntity.AssetAddress.AddressLine2);
            entity.AssetAddress.AddressLine3.Should().Be(databaseEntity.AssetAddress.AddressLine3);
            entity.AssetAddress.AddressLine4.Should().Be(databaseEntity.AssetAddress.AddressLine4);
            entity.AssetAddress.PostCode.Should().Be(databaseEntity.AssetAddress.PostCode);
            entity.Person.Title.Should().Be(databaseEntity.Person.Title);
            entity.Person.FirstName.Should().Be(databaseEntity.Person.FirstName);
            entity.Person.LastName.Should().Be(databaseEntity.Person.LastName);

        }
    }
}
