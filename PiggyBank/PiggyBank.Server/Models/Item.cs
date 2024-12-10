using PiggyBank.Server.Models;

namespace PiggyBank.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int ExpenseId { get; set; }
        public Expense expense { get; set; }
    }
}
