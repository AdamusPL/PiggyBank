using PiggyBank.Models;

namespace PiggyBank.Server.Dtos
{
    public class ExpensePrintDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PurchaseDate { get; set; }
        public ICollection<Item> Items { get; set; }
        public double SumItems { get; set; }

        public ExpensePrintDto(int id, string name, string purchaseDate, ICollection<Item> items, double sumItems) 
        {
            Id = id;
            Name = name;
            PurchaseDate = purchaseDate;
            Items = items;
            SumItems = sumItems;
        }
    }
}
