using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EURIS.Entities.Entities
{
    public class ProductsCatalogs
    {
        public int Id { get; set; }
        public virtual Catalog Catalog { get; set; }
        public virtual Product Product { get; set; }
    }
}
