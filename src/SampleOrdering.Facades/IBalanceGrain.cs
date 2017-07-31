using Orleans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleOrdering.Facades
{
    public interface IBalanceGrain : IGrainWithGuidKey
    {
        Task Decrease(decimal amount);

        Task Increase(decimal amount);

        Task<decimal> GetAmount();

        Task SetClient(Guid clientId);
    }
}
