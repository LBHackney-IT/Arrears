using System.Collections.Generic;
using System.Linq;
using ArrearsApi.V1.Boundary.Response;
using ArrearsApi.V1.Domain;

namespace ArrearsApi.V1.Factories
{
    public static class ResponseFactory
    {
        //TODO: Map the fields in the domain object(s) to fields in the response object(s).
        // More information on this can be found here https://github.com/LBHackney-IT/lbh-base-api/wiki/Factory-object-mappings
        public static ArrearsResponseObject ToResponse(this Arrears domain)
        {
            return new ArrearsResponseObject {
                Id = domain.Id,
                TargetType=domain.TargetType,
                TargetId = domain.TargetId,
                TotalPaid = domain.TotalPaid,
                TotalCharged = domain.TotalCharged,
                CurrentBalance = domain.CurrentBalance,
                CreatedAt = domain.CreatedAt
            };
        }

        public static List<ArrearsResponseObject> ToResponse(this IEnumerable<Arrears> domainList)
        {
            return domainList.Select(domain => domain.ToResponse()).ToList();
        }
    }
}
