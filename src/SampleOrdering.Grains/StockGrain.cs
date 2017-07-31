using Orleans;
using Orleans.Providers;
using SampleOrdering.Domain.Entities;
using SampleOrdering.Facades;
using System;
using System.Threading.Tasks;

namespace SampleOrdering.Grains
{
    [StorageProvider(ProviderName = "MemoryStore")]
    public class StockGrain : Grain<Stock>, IStockGrain
    {
        public Task<Guid> GetProductId() => Task.FromResult(State.ProductId);

        public Task<bool> HasAvaialableAmount(int amount) => Task.FromResult(State.Amount >= amount);

        public Task IncrementStock(int amount = 1)
        {
            State.Amount += 1;
            return WriteStateAsync();
        }

        public Task RetrieveFromStock(int amount = 1)
        {
            if (State.Amount >= amount)
            {
                State.Amount -= amount;
                return WriteStateAsync();
            }
            else
            {
                throw new InvalidOperationException("Can't retrieve more amount than in stock");
            }
        }

        public Task SetAttributes(Guid productId, int amount)
        {
            if (!State.IsActivated)
            {
                State.ProductId = productId;
                State.Amount = amount;
                return WriteStateAsync();
            }

            return Task.FromResult(0);
        }
    }
}
