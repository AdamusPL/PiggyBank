﻿using Microsoft.AspNetCore.Mvc;
using PiggyBank.Server.Dtos;
using PiggyBank.Services;

namespace PiggyBank.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemsController : Controller
    {
        private readonly IItemsService _itemsService;
        public ItemsController(IItemsService itemsService)
        {
            _itemsService = itemsService;
        }

        [HttpGet("GetRoomExpenses", Name = "GetRoomExpenses")]
        public IEnumerable<RoomPrintDto> GetUserRooms([FromQuery] int userId)
        {
            return _itemsService.GetRoomExpenses(userId);
        }

        [HttpPost("AddItem", Name = "AddItem")]
        public IActionResult AddItem([FromBody] ItemDto itemDto)
        {
            return Ok(new { id = _itemsService.AddItem(itemDto) });
        }

        [HttpPost("AddExpense", Name = "AddExpense")]
        public IActionResult AddExpense([FromBody] ExpenseDto expenseDto)
        {
            return Ok(new { id = _itemsService.AddExpense(expenseDto) });
        }

        [HttpPost("RemoveItem", Name = "RemoveItem")]
        public IActionResult RemoveItem([FromQuery] int itemId)
        {
            _itemsService.RemoveItem(itemId);
            return Ok(new { message = "Successfully removed item" });
        }

        [HttpPost("RemoveExpense", Name = "RemoveExpense")]
        public IActionResult RemoveExpense([FromQuery] int expenseId)
        {
            _itemsService.RemoveExpense(expenseId);
            return Ok(new { message = "Successfully removed item" });
        }

    }
}
