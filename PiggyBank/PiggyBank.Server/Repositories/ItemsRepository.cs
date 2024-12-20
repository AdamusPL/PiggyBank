using PiggyBank.Server.Dtos;

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
        private readonly AppDbContext _dbContext;

        public ItemsRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int AddItem(ItemDto itemDto)
        {
            return _dbContext.AddItem(itemDto);
        }

        public int AddExpense(ExpenseDto expenseDto)
        {
            return _dbContext.AddExpense(expenseDto);
        }

        public void RemoveItem(int itemId)
        {
            var entity = _dbContext.Item.Find(itemId);
            _dbContext.Item.Remove(entity);
            _dbContext.SaveChanges();
        }

        public void RemoveExpense(int expenseId)
        {
            _dbContext.RemoveExpense(expenseId);
        }

    }
}
