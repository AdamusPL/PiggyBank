using PiggyBank.Server.Models;

namespace PiggyBank.Server.Dtos
{
    public class RoomPrintDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<ExpensePrintDto> Expenses { get; set; }
        public double SumExpenses { get; set; }

        public RoomPrintDto(int id, string name, double sumExpenses) 
        {
            Id = id;
            Name = name;
            SumExpenses = SumExpenses;
            Expenses = new List<ExpensePrintDto>();
        }
    }
}
