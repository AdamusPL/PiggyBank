﻿namespace PiggyBank.Server.Models
{
    public class Users
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public int? RoomUserId { get; set; }
    }
}
