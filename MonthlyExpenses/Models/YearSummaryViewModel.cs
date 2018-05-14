using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonthlyExpenses.Models
{
    public class YearSummaryViewModel
    {
        public string Year { get; set; }
        public List<Expense> Expenses { get; set; } = new List<Expense>();
        public List<string> Months { get; set; }
    }
}