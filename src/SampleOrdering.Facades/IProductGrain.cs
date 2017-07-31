using Orleans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleOrdering.Facades
{
    public interface IProductGrain : IGrainWithGuidKey
    {
        Task<Guid> GetStockId();

        Task<decimal> GetPrice();

        Task SetAttributes(ProductAttributes attributes);
    }

    
    /// <summary>
    /// Need for bootstrapping products.
    /// </summary>
    [Serializable]
    public class ProductAttributes
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public decimal CurrentPrice { get; set; }

        /// <summary>
        /// Sock where we persist information about amount of items.
        /// </summary>
        public Guid StockId { get; set; }
    }

}
