using ArrearsApi.V1.Domain;
using ArrearsApi.V1.Factories;
using NUnit.Framework;

namespace ArrearsApi.Tests.V1.Factories
{
    public class ResponseFactoryTest
    {
        //TODO: add assertions for all the fields being mapped in `ResponseFactory.ToResponse()`. Also be sure to add test cases for
        // any edge cases that might exist.
        [Test]
        public void CanMapADatabaseEntityToADomainObject()
        {
            var domain = new Arrears();
            var response = domain.ToResponse();
            //TODO: check here that all of the fields have been mapped correctly. i.e. response.fieldOne.Should().Be("one")
        }
    }
}
