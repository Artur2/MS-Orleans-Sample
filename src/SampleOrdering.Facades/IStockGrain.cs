using System.Threading.Tasks;
using Orleans;
using System;

namespace SampleOrdering.Facades
{
    /// <summary>
    /// Grain interface IStockGrain
    /// </summary>
    public interface IStockGrain : IGrainWithGuidKey
    {
        /// <summary>
        /// Product id of stock grain.
        /// </summary>
        Task<Guid> GetProductId();

        /// <summary>
        /// Retrieve from stock product to place somewhere e.g. reserve.
        /// </summary>
        /// <param name="productId">Id of product</param>
        /// <returns></returns>
        Task RetrieveFromStock(int amount = 1);

        Task IncrementStock(int amount = 1);

        /// <summary>
        /// Checking if amount is available. 
        /// </summary>
        /// <param name="amount">Amount.</param>
        /// <returns>True if available, false if not.</returns>
        Task<bool> HasAvaialableAmount(int amount);

        Task SetAttributes(Guid productId, int amount);
    }
}
