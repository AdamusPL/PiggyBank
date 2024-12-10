using PiggyBank.Models;

namespace PiggyBank.Server.Models
{
    public class Expense
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime PurchaseDate { get; set; }
        public int RoomId { get; set; }
        public ICollection<Item> Items { get; set; }
    }
}
