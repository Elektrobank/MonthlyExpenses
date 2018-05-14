using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonthlyExpenses.Models
{
    public class SqlModel
    {
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public double Cost { get; set; }
    }
}