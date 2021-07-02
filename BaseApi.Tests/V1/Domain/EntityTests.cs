using System;
using ArrearsApi.V1.Domain;
using FluentAssertions;
using NUnit.Framework;

namespace ArrearsApi.Tests.V1.Domain
{
    [TestFixture]
    public class EntityTests
    {
        [Test]
        public void EntitiesHaveAnId()
        {
            var entity = new Arrears
            {
                Id = new Guid("9b014c26-88be-466e-a589-0f402c6b94c1"),
                TargetId = new Guid("58daf21a-e2d5-475f-87f4-1c0c7f1ffb10"),
                TargetType = TargetType.estate,
                CreatedAt = new DateTime(2021, 7, 1),
                TotalCharged=100,
                TotalPaid=20,
                CurrentBalance= 80,
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

            };
            entity.Id.Should().Be(new Guid("9b014c26-88be-466e-a589-0f402c6b94c1"));
            entity.TargetId.Should().Be(new Guid("58daf21a-e2d5-475f-87f4-1c0c7f1ffb10"));
            entity.TargetType.Should().Be(TargetType.estate);
            entity.CreatedAt.Should().Be(new DateTime(2021, 7, 1));
            entity.TotalCharged.Should().Be(100);
            entity.TotalPaid.Should().Be(20);
            entity.CurrentBalance.Should().Be(80);
            entity.AssetAddress.Should().NotBeNull();
            entity.Person.Should().NotBeNull();
        }

        [Test]
        public void EntitiesHaveACreatedAt()
        {
            var entity = new Arrears();
            var date = new DateTime(2019, 02, 21);
            entity.CreatedAt = date;

            entity.CreatedAt.Should().BeSameDateAs(date);
        }
    }
}
