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
        private DynamoDbGateway _classUnderTest;

        [SetUp]
        public void Setup()
        {
            _dynamoDb = new Mock<IDynamoDBContext>();
            _classUnderTest = new DynamoDbGateway(_dynamoDb.Object);
        }

        [Test]
        public void GetEntityByIdReturnsNullIfEntityDoesntExist()
        {
            var response = _classUnderTest.GetEntityByIdAsync(Guid.NewGuid());

            response.Should().BeNull();
        }

        [Test]
        public void GetEntityByIdReturnsTheEntityIfItExists()
        {
            var entity = _fixture.Create<Arrears>();
            var dbEntity = DatabaseEntityHelper.CreateDatabaseEntityFrom(entity);

            _dynamoDb.Setup(x => x.LoadAsync<ArrearsDbEntity>(entity.Id, default))
                     .ReturnsAsync(dbEntity);

            var response = _classUnderTest.GetEntityByIdAsync(entity.Id);

            _dynamoDb.Verify(x => x.LoadAsync<ArrearsDbEntity>(entity.Id, default), Times.Once);

            entity.Id.Should().Be(response.Result.Id);
            entity.CreatedAt.Should().BeSameDateAs(response.Result.CreatedAt);
        }
    }
}
