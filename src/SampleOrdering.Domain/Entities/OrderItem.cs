using System;

namespace SampleOrdering.Domain.Entities
{
    public class OrderItem : Entity
    {
        public Guid ProductId { get; set; }

        public int Amount { get; set; }

        public decimal Price { get; set; }

        public decimal GetSummaryPrice() => Amount * Price;
    }
}