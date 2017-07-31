using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleOrdering.Domain.Entities
{
    public class Product : Entity
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public decimal CurrentPrice { get; set; }

        /// <summary>
        /// Stock where we persist information about amount of items.
        /// </summary>
        public Guid StockId { get; set; }
    }
}
