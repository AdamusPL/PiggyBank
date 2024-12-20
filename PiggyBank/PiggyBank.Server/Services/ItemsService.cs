using Microsoft.EntityFrameworkCore;
using PiggyBank.Models;
using PiggyBank.Repositories;
using PiggyBank.Server.Dtos;
using PiggyBank.Server.Models;
using System;

namespace PiggyBank.Services
{
    public interface IItemsService
    {
        List<RoomPrintDto> GetRoomExpenses(int roomUserId);
        int AddItem(ItemDto itemDto);
        int AddExpense(ExpenseDto expenseDto);
        void RemoveItem(int itemId);
        void RemoveExpense(int expenseId);
    }

    internal class ItemsService : IItemsService
    {
        private readonly IItemsRepository _itemsRepository;
        private readonly AppDbContext _appDbContext;
        public ItemsService(IItemsRepository itemsRepository, AppDbContext appDbContext)
        {
            _itemsRepository = itemsRepository;
            _appDbContext = appDbContext;
        }

        public List<RoomPrintDto> GetRoomExpenses(int roomUserId)
        {
            List<RoomPrintDto> list = new List<RoomPrintDto>();

            var rooms = _appDbContext.Room
                .Where(r => r.Room_RoomUsers.Any(ru => ru.RoomUserId == roomUserId))
                .Include(r => r.Expenses)
                    .ThenInclude(e => e.Items)
                .ToList();

            foreach (var room in rooms)
            {
                double SumExpenses = room.Expenses?.Sum(e => e.Items.Sum(i => i.Price)) ?? 0;
                RoomPrintDto roomPrintDto = new RoomPrintDto(room.Id, room.Name, Math.Round(SumExpenses, 2));

                foreach (var expense in room.Expenses)
                {
                    double SumItems = expense.Items?.Sum(i => i.Price) ?? 0;
                    roomPrintDto.Expenses.Add(new ExpensePrintDto(expense.Id, expense.Name, expense.PurchaseDate.ToString("MM/dd/yyyy"), expense.Items, Math.Round(SumItems, 2)));
                }

                list.Add(roomPrintDto);

            }

            return list;
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
