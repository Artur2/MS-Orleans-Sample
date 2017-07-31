using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleOrdering.Domain.Entities
{
    /// <summary>
    /// Clien which place orders in this application.
    /// </summary>
    public class Client : Entity
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        /// <summary>
        /// Id of balance entity, for more info see <see cref="Balance"/>.
        /// </summary>
        public Guid BalanceId { get; set; }

        /// <summary>
        /// Id of order.
        /// Can be null if client haven't yet order.
        /// </summary>
        public Guid? OrderId { get; set; }
    }
}
