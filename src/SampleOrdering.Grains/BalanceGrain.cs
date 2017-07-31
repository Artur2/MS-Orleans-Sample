using Orleans;
using Orleans.Providers;
using SampleOrdering.Domain.Entities;
using SampleOrdering.Facades;
using System;
using System.Threading.Tasks;

namespace SampleOrdering.Grains
{
    [StorageProvider(ProviderName = "MemoryStore")]
    public class BalanceGrain : Grain<Balance>, IBalanceGrain
    {
        public Task Decrease(decimal amount)
        {
            State.Amount -= amount;

            return WriteStateAsync();
        }

        public Task<decimal> GetAmount()
        {
            return Task.FromResult(State.Amount);
        }

        public Task Increase(decimal amount)
        {
            State.Amount += amount;

            return WriteStateAsync();
        }

        public Task SetClient(Guid clientId)
        {
            State.ClientId = clientId;

            return WriteStateAsync();
        }
    }
}
