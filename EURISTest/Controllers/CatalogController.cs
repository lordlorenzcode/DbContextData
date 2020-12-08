using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EURIS.Service;
using EURIS.Entities;
using EURIS.Service.Repository;
using EURIS.Entities.Entities;
using EURISTest.Models;

namespace EURISTest.Controllers
{
    public class CatalogController : Controller
    {

        private IContextRepository<Product> _product;
        private IContextRepository<Catalog> _catalog;
        private IContextRepository<ProductsCatalogs> _productscatalogs;

        public CatalogController(IContextRepository<Product> product, 
                                 IContextRepository<Catalog> catalog,
                                 IContextRepository<ProductsCatalogs> productscatalogs)
        {
            this._product = product;
            this._catalog = catalog;
            this._productscatalogs = productscatalogs;
        }

        public ActionResult Index()
        {
            CatalogModels model = new CatalogModels();

            model.listOfCatalogs = this._catalog.GetAll();

            return View(model);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            CatalogModels model = new CatalogModels();

            model.catalog = _catalog.GetById(id);

            IEnumerable<Product> products = _product.GetAll();

            List<SelectedProducts> selProds = new List<SelectedProducts>();

            foreach (var pro in products)
            {
                SelectedProducts prods = new SelectedProducts();
                prods.Checked = _productscatalogs.GetAll().Where(c => c.Catalog.Id == id).Where(c => c.Product.Id == pro.Id).Any() ? 1 : 0;
                prods.Id = pro.Id;
                prods.Code = pro.Code;
                prods.Description = pro.Description;
                selProds.Add(prods);
            }

            model._SelectedProducts = selProds;

            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            CatalogModels model = new CatalogModels();

            model.catalog = _catalog.GetById(id);

            IEnumerable<Product> products = _product.GetAll();

            List<SelectedProducts> selProds = new List<SelectedProducts>();

            foreach (var pro in products)
            {
                SelectedProducts prods = new SelectedProducts();
                prods.Checked = _productscatalogs.GetAll().Where(c=> c.Catalog.Id==id).Where(c=> c.Product.Id == pro.Id).Any()?1:0;
                prods.Id = pro.Id;
                prods.Code = pro.Code;
                prods.Description = pro.Description;
                selProds.Add(prods);
            }

            model._SelectedProducts = selProds;

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(CatalogModels model)
        {

            if (ModelState.IsValid)
            {

                Catalog catalog = _catalog.GetById(model.catalog.Id);

                try
                {

                    if (model.catalog != null)
                    {
                        catalog.Code = model.catalog.Code;
                        catalog.Description = model.catalog.Description;
                        _catalog.Save();
                    }

                    if(model._SelectedProducts != null)
                    {
                        foreach(var sel in model._SelectedProducts)
                        {

                            ProductsCatalogs procals = _productscatalogs.GetAll().Where(c => c.Catalog.Id == model.catalog.Id).Where(c => c.Product.Id == sel.Id).FirstOrDefault();

                            if (sel.Checked == 0 && procals != null)
                            {
                                _productscatalogs.Delete(procals);
                                _productscatalogs.Save();
                            }
                            else if(sel.Checked == 1 && procals == null)
                            {
 
                                ProductsCatalogs procalen = new ProductsCatalogs();
                                Product pro = _product.GetById(sel.Id);
                                procalen.Product = pro;
                                procalen.Catalog = catalog;
                                _productscatalogs.Insert(procalen);
                                _productscatalogs.Save();
                                
                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    //...
                }
            }

            return RedirectToAction("Index");

        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CatalogModels model)
        {

            if (ModelState.IsValid)
            {

                Catalog catalog = new Catalog();

                try
                {

                    if (model.catalog != null)
                    {
                        catalog.Code = model.catalog.Code;
                        catalog.Description = model.catalog.Description;
                        _catalog.Insert(catalog);
                        _catalog.Save();
                    }

                }
                catch (Exception ex)
                {
                    //...
                }
            }

            return RedirectToAction("Index");

        }


        public ActionResult Delete(int id)
        {

            try
            {
                List<ProductsCatalogs> procals = _productscatalogs.GetAll().Where(p => p.Catalog.Id == id).ToList();
                foreach (var el in procals)
                {
                    _productscatalogs.Delete(el);
                }
                _productscatalogs.Save();
                Catalog catalog = _catalog.GetById(id);
                _catalog.Delete(catalog);
                _catalog.Save();
            }
            catch (Exception ex)
            {

            }

            return RedirectToAction("Index");
        }
        
    }
}
