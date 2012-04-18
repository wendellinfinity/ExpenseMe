using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExpenseMe.Models {
    public class ViewExpense {
        public int ExpenseId { get; set; }
        public string Description { get; set; }
        public DateTime ExpenseDate { get; set; }
        public double Spent { get; set; }
        public string FormattedExpenseDate { get; set; }
    }
}