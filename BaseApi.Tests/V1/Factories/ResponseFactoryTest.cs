using ArrearsApi.V1.Domain;
using ArrearsApi.V1.Factories;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace ArrearsApi.Tests.V1.Factories
{
    public class ResponseFactoryTest
    {
        //TODO: add assertions for all the fields being mapped in `ResponseFactory.ToResponse()`. Also be sure to add test cases for
        // any edge cases that might exist.
        [Test]
        public void CanMapADatabaseEntityToADomainObject()
        {
            var domain = new Arrears
            {
                Id=new Guid("58daf21a-e2d5-475f-87f4-1c0c7f1ffb10"),
                TargetId = new Guid("2a6e12ca-3691-4fa7-bd77-5039652f0354"),
                TargetType = TargetType.tenure,
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
            };
            var response = domain.ToResponse();
            response.Id.Should().Be(new Guid("58daf21a-e2d5-475f-87f4-1c0c7f1ffb10"));
            response.TargetId.Should().Be(new Guid("2a6e12ca-3691-4fa7-bd77-5039652f0354"));
            response.TargetType.Should().Be(TargetType.tenure);
            response.CreatedAt.Should().Be(new DateTime(2021, 7, 1));
            response.TotalCharged.Should().Be(100);
            response.TotalPaid.Should().Be(20);
            response.CurrentBalance.Should().Be(80);
            response.Person.Title.Should().Be("Mr");
            response.Person.FirstName.Should().Be("Kian");
            response.Person.LastName.Should().Be("Hayward");
            response.AssetAddress.AddressLine1.Should().Be("15Marcon Court");
            response.AssetAddress.AddressLine2.Should().Be("Hackney");
            response.AssetAddress.AddressLine3.Should().Be("London");
            response.AssetAddress.AddressLine4.Should().Be("UK");
            response.AssetAddress.PostCode.Should().Be("E8 1ND");

        }

        [Test]
        public void CanMapListOfArrearsDomainObjectsToResponse()
        {
            var firstArrear = new Arrears
            {
                Id = new Guid("58daf21a-e2d5-475f-87f4-1c0c7f1ffb10"),
                TargetId = new Guid("2a6e12ca-3691-4fa7-bd77-5039652f0354"),
                TargetType = TargetType.tenure,
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
            };

            var secondArrear = new Arrears
            {
                Id = new Guid("94af400f-4d5b-4866-9a79-cbb438019a0f"),
                TargetId = new Guid("e537d451-d8cf-4449-9635-bb08afd61bf8"),
                TargetType = TargetType.estate,
                TotalCharged = 100,
                CreatedAt = new DateTime(2021, 7, 1),
                TotalPaid = 20,
                CurrentBalance = 80,
                AssetAddress = new AssetAddress
                {
                    AddressLine1 = "15A Marcon Court",
                    AddressLine2 = "Hackney1",
                    AddressLine3 = "London1",
                    AddressLine4 = "UK1",
                    PostCode = "E8 2ND"
                }
            };

            var listOfDomains = new List<Arrears>()
            {
                firstArrear,
                secondArrear
            };

            var response = listOfDomains.ToResponse();

            response[0].Id.Should().Be(new Guid("58daf21a-e2d5-475f-87f4-1c0c7f1ffb10"));
            response[0].TargetId.Should().Be(new Guid("2a6e12ca-3691-4fa7-bd77-5039652f0354"));
            response[0].TargetType.Should().Be(TargetType.tenure);
            response[0].CreatedAt.Should().Be(new DateTime(2021, 7, 1));
            response[0].TotalCharged.Should().Be(100);
            response[0].TotalPaid.Should().Be(20);
            response[0].CurrentBalance.Should().Be(80);
            response[0].AssetAddress.AddressLine1.Should().Be("15Marcon Court");
            response[0].AssetAddress.AddressLine2.Should().Be("Hackney");
            response[0].AssetAddress.AddressLine3.Should().Be("London");
            response[0].AssetAddress.AddressLine4.Should().Be("UK");
            response[0].AssetAddress.PostCode.Should().Be("E8 1ND");
            response[0].Person.Title.Should().Be("Mr");
            response[0].Person.FirstName.Should().Be("Kian");
            response[0].Person.LastName.Should().Be("Hayward");


            response[1].Id.Should().Be(new Guid("94af400f-4d5b-4866-9a79-cbb438019a0f"));
            response[1].TargetId.Should().Be(new Guid("e537d451-d8cf-4449-9635-bb08afd61bf8"));
            response[1].TargetType.Should().Be(TargetType.estate);
            response[1].CreatedAt.Should().Be(new DateTime(2021, 7, 1));
            response[1].TotalCharged.Should().Be(100);
            response[1].TotalPaid.Should().Be(20);
            response[1].CurrentBalance.Should().Be(80);
            response[1].AssetAddress.AddressLine1.Should().Be("15A Marcon Court");
            response[1].AssetAddress.AddressLine2.Should().Be("Hackney1");
            response[1].AssetAddress.AddressLine3.Should().Be("London1");
            response[1].AssetAddress.AddressLine4.Should().Be("UK1");
            response[1].AssetAddress.PostCode.Should().Be("E8 2ND");
        }
    }
}
