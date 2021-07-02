using System;

namespace ArrearsApi.V1.Domain
{
    //TODO: rename this class to be the domain object which this API will getting. e.g. Residents or Claimants
    public class Arrears
    {
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
