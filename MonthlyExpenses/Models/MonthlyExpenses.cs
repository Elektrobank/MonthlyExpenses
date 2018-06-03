using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NPoco;

namespace MonthlyBills.Models
{
    [PrimaryKey("Date, Name")]
    public class MonthlyExpenses
    {
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public double Cost { get; set; }
    }
}