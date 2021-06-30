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
            var entity = new Arrears();
            entity.Id.Should().NotBeEmpty();
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
