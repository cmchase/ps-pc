using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ParentChild.DataLibrary;
using ParentChild.Model;

namespace ParentChild.Web.Controllers
{
    using ParentChild.Web.ViewModels;

    public class SalesController : Controller
    {
        private SalesContext _salesContext;

        public SalesController()
        {
            this._salesContext = new SalesContext();
        }

        // GET: Sales
        public ActionResult Index()
        {
            return View(this._salesContext.SalesOrders.ToList());
        }

        // GET: Sales/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalesOrder salesOrder = this._salesContext.SalesOrders.Find(id);
            if (salesOrder == null)
            {
                return HttpNotFound();
            }

            SalesOrderViewModel salesOrderViewModel = ViewModels.Helpers.CreateSalesOrderViewModelFromSalesOrder(salesOrder);
            salesOrderViewModel.MessageToClient = "Details view";

            return View(salesOrderViewModel);
        }

        // GET: Sales/Create
        public ActionResult Create()
        {
            SalesOrderViewModel salesOrderViewModel = new SalesOrderViewModel();
            salesOrderViewModel.ObjectState = ObjectState.Added;
            return View(salesOrderViewModel);
        }
        
        // GET: Sales/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalesOrder salesOrder = this._salesContext.SalesOrders.Find(id);
            if (salesOrder == null)
            {
                return HttpNotFound();
            }

            SalesOrderViewModel salesOrderViewModel = ViewModels.Helpers.CreateSalesOrderViewModelFromSalesOrder(salesOrder);

            salesOrderViewModel.MessageToClient  = string.Format("This is the original value of Customer Name is {0}", salesOrder.CustomerName);

            return View(salesOrderViewModel);
        }

        // POST: Sales/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SalesOrderId,CustomerName,PoNumber")] SalesOrder salesOrder)
        {
            if (ModelState.IsValid)
            {
                this._salesContext.Entry(salesOrder).State = EntityState.Modified;
                this._salesContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(salesOrder);
        }

        // GET: Sales/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalesOrder salesOrder = this._salesContext.SalesOrders.Find(id);
            if (salesOrder == null)
            {
                return HttpNotFound();
            }

            SalesOrderViewModel salesOrderViewModel = ViewModels.Helpers.CreateSalesOrderViewModelFromSalesOrder(salesOrder);
            salesOrderViewModel.MessageToClient = "You are about to delete this entry";

            return View(salesOrderViewModel);
        }

        // POST: Sales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SalesOrder salesOrder = this._salesContext.SalesOrders.Find(id);
            this._salesContext.SalesOrders.Remove(salesOrder);
            this._salesContext.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this._salesContext.Dispose();
            }
            base.Dispose(disposing);
        }

        public JsonResult Save(SalesOrderViewModel salesOrderViewModel)
        {
            SalesOrder salesOrder = ViewModels.Helpers.CreateSalesOrderFromSalesOrderViewModel(salesOrderViewModel);

            _salesContext.SalesOrders.Attach(salesOrder);
            _salesContext.ApplyStateChanges();
            _salesContext.SaveChanges();

            if (salesOrderViewModel.ObjectState == ObjectState.Deleted)
            {
                return Json(new { newLocation = "/Sales/Index/" });
            }

            string messageToClient = ViewModels.Helpers.GetMessageToClient(salesOrderViewModel.ObjectState, salesOrder.CustomerName);

            salesOrderViewModel = ViewModels.Helpers.CreateSalesOrderViewModelFromSalesOrder(salesOrder);

            salesOrderViewModel.MessageToClient = messageToClient;

            return Json(new { salesOrderViewModel });
        }
    }
}
