namespace PiggyBank.Server.Models
{
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Password { get; set; }
        public ICollection<Expense> Expenses { get; set; }
        public ICollection<RoomUser> RoomUsers { get; set; }
    }
}
