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
    public class ProductController : Controller
    {

        private IContextRepository<Product> _product;
        private IContextRepository<Catalog> _catalog;
        private IContextRepository<ProductsCatalogs> _productscatalogs;

        public ProductController(IContextRepository<Product> product, 
                                 IContextRepository<Catalog> catalog,
                                 IContextRepository<ProductsCatalogs> productscatalogs)
        {
            this._product = product;
            this._catalog = catalog;
            this._productscatalogs = productscatalogs;
        }

        public ActionResult Index()
        {
            ProductModels model = new ProductModels();

            model.listOfProducts = this._product.GetAll();

            return View(model);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            ProductModels model = new ProductModels();

            model.product = _product.GetById(id);

            IEnumerable<Catalog> catalogs = _catalog.GetAll();

            List<SelectedCatalogs> selCats = new List<SelectedCatalogs>();

            foreach (var cat in catalogs)
            {
                SelectedCatalogs catal = new SelectedCatalogs();
                catal.Checked = _productscatalogs.GetAll().Where(c => c.Product.Id == id).Where(c => c.Catalog.Id == cat.Id).Any() ? 1 : 0;
                catal.Id = cat.Id;
                catal.Code = cat.Code;
                catal.Description = cat.Description;
                selCats.Add(catal);
            }

            model._SelectedCatalogs = selCats;

            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            ProductModels model = new ProductModels();

            model.product = _product.GetById(id);

            IEnumerable<Catalog> catalogs = _catalog.GetAll();

            List<SelectedCatalogs> selCats = new List<SelectedCatalogs>();

            foreach (var cat in catalogs)
            {
                SelectedCatalogs catal = new SelectedCatalogs();
                catal.Checked = _productscatalogs.GetAll().Where(c=> c.Product.Id==id).Where(c=> c.Catalog.Id == cat.Id).Any()?1:0;
                catal.Id = cat.Id;
                catal.Code = cat.Code;
                catal.Description = cat.Description;
                selCats.Add(catal);
            }

            model._SelectedCatalogs = selCats;

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(ProductModels model)
        {

            if (ModelState.IsValid)
            {

                Product product = _product.GetById(model.product.Id);

                try
                {

                    if (model.product != null)
                    {
                        product.Code = model.product.Code;
                        product.Description = model.product.Description;
                        _product.Save();
                    }

                    if(model._SelectedCatalogs != null)
                    {
                        foreach(var sel in model._SelectedCatalogs)
                        {

                            ProductsCatalogs procals = _productscatalogs.GetAll().Where(c => c.Product.Id == model.product.Id).Where(c => c.Catalog.Id == sel.Id).FirstOrDefault();

                            if (sel.Checked == 0 && procals != null)
                            {
                                _productscatalogs.Delete(procals);
                                _productscatalogs.Save();
                            }
                            else if(sel.Checked == 1 && procals == null)
                            {
 
                                ProductsCatalogs procalen = new ProductsCatalogs();
                                Catalog cat = _catalog.GetById(sel.Id);
                                procalen.Catalog = cat;
                                procalen.Product = product;
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
        public ActionResult Create(ProductModels model)
        {

            if (ModelState.IsValid)
            {

                Product product = new Product();

                try
                {

                    if (model.product != null)
                    {
                        product.Code = model.product.Code;
                        product.Description = model.product.Description;
                        _product.Insert(product);
                        _product.Save();
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
                List<ProductsCatalogs> procals = _productscatalogs.GetAll().Where(p => p.Product.Id == id).ToList();
                foreach(var el in procals)
                {
                    _productscatalogs.Delete(el);
                }
                _productscatalogs.Save();
                Product product = _product.GetById(id);
                _product.Delete(product);
                _product.Save();
            }
            catch (Exception ex)
            {

            }

            return RedirectToAction("Index");
        }
        
    }
}
