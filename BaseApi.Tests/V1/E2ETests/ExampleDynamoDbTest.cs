using AutoFixture;
using ArrearsApi;
using ArrearsApi.Tests;
using ArrearsApi.V1.Domain;
using ArrearsApi.V1.Factories;
using ArrearsApi.V1.Infrastructure;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Net;
using System.Threading.Tasks;
using ArrearsApi.V1.Boundary;
using System.Net.Http;
using System.Net.Http.Headers;

namespace ArrearsApi.Tests.V1.E2ETests
{
    
public class ExampleDynamoDbTest : DynamoDbIntegrationTests<Startup>
    {
        private readonly Fixture _fixture = new Fixture();

        /// <summary>
        /// Method to construct a test entity that can be used in a test
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private Arrears ConstructTestEntity()
        {
            var entity = _fixture.Create<Arrears>();
            entity.CreatedAt = DateTime.UtcNow;
            return entity;
        }

        /// <summary>
        /// Method to add an entity instance to the database so that it can be used in a test.
        /// Also adds the corresponding action to remove the upserted data from the database when the test is done.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private async Task SetupTestData(Arrears entity)
        {
            await DynamoDbContext.SaveAsync(entity.ToDatabase()).ConfigureAwait(false);
            CleanupActions.Add(async () => await DynamoDbContext.DeleteAsync<ArrearsDbEntity>(entity.Id).ConfigureAwait(false));
        }

        [Test]
        public async Task GetArrearsByIdNotFoundReturns404()
        {
            var id = new Guid("1b1d6a4b-9f44-4f6d-b6b4-c4cf4046e8d8");
            //TODO: Update uri route to match the APIs endpoint
            var uri = new Uri($"api/v1/arrears/{id}", UriKind.Relative);
            var response = await Client.GetAsync(uri).ConfigureAwait(false);

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);

            var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var apiEntity = JsonConvert.DeserializeObject<AppException>(responseContent);

            apiEntity.Should().NotBeNull();
            apiEntity.Message.Should().BeEquivalentTo("No Arrear by provided arrearId cannot be found!");
            apiEntity.StatusCode.Should().Be(404);
            apiEntity.Details.Should().BeEquivalentTo(null);
        }

        [Test]
        public async Task GetArrearsBydIdFoundReturnsResponse()
        {
            var entity = ConstructTestEntity();
            await SetupTestData(entity).ConfigureAwait(false);

            //TODO: Update uri route to match the APIs endpoint
            var uri = new Uri($"api/v1/arrears/{entity.Id}", UriKind.Relative);
            var response = await Client.GetAsync(uri).ConfigureAwait(false);

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var apiEntity = JsonConvert.DeserializeObject<Arrears>(responseContent);

            apiEntity.Should().BeEquivalentTo(entity, (x) => x.Excluding(y => y.CreatedAt));
            apiEntity.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, 1000);
        }

        [Test]
        public async Task HealchCheckOkReturns200()
        {
            var uri = new Uri($"api/v1/healthcheck/ping", UriKind.Relative);
            var response = await Client.GetAsync(uri).ConfigureAwait(false);

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var apiEntity = JsonConvert.DeserializeObject<HealthCheckResponse>(responseContent);

            apiEntity.Should().NotBeNull();
            apiEntity.Message.Should().BeNull();
            apiEntity.Success.Should().BeTrue();
        }
        [Test]
        public async Task CreateArrearsBadRequestReturns400()
        {
            var arrearsDomain = ConstructTestEntity();

            arrearsDomain.TargetType = TargetType.estate;
            arrearsDomain.TotalCharged = -1;
            arrearsDomain.TotalPaid = -1;
            arrearsDomain.CurrentBalance = -1;
            arrearsDomain.TargetId = new Guid();
            arrearsDomain.Person = new Person();
            arrearsDomain.AssetAddress = new AssetAddress();

            var uri = new Uri($"api/v1/arrears", UriKind.Relative);
            string body = JsonConvert.SerializeObject(arrearsDomain);

            HttpResponseMessage response;
            using (StringContent stringContent = new StringContent(body))
            {
                stringContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                response = await Client.PostAsync(uri, stringContent).ConfigureAwait(false);
            }

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            
        }

        [Test]
        public async Task CreateArrearsValidRequestReturns201()
        {
            var arrearsDomain = ConstructTestEntity();


            var uri = new Uri($"api/v1/arrears", UriKind.Relative);
            string body = JsonConvert.SerializeObject(arrearsDomain);

            HttpResponseMessage response;
            using (StringContent stringContent = new StringContent(body))
            {
                stringContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                response = await Client.PostAsync(uri, stringContent).ConfigureAwait(false);
            }

            response.StatusCode.Should().Be(HttpStatusCode.Created);
            response.Headers.Location.Should().NotBeNull();
        }
    }
}
