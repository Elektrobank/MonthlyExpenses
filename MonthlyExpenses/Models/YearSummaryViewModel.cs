using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonthlyBills.Models
{
    public class YearSummaryViewModel
    {
        public int Year { get; set; }
        public List<Expense> Expenses { get; set; } = new List<Expense>();
    }
}