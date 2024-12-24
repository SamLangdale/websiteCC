using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using websiteCCreal.Models;

namespace websiteCCreal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly ExpensesDbContex _contex;
        public HomeController(ILogger<HomeController> logger, ExpensesDbContex contex)
        {
            _logger = logger;
            _contex = contex;
        }
        public IActionResult Expenses()
        {
            var allExpenses = _contex.Expenses.ToList();

            var totalExpenses = allExpenses.Sum(x => x.Value);

            ViewBag.Expenses = totalExpenses;

            return View(allExpenses);
        }
        public IActionResult CreateEditExpense(int? id)
        {
            if(id != null)
            {
                // editing -> load
                var expenseInDb = _contex.Expenses.SingleOrDefault(expense => expense.Id == id);
                return View(expenseInDb);
            }

            
            return View();
        }
        public IActionResult DeleteExpense(int id)
        {
            var expenseInDb = _contex.Expenses.SingleOrDefault(expense => expense.Id == id);
            _contex.Expenses.Remove(expenseInDb);
            _contex.SaveChanges();
            return RedirectToAction("Expenses");
        }
        public IActionResult CreateEditExpenseForm(Expense model)
        {
            if(model.Id == 0)
            {
                // adding
                _contex.Expenses.Add(model);
            }
            else
            {
                // editing
                _contex.Expenses.Update(model);
            }
            
            _contex.SaveChanges();


            return RedirectToAction("Expenses");
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
