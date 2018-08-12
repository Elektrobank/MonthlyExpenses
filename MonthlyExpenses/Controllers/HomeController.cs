using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MonthlyBills.Models;
using NPoco;


namespace MonthlyBills.Controllers
{
    public class MonthlyBillsController : Controller
    {
        static Database db = new Database("Finances");
        public List<MonthlyExpenses> GetYearExpenses(int year)
        {
            return db.Fetch<MonthlyExpenses>($"SELECT [Date] ,[Name] ,[Amount] ,[Category] FROM [Finances].[dbo].[MonthlyExpenses] WHERE YEAR(Date) = '{year}'");
        }

        public static List<ExpenseDetails> GetExpenseDueDates()
        {
            return db.Fetch<ExpenseDetails>($"SELECT * FROM [Finances].[dbo].[ExpenseDetails]");
        }

        List<ExpenseDetails> dueDates = GetExpenseDueDates();
       
        [Route("MonthlyBills/YearSummary/{year?}")]
        public ActionResult YearSummary(int year = 2018)
        {
            var vm = new YearSummaryViewModel();
            vm.Year = year;
            var expenses = GetYearExpenses(year);

            foreach (var e in expenses.GroupBy(g => new { g.Name, g.Category }).ToList())
            {
                var currentExpense = new Expense();
                currentExpense.Name = e.Key.Name;
                currentExpense.Category = e.Key.Category;
                currentExpense.DueDate = dueDates.Where(x => x.Name == e.Key.Name).Select(x => x.DueDate).FirstOrDefault() ;
                currentExpense.AutoPay = dueDates.Where(x => x.Name == e.Key.Name).Select(x => x.AutoPay).FirstOrDefault();
                currentExpense.Icon = dueDates.Where(x => x.Name == e.Key.Name).Select(x => x.Icon).FirstOrDefault();
                currentExpense.MonthlyAmountPaid = expenses
                    .Where(n => n.Name == e.Key.Name)
                    .Select(n => new AmountPaid()
                    {
                        Date = n.Date,
                        Amount = n.Amount
                    })
                    .OrderBy(n => n.Date)
                    .ToList();

                vm.Expenses.Add(currentExpense);
            }

            return View(vm);
        }


        [HttpPost]
        public ActionResult Update(DateTime date, string name, string category, double amount, int? dueDate)
        {
            if (!dueDates.Any(n => n.Name == name))
            {
                var newExpenseDueDate = new ExpenseDetails { Name = name, DueDate = dueDate };
                db.Save(newExpenseDueDate);
            }

            var formData = new MonthlyExpenses { Date = date, Name = name, Category = category, Amount = amount };
            db.Save(formData);

            var returnYear = date.Year > DateTime.Now.Year ? DateTime.Now.Year : date.Year;

            return RedirectToAction("YearSummary/" + returnYear);
        }

    }
}