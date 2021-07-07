using System.Collections.Generic;
using System.Linq;
using ArrearsApi.V1.Boundary.Response;
using ArrearsApi.V1.Domain;

namespace ArrearsApi.V1.Factories
{
    public static class ResponseFactory
    {
        public static ArrearsResponseObject ToResponse(this Arrears domain)
        {
            return new ArrearsResponseObject {
                Id = domain.Id,
                TargetType=domain.TargetType,
                TargetId = domain.TargetId,
                TotalPaid = domain.TotalPaid,
                TotalCharged = domain.TotalCharged,
                CurrentBalance = domain.CurrentBalance,
                CreatedAt = domain.CreatedAt,
                AssetAddress = domain.AssetAddress,
                Person = domain.Person
            };
        }

        public static List<ArrearsResponseObject> ToResponse(this IEnumerable<Arrears> domainList)
        {
            return domainList.Select(domain => domain.ToResponse()).ToList();
        }
    }
}
