using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleOrdering.Domain.Entities
{
    /// <summary>
    /// Balance of client.
    /// </summary>
    public class Balance : Entity
    {
        /// <summary>
        /// Id of client, for more information see <see cref="Client"/>.
        /// </summary>
        public Guid ClientId { get; set; }

        /// <summary>
        /// Amount of balance on client credit or whatever.
        /// </summary>
        public decimal Amount { get; set; }
    }
}
