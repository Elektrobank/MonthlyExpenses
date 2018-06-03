using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NPoco;

namespace MonthlyBills.Models
{
    [PrimaryKey("Name")]
    public class ExpenseDueDates
    {
        public string Name { get; set; }
        public int DueDate { get; set; }
    }
}