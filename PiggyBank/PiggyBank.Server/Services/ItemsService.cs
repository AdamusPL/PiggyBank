using Microsoft.EntityFrameworkCore;
using PiggyBank.Models;
using PiggyBank.Repositories;
using PiggyBank.Server.Dtos;
using PiggyBank.Server.Models;

namespace PiggyBank.Services
{
    public interface IItemsService
    {
        List<Item> GetItems();
        List<Room> GetRoomExpenses(int roomUserId);
        int AddItem(Item item);
        int AddExpense(Expense expense);
        void RemoveItem(Item item);
        void RemoveExpense(int expenseId);
    }

    internal class ItemsService : IItemsService
    {
        private readonly IItemsRepository _itemsRepository;
        public ItemsService(IItemsRepository itemsRepository) {
            _itemsRepository = itemsRepository;
        }

        public List<Item> GetItems()
        {
            return _itemsRepository.GetItems(); 
        }

        public List<Room> GetRoomExpenses(int roomUserId)
        {
            using (var context = new DbContext())
            {
                return context.Room
                    .Where(r => r.RoomUsers.Any(ru => ru.Id == roomUserId))
                    .Include(r => r.Expenses)
                        .ThenInclude(e => e.Items)
                    .ToList();
            }
        }

        public int AddItem(Item item)
        {
            return _itemsRepository.AddItem(item);
        }

        public int AddExpense(Expense expense)
        {
            return _itemsRepository.AddExpense(expense);
        }

        public void RemoveItem(Item item)
        {
            _itemsRepository.RemoveItem(item);
        }

        public void RemoveExpense(int expenseId)
        {
            _itemsRepository.RemoveExpense(expenseId);
        }
    }
}
