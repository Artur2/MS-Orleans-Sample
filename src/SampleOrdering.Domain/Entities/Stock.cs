using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleOrdering.Domain.Entities
{
    /// <summary>
    /// Stock of products.
    /// </summary>
    public class Stock : Entity
    {
        /// <summary>
        /// Id of product, for more info see <see cref="Product"/>.
        /// </summary>
        public Guid ProductId { get; set; }

        /// <summary>
        /// Amount of products in stock.
        /// </summary>
        public int Amount { get; set; }
    }
}
