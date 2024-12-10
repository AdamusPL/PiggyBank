using PiggyBank.Models;
using PiggyBank.Server.Dtos;
using PiggyBank.Server.Models;

namespace PiggyBank.Repositories
{
    internal interface IItemsRepository
    {
        int AddItem(ItemDto itemDto);
        int AddExpense(ExpenseDto expenseDto);
        void RemoveItem(int itemId);
        void RemoveExpense(int expenseId);
    }

    internal class ItemsRepository : IItemsRepository
    {
        public int AddItem(ItemDto itemDto)
        {
            int id;
            using (var dbContext = new DbContext())
            {
                id = dbContext.AddItem(itemDto);
            }
            return id;
        }

        public int AddExpense(ExpenseDto expenseDto)
        {
            int id;
            using (var dbContext = new DbContext())
            {
                id = dbContext.AddExpense(expenseDto);
            }
            return id;
        }

        public void RemoveItem(int itemId)
        {
            using (var dbContext = new DbContext())
            {
                var entity = dbContext.Item.Find(itemId);
                dbContext.Item.Remove(entity);
                dbContext.SaveChanges();
            }
        }

        public void RemoveExpense(int expenseId)
        {
            using (var dbContext = new DbContext())
            {
                dbContext.RemoveExpense(expenseId);
            }
        }
        
    }
}
