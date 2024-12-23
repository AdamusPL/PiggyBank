namespace PiggyBank.Server.Models
{
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Password { get; set; }
        public string? Salt { get; set; }
        public ICollection<Expense> Expenses { get; set; }
        public ICollection<Room_RoomUser> Room_RoomUsers { get; set; }
    }
}
