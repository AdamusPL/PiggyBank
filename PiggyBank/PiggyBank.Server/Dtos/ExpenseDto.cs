namespace PiggyBank.Server.Dtos
{
    public class ExpenseDto
    {
        public string Name {  get; set; }
        public DateTime PurchaseDate { get; set; }
        public int RoomId { get; set; }
    }
}
