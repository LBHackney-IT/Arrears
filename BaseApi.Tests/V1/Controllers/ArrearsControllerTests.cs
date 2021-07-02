
using ArrearsApi.V1.Boundary.Response;
using ArrearsApi.V1.Controllers;
using ArrearsApi.V1.Domain;
using ArrearsApi.V1.UseCase.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Routing;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace ArrearsApi.Tests.V1.Controllers
{
   // [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031Big SmileoNotCatchGeneralExceptionTypes")]
    [TestFixture]
    public class ArrearsControllerTests
    {
        private ArrearsApiController _arrearsApiController;
        private ControllerContext _controllerContext;
        private HttpContext _httpContext;

        private Mock<IGetAllUseCase> _getAllUseCase;
        private Mock<IGetByIdUseCase> _getByIdUseCase;
        private Mock<IAddUseCase> _addUseCase;

        [SetUp]
        public void Init()
        {
            _getAllUseCase = new Mock<IGetAllUseCase>();

            _getByIdUseCase = new Mock<IGetByIdUseCase>();

            _addUseCase = new Mock<IAddUseCase>();

            _httpContext = new DefaultHttpContext();
            _controllerContext = new ControllerContext(new ActionContext(_httpContext, new RouteData(), new ControllerActionDescriptor()));
            _arrearsApiController = new ArrearsApiController(_getAllUseCase.Object, _getByIdUseCase.Object, _addUseCase.Object)
            {
                ControllerContext = _controllerContext
            };
        }

        [Test]
        public async Task GetAllByTargetTypeEstateObjectsReturns200()
        {
            _getAllUseCase.Setup(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<int>()))
                .ReturnsAsync(
                    new ArrearsResponseObjectList
                    {
                        ArrearsResponseObjects = new List<ArrearsResponseObject>() {
                            new ArrearsResponseObject
                            {
                                Id = new Guid("3cb13efc-14b9-4da8-8eb2-f552434d219d"),
                                TargetId = new Guid("0f1da28f-a1e7-478b-aee9-3656cf9d8ab1"),
                                TargetType = TargetType.estate,
                                TotalCharged =100,
                                TotalPaid=20,
                                CurrentBalance=80,
                                CreatedAt = new DateTime(2021, 7, 2),
                                Person= new Person
                                {
                                    Title="Mr",
                                    FirstName="Kian",
                                    LastName="Hayward"
                                },
                                AssetAddress=new AssetAddress
                                {
                                    AddressLine1="15Marcon Court",
                                    AddressLine2="Hackney",
                                    AddressLine3="London",
                                    AddressLine4="UK",
                                    PostCode="E8 1ND"
                                }

                            }
                        }
                    });
                                                
                    

            var result = await _arrearsApiController.GetAllAsync("tenure", 1).ConfigureAwait(false);

            result.Should().NotBeNull();

            var okResult = result as OkObjectResult;

            okResult.Should().NotBeNull();

            var arrearsResult = okResult.Value as ArrearsResponseObjectList;

            arrearsResult.Should().NotBeNull();

            arrearsResult.ArrearsResponseObjects.Should().HaveCount(1);

            arrearsResult.ArrearsResponseObjects[0].Id.Should().Be(new Guid("3cb13efc-14b9-4da8-8eb2-f552434d219d"));
            arrearsResult.ArrearsResponseObjects[0].TargetId.Should().Be(new Guid("0f1da28f-a1e7-478b-aee9-3656cf9d8ab1"));
            arrearsResult.ArrearsResponseObjects[0].TargetType.Should().Be(TargetType.estate);
            arrearsResult.ArrearsResponseObjects[0].CreatedAt.Should().Be(new DateTime(2021, 7, 2));
            arrearsResult.ArrearsResponseObjects[0].TotalCharged.Should().Be(100);
            arrearsResult.ArrearsResponseObjects[0].TotalPaid.Should().Be(20);
            arrearsResult.ArrearsResponseObjects[0].CurrentBalance.Should().Be(80);
            arrearsResult.ArrearsResponseObjects[0].AssetAddress.Should().NotBeNull();
            arrearsResult.ArrearsResponseObjects[0].AssetAddress.AddressLine1.Should().Be("15Marcon Court");
            arrearsResult.ArrearsResponseObjects[0].AssetAddress.AddressLine2.Should().Be("Hackney");
            arrearsResult.ArrearsResponseObjects[0].AssetAddress.AddressLine3.Should().Be("London");
            arrearsResult.ArrearsResponseObjects[0].AssetAddress.AddressLine4.Should().Be("UK");
            arrearsResult.ArrearsResponseObjects[0].AssetAddress.PostCode.Should().Be("E8 1ND");
            arrearsResult.ArrearsResponseObjects[0].Person.Should().NotBeNull();
            arrearsResult.ArrearsResponseObjects[0].Person.Title.Should().Be("Mr");
            arrearsResult.ArrearsResponseObjects[0].Person.FirstName.Should().Be("Kian");
            arrearsResult.ArrearsResponseObjects[0].Person.LastName.Should().Be("Hayward");

        }

        

        [Test]
        public async Task GetAllAsyncObjectsReturns500()
        {
            _getAllUseCase.Setup(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<int>()))
                .ThrowsAsync(new Exception("Test exception"));

            try
            {
                var result = await _arrearsApiController.GetAllAsync("tenure", 4).ConfigureAwait(false);
                Assert.Fail();
            }
            
            catch (Exception ex)
            {
                ex.GetType().Should().Be(typeof(Exception));
                ex.Message.Should().Be("Test exception");
            }
            
        }

        [Test]
        public async Task GetByArrearsIdValidIdReturns200()
        {
            _getByIdUseCase.Setup(x => x.ExecuteAsync(It.IsAny<Guid>()))
               .ReturnsAsync(new ArrearsResponseObject
               {
                   Id = new Guid("3cb13efc-14b9-4da8-8eb2-f552434d219d"),
                   TargetId = new Guid("0f1da28f-a1e7-478b-aee9-3656cf9d8ab1"),
                   TargetType = TargetType.estate,
                   TotalCharged = 100,
                   TotalPaid = 20,
                   CurrentBalance = 80,
                   CreatedAt = new DateTime(2021, 7, 2),
                   Person = new Person
                   {
                       Title = "Mr",
                       FirstName = "Kian",
                       LastName = "Hayward"
                   },
                   AssetAddress = new AssetAddress
                   {
                       AddressLine1 = "15Marcon Court",
                       AddressLine2 = "Hackney",
                       AddressLine3 = "London",
                       AddressLine4 = "UK",
                       PostCode = "E8 1ND"
                   }

               });

            var result = await _arrearsApiController.Get(new Guid("3cb13efc-14b9-4da8-8eb2-f552434d219d"))
                .ConfigureAwait(false);

            result.Should().NotBeNull();

            var okResult = result as OkObjectResult;

            okResult.Should().NotBeNull();

            var arrearsResponse = okResult.Value as ArrearsResponseObject;

            arrearsResponse.Should().NotBeNull();

            arrearsResponse.Id.Should().Be(new Guid("3cb13efc-14b9-4da8-8eb2-f552434d219d"));
            arrearsResponse.TargetId.Should().Be(new Guid("0f1da28f-a1e7-478b-aee9-3656cf9d8ab1"));
            arrearsResponse.TargetType.Should().Be(TargetType.estate);
            arrearsResponse.CreatedAt.Should().Be(new DateTime(2021, 7, 2));
            arrearsResponse.TotalCharged.Should().Be(100);
            arrearsResponse.TotalPaid.Should().Be(20);
            arrearsResponse.CurrentBalance.Should().Be(80);
            arrearsResponse.AssetAddress.Should().NotBeNull();
            arrearsResponse.AssetAddress.AddressLine1.Should().Be("15Marcon Court");
            arrearsResponse.AssetAddress.AddressLine2.Should().Be("Hackney");
            arrearsResponse.AssetAddress.AddressLine3.Should().Be("London");
            arrearsResponse.AssetAddress.AddressLine4.Should().Be("UK");
            arrearsResponse.AssetAddress.PostCode.Should().Be("E8 1ND");
            arrearsResponse.Person.Should().NotBeNull();
            arrearsResponse.Person.Title.Should().Be("Mr");
            arrearsResponse.Person.FirstName.Should().Be("Kian");
            arrearsResponse.Person.LastName.Should().Be("Hayward");

        }

        [Test]
        public async Task GetByArrearsIdWithInvalidIdReturns404()
        {
            _getByIdUseCase.Setup(x => x.ExecuteAsync(It.IsAny<Guid>()))
                .ReturnsAsync((ArrearsResponseObject) null);

            var result = await _arrearsApiController.Get( new Guid("ff353355-d884-4bc9-a684-f0ccc616ba4e"))
                .ConfigureAwait(false);

            result.Should().NotBeNull();

            var notFoundResult = result as NotFoundObjectResult;

            notFoundResult.Should().NotBeNull();

            var response = notFoundResult.Value as AppException;

            response.Should().NotBeNull();

            response.StatusCode.Should().Be((int) HttpStatusCode.NotFound);

            response.Message.Should().Be("No Arrear by provided arrearId cannot be found!");

            response.Details.Should().Be(null);
        }

        [Test]
        public async Task GetByArrearsIdReturns500()
        {
            _getByIdUseCase.Setup(x => x.ExecuteAsync(It.IsAny<Guid>()))
                .ThrowsAsync(new Exception("Test exception"));

            try
            {
                var result = await _arrearsApiController.Get(new Guid("6791051d-961d-4e16-9853-6e7e45b01b49"))
                    .ConfigureAwait(false);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                ex.GetType().Should().Be(typeof(Exception));
                ex.Message.Should().Be("Test exception");
            }
        }

        [Test]
        public async Task CreateArrearsWithValidDataReturns200()
        {
            _addUseCase.Setup(x => x.ExecuteAsync(It.IsAny<Arrears>())).Returns(Task.FromResult(new ArrearsResponseObject()));
            _getByIdUseCase.Setup(x => x.ExecuteAsync(It.IsAny<Guid>()))
                .ReturnsAsync((ArrearsResponseObject) null);

            var result = await _arrearsApiController.Post(
                new Arrears
                {
                    TargetId = new Guid("2a6e12ca-3691-4fa7-bd77-5039652f0354"),
                    TargetType = TargetType.estate,
                    TotalCharged = 100,
                    CreatedAt = new DateTime(2021, 7, 1),
                    TotalPaid = 20,
                    CurrentBalance = 80,
                    Person = new Person
                    {
                        Title = "Mr",
                        FirstName = "Kian",
                        LastName = "Hayward"
                    },
                    AssetAddress = new AssetAddress
                    {
                        AddressLine1 = "15Marcon Court",
                        AddressLine2 = "Hackney",
                        AddressLine3 = "London",
                        AddressLine4 = "UK",
                        PostCode = "E8 1ND"
                    }
                }).ConfigureAwait(false);

            result.Should().NotBeNull();

            _addUseCase.Verify(x => x.ExecuteAsync(It.IsAny<Arrears>()), Times.Once);

            var redirectToActionResult = result as CreatedAtRouteResult;

            redirectToActionResult.Should().NotBeNull();

            redirectToActionResult.RouteName.Should().Be("Get");

            redirectToActionResult.RouteValues.Should().NotBeNull();

            redirectToActionResult.RouteValues.Should().HaveCount(1);

        }

        [Test]
        public async Task CreateArrearsWithInvalidDataReturns400()
        {
            _getByIdUseCase.Setup(x => x.ExecuteAsync(It.IsAny<Guid>()))
                .ReturnsAsync((ArrearsResponseObject) null);
            var result = await _arrearsApiController.Post( new Arrears()).ConfigureAwait(false);

            result.Should().NotBeNull();

            var badRequestResult = result as BadRequestObjectResult;

            badRequestResult.Should().NotBeNull();

            var response = badRequestResult.Value as AppException;

            response.Should().NotBeNull();

            response.StatusCode.Should().Be((int) HttpStatusCode.BadRequest);

            response.Details.Should().Be(null);

            response.Message.Should().Be("Arrears record save failed!");
        }

        [Test]
        public async Task CreateArrearsReturns500()
        {
            _addUseCase.Setup(x => x.ExecuteAsync(It.IsAny<Arrears>()))
                .ThrowsAsync(new Exception("Test exception"));

            try
            {
                var result = await _arrearsApiController.Post( new Arrears { })
                    .ConfigureAwait(false);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                ex.GetType().Should().Be(typeof(Exception));
                ex.Message.Should().Be("Test exception");
                
            }
        }
    }
}
