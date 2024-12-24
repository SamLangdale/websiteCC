using Microsoft.EntityFrameworkCore;

namespace websiteCCreal.Models
{
    public class ExpensesDbContex : DbContext
    {
        public DbSet<Expense> Expenses {  get; set; }

        public ExpensesDbContex(DbContextOptions<ExpensesDbContex> options)
        : base(options) 
        {

        }
    }
}
