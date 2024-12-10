using Microsoft.AspNetCore.Mvc;
using PiggyBank.Models;
using PiggyBank.Server.Dtos;
using PiggyBank.Server.Models;
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
        public IEnumerable<Room> GetUserRooms([FromQuery] int userId)
        {
            IEnumerable<Room> items = _itemsService.GetRoomExpenses(userId);
            return items;
        }

        [HttpPost("AddItem", Name = "AddItem")]
        public IActionResult AddItem([FromBody] ItemDto itemDto)
        {
            int id = _itemsService.AddItem(itemDto);
            return Ok(new { id = id });
        }

        [HttpPost("AddExpense", Name = "AddExpense")]
        public IActionResult AddExpense([FromBody] ExpenseDto expenseDto)
        {
            int id = _itemsService.AddExpense(expenseDto);
            return Ok(new { id = id });

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
