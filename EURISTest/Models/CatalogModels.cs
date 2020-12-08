using EURIS.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EURISTest.Models
{
    public class SelectedProducts
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public int Checked { get; set; }
    }

    public class CatalogModels
    {

        public Catalog catalog { get; set; }

        public IEnumerable<Catalog> listOfCatalogs { get; set; }

        public List<SelectedProducts> _SelectedProducts { get; set; }

    }
}