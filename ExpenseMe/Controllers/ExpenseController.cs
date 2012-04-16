using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using ExpenseMe.Models;
using ExpenseMe.Domain;

namespace ExpenseMe.Controllers {
    [Authorize]
    public class ExpenseController : Controller {

        public static void RegisterRoutes(RouteCollection routes) {
            routes.MapRoute(
                "Expense", // Route name
                "Expense/{action}/{id}", // URL with parameters
                new { controller = "Expense", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );
        }

        //
        // GET: /Expense/

        public JsonResult Index() {
            List<ViewExpense> expenses = new List<ViewExpense>();
            return this.Json(expenses);
        }

        //
        // GET: /Expense/Details/5
        public JsonResult Details(int id) {
            ViewExpense expense = new ViewExpense()
            {
                Description="Kuma treat",
                ExpenseDate=DateTime.Today,
                ExpenseId=888,
                Spent=1000.10
            };
            return this.Json(expense,JsonRequestBehavior.AllowGet);
        }

        //
        // POST: /Expense/Create
        [HttpPost]
        public JsonResult Create(ViewExpense expense) {
            try {
                // TODO: Add insert logic here
                //create a new Client object
                ExpenseMeModelContainer context = new ExpenseMeModelContainer();
                Expense e = new Expense();
                e.Description = expense.Description;
                //save to db
                context.AddToExpenses(e);
                context.SaveChanges();
                return this.Json(expense);
            }
            catch (Exception ex) {
                return this.Json(new TransactionResult() { IsError = true, StatusDescription = ex.Message });
            }
        }


        //
        // POST: /Expense/Edit/5
        [HttpPost]
        public JsonResult Edit(ViewExpense expense) {
            try {
                // TODO: Add update logic here

                return this.Json(expense);
            }
            catch (Exception ex) {
                return this.Json(new TransactionResult() { IsError = true, StatusDescription = ex.Message });
            }
        }

        //
        // POST: /Expense/Delete/5
        [HttpPost]
        public JsonResult Delete(int id) {
            try {
                // TODO: Add delete logic here

                return this.Json(new TransactionResult() { IsError = false, StatusDescription = "OK" });
            }
            catch (Exception ex) {
                return this.Json(new TransactionResult() { IsError = true, StatusDescription = ex.Message });
            }
        }
    }
}
