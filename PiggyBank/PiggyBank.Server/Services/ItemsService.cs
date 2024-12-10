using Microsoft.EntityFrameworkCore;
using PiggyBank.Models;
using PiggyBank.Repositories;
using PiggyBank.Server.Dtos;
using PiggyBank.Server.Models;

namespace PiggyBank.Services
{
    public interface IItemsService
    {
        List<Room> GetRoomExpenses(int roomUserId);
        int AddItem(ItemDto itemDto);
        int AddExpense(ExpenseDto expenseDto);
        void RemoveItem(int itemId);
        void RemoveExpense(int expenseId);
    }

    internal class ItemsService : IItemsService
    {
        private readonly IItemsRepository _itemsRepository;
        public ItemsService(IItemsRepository itemsRepository) {
            _itemsRepository = itemsRepository;
        }

        public List<Room> GetRoomExpenses(int roomUserId)
        {
            using (var context = new DbContext())
            {
                return context.Room
                    .Where(r => r.Room_RoomUsers.Any(ru => ru.RoomUserId == roomUserId))
                    .Include(r => r.Expenses)
                        .ThenInclude(e => e.Items)
                    .ToList();
            }
        }

        public int AddItem(ItemDto itemDto)
        {
            return _itemsRepository.AddItem(itemDto);
        }

        public int AddExpense(ExpenseDto expenseDto)
        {
            return _itemsRepository.AddExpense(expenseDto);
        }

        public void RemoveItem(int itemId)
        {
            _itemsRepository.RemoveItem(itemId);
        }

        public void RemoveExpense(int expenseId)
        {
            _itemsRepository.RemoveExpense(expenseId);
        }
    }
}
