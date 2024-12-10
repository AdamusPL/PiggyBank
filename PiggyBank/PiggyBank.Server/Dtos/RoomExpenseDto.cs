using PiggyBank.Server.Models;

namespace PiggyBank.Server.Dtos
{
    public class RoomExpenseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Expense> Expenses { get; set; }
        public ICollection<RoomUser> RoomUsers { get; set; }
    }
}
