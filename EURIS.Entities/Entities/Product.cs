using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EURIS.Entities.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public virtual IQueryable<ProductsCatalogs> ProductsCatalogs { get; set; }

    }
}
