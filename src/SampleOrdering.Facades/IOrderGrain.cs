using System.Threading.Tasks;
using Orleans;
using System;

namespace SampleOrdering.Facades
{
    /// <summary>
    /// Grain interface for order.
    /// </summary>
    public interface IOrderGrain : IGrainWithGuidKey
    {
        /// <summary>
        /// Adding new product
        /// Returns price after modification of order.
        /// </summary>
        /// <param name="productId">Id of product.</param>
        /// <returns></returns>
        Task<decimal> AddItem(Guid productId, int amount = 1);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productId">Id of product.</param>
        /// <param name="amount">Amount of products.</param>
        /// <returns></returns>
        Task<decimal> RemoveItem(Guid productId, int amount);

        /// <summary>
        /// Calculates and returning price.
        /// </summary>
        Task<decimal> GetSummaryPrice();

        /// <summary>
        /// Set up client id for order.
        /// </summary>
        Task SetClientId(Guid clientId);

        // TODO: Make events which distributed to client grain, for example
    }
}
