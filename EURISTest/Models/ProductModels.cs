using EURIS.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EURISTest.Models
{

    public class SelectedCatalogs
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public int Checked { get; set; }
    }

    public class ProductModels
    {

        public Product product { get; set; }

        public IEnumerable<Product> listOfProducts { get; set; }

        public List<SelectedCatalogs> _SelectedCatalogs { get; set; }

    }
}