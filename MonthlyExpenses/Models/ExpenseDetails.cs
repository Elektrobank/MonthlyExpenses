using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NPoco;

namespace MonthlyBills.Models
{
    [PrimaryKey("Name, DueDate")]
    public class ExpenseDetails
    {
        public string Name { get; set; }
        public int? DueDate { get; set; }
        public bool AutoPay { get; set; }
        public string Category { get; set; }
        public string Icon { get; set; }
    }
}