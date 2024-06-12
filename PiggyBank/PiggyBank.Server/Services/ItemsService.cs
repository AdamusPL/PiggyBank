﻿using PiggyBank.Models;
using PiggyBank.Repositories;
using PiggyBank.Server.Dtos;

namespace PiggyBank.Services
{
    public interface IItemsService
    {
        List<Item> GetItems();
        List<RoomExpenseDto> GetRoomExpenses(int userId);
        void AddItem(Item item);
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

        public List<RoomExpenseDto> GetRoomExpenses(int userId)
        {
            using (var dbContext = new DbContext())
            {
                return _itemsRepository.GetRoomExpenses(userId);
            }
        }

        public void AddItem(Item item)
        {
            _itemsRepository.AddItem(item);
        }
    }
}
