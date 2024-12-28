using Microsoft.AspNetCore.Mvc;
using PiggyBank.Server.Dtos;
using PiggyBank.Server.Models;
using PiggyBank.Server.Services;

namespace PiggyBank.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoomsController : Controller
    {
        private readonly IRoomsService _roomsService;
        public RoomsController(IRoomsService roomsService)
        {
            _roomsService = roomsService;
        }
        
        [HttpGet(Name = "GetRooms")]
        public IEnumerable<Room> Get()
        {
            return _roomsService.GetRooms();
        }

        [HttpGet("GetUserRooms", Name = "GetUserRooms")]
        public IEnumerable<Room_RoomUser> GetUserRooms([FromQuery] int userId)
        {
            return _roomsService.GetUserRooms(userId);
        }

        [HttpPost("create", Name = "CreateRoom")]
        public IActionResult CreateRoom([FromBody] NewRoomDto room) {
            _roomsService.CreateRoom(room);
            return Ok(new { message = "Successfully created room" });
        }

        [HttpPost("join", Name = "JoinRoom")]
        public IActionResult JoinRoom([FromBody] RoomOperationDto roomOperationDto)
        {
            _roomsService.JoinRoom(roomOperationDto);
            return Ok(new { message = "Successfully joined room" });
        }

        [HttpPost("leave", Name = "LeaveRoom")]
        public IActionResult LeaveRoom([FromBody] RoomOperationDto roomOperationDto)
        {
            _roomsService.LeaveRoom(roomOperationDto);
            return Ok(new { message = "Successfully joined room" });
        }

        [HttpPost("checkPassword", Name = "CheckRoomPassword")]
        public IActionResult CheckPassword([FromBody] PasswordDto passwordDto)
        {
            bool isCorrect = _roomsService.CheckPassword(passwordDto);
            if (isCorrect)
            {
                return Ok();
            }
            else
            {
                return Forbid();
            }
        }
    }
}
