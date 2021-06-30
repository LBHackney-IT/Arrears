using AutoFixture;
using ArrearsApi.Tests.V1.Helper;
using ArrearsApi.V1.Domain;
using ArrearsApi.V1.Gateways;
using FluentAssertions;
using NUnit.Framework;
using System;

namespace ArrearsApi.Tests.V1.Gateways
{
    //TODO: Remove this file if Postgres gateway is not being used
    //TODO: Rename Tests to match gateway name
    //For instruction on how to run tests please see the wiki: https://github.com/LBHackney-IT/lbh-base-api/wiki/Running-the-test-suite.
    [TestFixture]
    public class ExampleGatewayTests : DatabaseTests
    {
        private readonly Fixture _fixture = new Fixture();
        private ArrearsApiGateway _classUnderTest;

        [SetUp]
        public void Setup()
        {
            _classUnderTest = new ArrearsApiGateway(DatabaseContext);
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
            var databaseEntity = DatabaseEntityHelper.CreateDatabaseEntityFrom(entity);

            DatabaseContext.Arrears.Add(databaseEntity);
            DatabaseContext.SaveChanges();

            var response = _classUnderTest.GetEntityByIdAsync(databaseEntity.Id);

            databaseEntity.Id.Should().Be(response.Result.Id);
            databaseEntity.CreatedAt.Should().BeSameDateAs(response.Result.CreatedAt);
        }

        //TODO: Add tests here for the get all method.
    }
}
