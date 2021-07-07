using ArrearsApi.V1.Domain;
using System;

namespace ArrearsApi.V1.Boundary.Response
{
    public class ArrearsResponseObject
    {
        public ArrearsResponseObject()
        {
            AssetAddress = new AssetAddress();
            Person = new Person();
        }

        public Guid Id { get; set; }
        public Guid TargetId { get; set; }
        public TargetType TargetType { get; set; }
        public decimal TotalCharged { get; set; }
        public decimal TotalPaid { get; set; }
        public decimal CurrentBalance { get; set; }
        public DateTime CreatedAt { get; set; }
        public AssetAddress AssetAddress { get; set; }
        public Person Person { get; set; }
    }
}
