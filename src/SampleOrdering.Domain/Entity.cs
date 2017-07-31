using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleOrdering.Domain
{
    /// <summary>
    /// Base class for entities.
    /// </summary>
    [Serializable]
    public abstract class Entity
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public bool IsActivated { get; set; }
    }
}
