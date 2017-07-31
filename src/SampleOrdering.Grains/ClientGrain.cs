using Orleans;
using Orleans.Providers;
using SampleOrdering.Domain.Entities;
using SampleOrdering.Facades;
using System;
using System.Threading.Tasks;

namespace SampleOrdering.Grains
{
    /// <summary>
    /// Implementation of <see cref="IClientGrain"/>.
    /// </summary>
    [StorageProvider(ProviderName = "MemoryStore")]
    public class ClientGrain : Grain<Client>, IClientGrain
    {
        public override async Task OnActivateAsync()
        {
            if (!State.IsActivated)
            {
                State.OrderId = Guid.NewGuid();
                State.BalanceId = Guid.NewGuid();

                var order = this.GrainFactory.GetGrain<IOrderGrain>(State.OrderId.Value);
                await order.SetClientId(State.OrderId.Value);
                var balance = this.GrainFactory.GetGrain<IBalanceGrain>(State.BalanceId);
                await balance.SetClient(this.GetPrimaryKey());

                await WriteStateAsync();
            }

            await base.OnActivateAsync();
        }

        public Task<decimal> GetBalance()
        {
            var balance = this.GrainFactory.GetGrain<IBalanceGrain>(State.BalanceId);

            return balance.GetAmount();
        }

        public Task<decimal> GetOrderPrice()
        {
            if (State.OrderId.HasValue)
            {
                var order = this.GrainFactory.GetGrain<IOrderGrain>(State.OrderId.Value);
                return order.GetSummaryPrice();
            }

            return Task.FromResult(0m);
        }

        public async Task PlaceProduct(Guid productId, int amount)
        {
            var order = this.GrainFactory.GetGrain<IOrderGrain>(State.OrderId.Value);
            var product = this.GrainFactory.GetGrain<IProductGrain>(productId);
            var priceOfProduct = await product.GetPrice();
            var balance = this.GrainFactory.GetGrain<IBalanceGrain>(State.BalanceId);

            await order.AddItem(productId, amount);

            await balance.Decrease(priceOfProduct * amount);
        }

        public Task<Guid> GetBalanceId() => Task.FromResult(State.BalanceId);
    }
}
