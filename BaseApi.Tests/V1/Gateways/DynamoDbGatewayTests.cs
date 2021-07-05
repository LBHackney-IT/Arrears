using Amazon.DynamoDBv2.DataModel;
using AutoFixture;
using ArrearsApi.Tests.V1.Helper;
using ArrearsApi.V1.Domain;
using ArrearsApi.V1.Gateways;
using ArrearsApi.V1.Infrastructure;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace ArrearsApi.Tests.V1.Gateways
{
    //TODO: Remove this file if DynamoDb gateway not being used
    //TODO: Rename Tests to match gateway name
    //For instruction on how to run tests please see the wiki: https://github.com/LBHackney-IT/lbh-base-api/wiki/Running-the-test-suite.
    [TestFixture]
    public class DynamoDbGatewayTests
    {
        private readonly Fixture _fixture = new Fixture();
        private Mock<IDynamoDBContext> _dynamoDb;
        private Mock<DynamoDbContextWrapper> _wrapper;
        private DynamoDbGateway _gateway;

        [SetUp]
        public void Setup()
        {
            _dynamoDb = new Mock<IDynamoDBContext>();
            _wrapper = new Mock<DynamoDbContextWrapper>();
            _gateway = new DynamoDbGateway(_dynamoDb.Object, _wrapper.Object);
        }

        [Test]
        public async Task GetEntityByIdReturnsNullIfEntityDoesntExist()
        {
            _wrapper.Setup(_ => _.LoadAsync(
                It.IsAny<IDynamoDBContext>(),
                It.IsAny<Guid>(),
                It.IsAny<DynamoDBOperationConfig>()))
                .ReturnsAsync(new ArrearsDbEntity());

            var assetSummary = await _gateway.GetEntityByIdAsync(new Guid("0b4f7df6-2749-420d-bdd1-ee65b8ed0032"))
                .ConfigureAwait(false);

            var response = _gateway.GetEntityByIdAsync(Guid.NewGuid());

            response.Result.AssetAddress.Should().BeNull();
            response.Result.Person.Should().BeNull();
        }

        [Test]
        public void GetEntityByIdReturnsTheEntityIfItExists()
        {
            var entity = _fixture.Create<Arrears>();
            var dbEntity = DatabaseEntityHelper.CreateDatabaseEntityFrom(entity);

            _wrapper.Setup(_ => _.LoadAsync(
               It.IsAny<IDynamoDBContext>(),
               It.IsAny<Guid>(),
               It.IsAny<DynamoDBOperationConfig>()))
               .ReturnsAsync(dbEntity);

            //_wrapper.Setup(x => x.LoadAsync<ArrearsDbEntity>(entity.Id, default))
            //         .ReturnsAsync(dbEntity);

            var response = _gateway.GetEntityByIdAsync(entity.Id);

            //_dynamoDb.Verify(x => x.LoadAsync<ArrearsDbEntity>(entity.Id, default), Times.Once);

            entity.Id.Should().Be(response.Result.Id);
            entity.TargetId.Should().Be(response.Result.TargetId);
            entity.TotalCharged.Should().Be(response.Result.TotalCharged);
            entity.TotalPaid.Should().Be(response.Result.TotalPaid);
            entity.CurrentBalance.Should().Be(response.Result.CurrentBalance);
            entity.CreatedAt.Should().BeSameDateAs(response.Result.CreatedAt);
            entity.Person.Title.Should().Be(response.Result.Person.Title);
            entity.Person.FirstName.Should().Be(response.Result.Person.FirstName);
            entity.Person.LastName.Should().Be(response.Result.Person.LastName);
            entity.AssetAddress.AddressLine1.Should().Be(response.Result.AssetAddress.AddressLine1);
            entity.AssetAddress.AddressLine2.Should().Be(response.Result.AssetAddress.AddressLine2);
            entity.AssetAddress.AddressLine3.Should().Be(response.Result.AssetAddress.AddressLine3);
            entity.AssetAddress.AddressLine4.Should().Be(response.Result.AssetAddress.AddressLine4);
            entity.AssetAddress.PostCode.Should().Be(response.Result.AssetAddress.PostCode);
        }

        [Test]
        public async Task GetAllArerarsReturnsList()
        {
            var firstEntity = _fixture.Create<ArrearsDbEntity>();
            var secondEntity = _fixture.Create<ArrearsDbEntity>();

            _wrapper.Setup(_ => _.ScanAsync(
                It.IsAny<IDynamoDBContext>(),
                It.IsAny<IEnumerable<ScanCondition>>(),
                It.IsAny<DynamoDBOperationConfig>()))
                .ReturnsAsync(new List<ArrearsDbEntity>()
                {
                    firstEntity,
                    secondEntity
                });

            var assetSummaries = await _gateway.GetAllAsync("tenure",4)
                .ConfigureAwait(false);

            assetSummaries.Should().HaveCount(2);
            var entity = assetSummaries[0];
            entity.Id.Should().Be(firstEntity.Id);
            entity.TargetId.Should().Be(firstEntity.TargetId);
            entity.TotalCharged.Should().Be(firstEntity.TotalCharged);
            entity.TotalPaid.Should().Be(firstEntity.TotalPaid);
            entity.CurrentBalance.Should().Be(firstEntity.CurrentBalance);
            entity.CreatedAt.Should().BeSameDateAs(firstEntity.CreatedAt);
            entity.Person.Title.Should().Be(firstEntity.Person.Title);
            entity.Person.FirstName.Should().Be(firstEntity.Person.FirstName);
            entity.Person.LastName.Should().Be(firstEntity.Person.LastName);
            entity.AssetAddress.AddressLine1.Should().Be(firstEntity.AssetAddress.AddressLine1);
            entity.AssetAddress.AddressLine2.Should().Be(firstEntity.AssetAddress.AddressLine2);
            entity.AssetAddress.AddressLine3.Should().Be(firstEntity.AssetAddress.AddressLine3);
            entity.AssetAddress.AddressLine4.Should().Be(firstEntity.AssetAddress.AddressLine4);
            entity.AssetAddress.PostCode.Should().Be(firstEntity.AssetAddress.PostCode);

            var entity2 = assetSummaries[1];
            entity2.Id.Should().Be(secondEntity.Id);
            entity2.TargetId.Should().Be(secondEntity.TargetId);
            entity2.TotalCharged.Should().Be(secondEntity.TotalCharged);
            entity2.TotalPaid.Should().Be(secondEntity.TotalPaid);
            entity2.CurrentBalance.Should().Be(secondEntity.CurrentBalance);
            entity2.CreatedAt.Should().BeSameDateAs(secondEntity.CreatedAt);
            entity2.Person.Title.Should().Be(secondEntity.Person.Title);
            entity2.Person.FirstName.Should().Be(secondEntity.Person.FirstName);
            entity2.Person.LastName.Should().Be(secondEntity.Person.LastName);
            entity2.AssetAddress.AddressLine1.Should().Be(secondEntity.AssetAddress.AddressLine1);
            entity2.AssetAddress.AddressLine2.Should().Be(secondEntity.AssetAddress.AddressLine2);
            entity2.AssetAddress.AddressLine3.Should().Be(secondEntity.AssetAddress.AddressLine3);
            entity2.AssetAddress.AddressLine4.Should().Be(secondEntity.AssetAddress.AddressLine4);
            entity2.AssetAddress.PostCode.Should().Be(secondEntity.AssetAddress.PostCode);
        }

        [Test]
        public async Task AddArrearsWithValidObject()
        {
            _dynamoDb.Setup(_ => _.SaveAsync(It.IsAny<ArrearsDbEntity>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            var domain = _fixture.Create<Arrears>();

            await _gateway.AddAsync(domain).ConfigureAwait(false);

            _dynamoDb.Verify(_ => _.SaveAsync(It.IsAny<ArrearsDbEntity>(), default), Times.Once);
        }

        [Test]
        public async Task AddAssetSummaryWithValidObject()
        {
            var entity = _fixture.Create<Arrears>();
            var dbEntity = DatabaseEntityHelper.CreateDatabaseEntityFrom(entity);

            _dynamoDb.Setup(_ => _.SaveAsync(dbEntity, It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);
            _wrapper.Setup(_ => _.LoadAsync(
              It.IsAny<IDynamoDBContext>(),
              It.IsAny<Guid>(),
              It.IsAny<DynamoDBOperationConfig>()))
              .ReturnsAsync(dbEntity);


            var response = await _gateway.AddAsync(entity).ConfigureAwait(false);

            _dynamoDb.Verify(_ => _.SaveAsync(It.IsAny<ArrearsDbEntity>(), default), Times.Once);
        }
    }
}
