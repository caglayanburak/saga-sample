using System;

namespace Commonlib
{
    public interface IOrderReceivedEvent
    {
        Guid CorrelationId { get; }
        int OrderId { get; }
        string OrderCode { get; }
    }

    public interface IOrderProcessedEvent
    {
        Guid CorrelationId { get; set; }
        int OrderId { get; set; }
        string OrderCode { get; set; }
    }

    public class OrderModel : IOrderReceivedEvent
    {
        public string OrderCode { get; set; }

        public Guid CorrelationId { get; set; }

        public int OrderId { get; set; }
    }
}
