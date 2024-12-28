namespace PiggyBank.Server.Dtos
{
    public class PasswordDto
    {
        public string RoomPassword { get; set; }
        public string Salt { get; set; }
        public string PasswordEnteredByUser { get; set; }
    }
}
