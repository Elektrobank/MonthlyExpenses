using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonthlyExpenses.Models
{
    public class Expense
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public List<AmountPaid> MonthlyAmountPaid { get; set; } = new List<AmountPaid>(); 
    }
}