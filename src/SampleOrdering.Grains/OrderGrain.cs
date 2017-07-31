using System;
using System.Threading.Tasks;
using Orleans;
using SampleOrdering.Facades;
using SampleOrdering.Domain.Entities;
using System.Linq;
using Orleans.Providers;

namespace SampleOrdering.Grains
{
    /// <summary>
    /// Implementation of Order grain facade.
    /// </summary>
    [StorageProvider(ProviderName = "MemoryStore")]
    public class OrderGrain : Grain<Order>, IOrderGrain
    {
        public async Task<decimal> AddItem(Guid productId, int amount)
        {
            var product = this.GrainFactory.GetGrain<IProductGrain>(productId);

            var stock = this.GrainFactory.GetGrain<IStockGrain>(await product.GetStockId());

            if (!(await stock.HasAvaialableAmount(amount)))
            {
                throw new InvalidOperationException("We haven't available amount of this product.");
            }

            await stock.RetrieveFromStock();

            var orderItem = State.Items.SingleOrDefault(x => x.ProductId == productId);

            if (orderItem == null)
            {
                orderItem = new OrderItem
                {
                    Id = Guid.NewGuid(),
                    ProductId = productId,
                    Amount = 1,
                    Price = await product.GetPrice()
                };

                State.Items.Add(orderItem);
            }
            else
            {
                orderItem.Amount += 1;
            }

            await WriteStateAsync();

            return State.GetSummaryPrice();
        }

        public Task<decimal> GetSummaryPrice()
        {
            return Task.FromResult(State.GetSummaryPrice());
        }

        public async Task<decimal> RemoveItem(Guid productId, int amount)
        {
            var product = this.GrainFactory.GetGrain<IProductGrain>(productId);

            var stock = this.GrainFactory.GetGrain<IStockGrain>(await product.GetStockId());

            await stock.IncrementStock(amount);

            var orderItem = State.Items.SingleOrDefault(x => x.ProductId == productId);
            if (orderItem == null)
            {
                throw new InvalidOperationException("Can't find order item");
            }

            orderItem.Amount -= 1;
            if (orderItem.Amount <= 0)
            {
                State.Items.Remove(orderItem);
            }

            await WriteStateAsync();

            return State.GetSummaryPrice();
        }

        public Task SetClientId(Guid clientId)
        {
            State.ClientId = clientId;
            return WriteStateAsync();
        }
    }
}
