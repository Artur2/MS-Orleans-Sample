using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleOrdering.Domain.Entities
{
    public class Order : Entity
    {
        public Guid ClientId { get; set; }

        public List<OrderItem> Items { get; set; } = new List<OrderItem>();

        public decimal GetSummaryPrice() => Items.Sum(x => x.GetSummaryPrice());
    }
}
