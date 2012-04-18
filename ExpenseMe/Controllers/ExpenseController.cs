using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using ExpenseMe.Models;
using ExpenseMe.Domain;

namespace ExpenseMe.Controllers {

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
            ExpenseMeModelContainer context = new ExpenseMeModelContainer();
            var data = context.Expenses.OrderByDescending(e => e.ExpenseDate).ToList();
            foreach (Expense e in data) {
                expenses.Add(new ViewExpense()
                {
                    ExpenseId = e.ExpenseId,
                    Description = e.Description,
                    ExpenseDate = e.ExpenseDate.Value,
                    FormattedExpenseDate = e.ExpenseDate.Value.ToString("MM/dd/yyyy HH:mm"),
                    Spent = e.Spent.Value
                });
            }
            return this.Json(expenses, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /Expense/Details/5
        public JsonResult Details(int id) {
            ExpenseMeModelContainer context = new ExpenseMeModelContainer();
            var expenseresult = context.Expenses.SingleOrDefault(e => e.ExpenseId == id);
            ViewExpense expense = new ViewExpense()
            {
                Description = expenseresult.Description,
                ExpenseDate = expenseresult.ExpenseDate.Value,
                ExpenseId = expenseresult.ExpenseId,
                Spent = expenseresult.Spent.Value
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
                int result;
                ExpenseMeModelContainer context = new ExpenseMeModelContainer();
                Expense e = context.Expenses.CreateObject();
                {
                    e.Description = expense.Description;
                    e.Spent = expense.Spent;
                    e.ExpenseDate = expense.ExpenseDate;
                    e.ExpenseId = 0;
                }
                //save to db
                context.Expenses.AddObject(e);
                result=context.SaveChanges();
                return this.Json(new TransactionResult() { IsError = true, StatusDescription = "Expense posted", Data = expense });
            }
            catch (Exception ex) {
                return this.Json(new TransactionResult() { IsError = true, StatusDescription = ex.Message });
            }
        }

        // POST: /Expense/Create
        [HttpPost]
        public JsonResult CreateBatch(IEnumerable<ViewExpense> expenses) {
            try {
                // TODO: Add insert logic here
                //create a new Client object
                int result;
                ExpenseMeModelContainer context = new ExpenseMeModelContainer();
                foreach (ViewExpense vw in expenses) {
                    Expense e = context.Expenses.CreateObject();
                    {
                        e.Description = vw.Description;
                        e.Spent = vw.Spent;
                        e.ExpenseDate = vw.ExpenseDate;
                        e.ExpenseId = 0;
                    }
                    context.Expenses.AddObject(e);
                }
                //save to db
                result = context.SaveChanges();
                return this.Json(new TransactionResult() { IsError = true, StatusDescription = "Expenses posted", Data = expenses });
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
