using Orleans;
using Orleans.Providers;
using SampleOrdering.Domain.Entities;
using SampleOrdering.Facades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleOrdering.Grains
{
    [StorageProvider(ProviderName = "MemoryStore")]
    public class ProductGrain : Grain<Product>, IProductGrain
    {
        public Task<decimal> GetPrice() => Task.FromResult(State.CurrentPrice);

        public Task<Guid> GetStockId() => Task.FromResult(State.StockId);

        public Task SetAttributes(ProductAttributes attributes)
        {
            if (!State.IsActivated)
            {
                State.Id = attributes.Id;
                State.StockId = attributes.StockId;
                State.Title = attributes.Title;
                State.Description = attributes.Description;
                State.IsActivated = true;
                State.CurrentPrice = attributes.CurrentPrice;

                var stock = this.GrainFactory.GetGrain<IStockGrain>(State.StockId);
                stock.SetAttributes(State.Id, 10);

                return WriteStateAsync();
            }

            return Task.FromResult(0);
        }
    }
}
