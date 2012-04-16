using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExpenseMe.Models {
    public struct TransactionResult {
        public string StatusDescription { get; set; }
        public bool IsError { get; set; }
        public object Data { get; set; }
    }
}