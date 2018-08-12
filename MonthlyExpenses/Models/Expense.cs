using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonthlyBills.Models
{
    public class Expense
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public int? DueDate { get; set; }
        public bool AutoPay { get; set; }
        public string Icon { get; set; }
        public List<AmountPaid> MonthlyAmountPaid { get; set; } = new List<AmountPaid>(); 
    }
}