using Orleans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleOrdering.Facades
{
    /// <summary>
    /// Grain which responsible for operations related to client and downstream.
    /// </summary>
    public interface IClientGrain : IGrainWithGuidKey
    {
        /// <summary>
        /// Retrieving balance of client.
        /// </summary>
        Task<decimal> GetBalance();

        Task<Guid> GetBalanceId();

        /// <summary>
        /// Placing product.
        /// </summary>
        Task PlaceProduct(Guid productId, int amount);

        /// <summary>
        /// Get price of order.
        /// </summary>
        Task<decimal> GetOrderPrice();
    }
}
