using System.Linq;
using AutoFixture;
using ArrearsApi.V1.Boundary.Response;
using ArrearsApi.V1.Domain;
using ArrearsApi.V1.Factories;
using ArrearsApi.V1.Gateways;
using ArrearsApi.V1.UseCase;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace ArrearsApi.Tests.V1.UseCase
{
    public class GetAllUseCaseTests
    {
        private Mock<IArrearsApiGateway> _mockGateway;
        private GetAllUseCase _classUnderTest;
        private Fixture _fixture;

        [SetUp]
        public void SetUp()
        {
            _mockGateway = new Mock<IArrearsApiGateway>();
            _classUnderTest = new GetAllUseCase(_mockGateway.Object);
            _fixture = new Fixture();
        }

        [Test]
        public void GetsAllFromTheGateway()
        {
            var stubbedEntities = _fixture.CreateMany<Arrears>().ToList();
            _mockGateway.Setup(x => x.GetAllAsync("tenure", 4)).Returns(Task.FromResult(stubbedEntities));

            var expectedResponse = new ArrearsResponseObjectList { ArrearsResponseObjects = stubbedEntities.ToResponse() };

            _classUnderTest.ExecuteAsync("tenure", 4).Should().BeEquivalentTo(expectedResponse);
        }

        //TODO: Add extra tests here for extra functionality added to the use case
    }
}
