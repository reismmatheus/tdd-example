using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TddExample.Domain
{
    public class Entity
    {
        public virtual Guid Id { get; set; }
        public virtual DateTime CreatedIn { get; set; }
        public virtual DateTime? UpdatedIn { get; set; }
        public virtual bool IsDeleted { get; set; }
    }
}
