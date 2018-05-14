using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MonthlyExpenses.Models;
using NPoco;

namespace MonthlyExpenses.Controllers
{
    public class MonthlyExpensesController : Controller
    {
        Database db = new Database("Finances");
        public List<SqlModel> GetYearExpenses(string year)
        {
            return db.Fetch<SqlModel>($"SELECT [Date] ,[Name] ,[Cost] ,[Category] FROM [Finances].[dbo].[MonthlyExpenses] WHERE YEAR(Date) = '{year}'");
        }

        public ActionResult Index()
        {
            return View();
        }

        [Route("{MonthlyExpenses}/{YearSummary}/{year}")]
        public ActionResult YearSummary(string year)
        {
            var vm = new YearSummaryViewModel();
            vm.Year = year;
            var expenses = GetYearExpenses(year);
            vm.Months = new List<string> { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };

            foreach (var e in expenses.GroupBy(g => new
            {
                g.Name,
                g.Category
            }).ToList())
            {
                var currentExpense = new Expense();
                currentExpense.Name = e.Key.Name;
                currentExpense.Category = e.Key.Category;

                currentExpense.MonthlyAmountPaid = expenses
                    .Where(n => n.Name == e.Key.Name)
                    .Select(n => new AmountPaid()
                    {
                        Date = n.Date,
                        Amount = n.Cost
                    })
                    .OrderBy(n => n.Date)
                    .ToList();

                vm.Expenses.Add(currentExpense);
            }


            return View(vm);
        }
    }
}