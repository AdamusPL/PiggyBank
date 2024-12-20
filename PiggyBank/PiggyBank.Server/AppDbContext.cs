using Microsoft.EntityFrameworkCore;
using PiggyBank.Models;
using PiggyBank.Server.Dtos;
using PiggyBank.Server.Models;

namespace PiggyBank
{
    public class AppDbContext : DbContext
    {
        public DbSet<Item> Item { get; set; }
        public DbSet<Room> Room { get; set; }
        public DbSet<Room_RoomUser> Room_RoomUser { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Expense> Expense { get; set; }
        public DbSet<RoomUser> RoomUser {  get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public void AddRoomUserToRoom(int roomId, int roomUserId)
        {
            var roomRoomUser = new Room_RoomUser
            {
                RoomId = roomId,
                RoomUserId = roomUserId
            };

            Room_RoomUser.Add(roomRoomUser);

            SaveChanges();
        }

        public void RemoveRoomUserFromRoom(int roomId, int roomUserId)
        {
            var roomRoomUser = new Room_RoomUser
            {
                RoomId = roomId,
                RoomUserId = roomUserId
            };

            Room_RoomUser.Remove(roomRoomUser);

            SaveChanges();
        }

        public int AddItem(ItemDto itemDto)
        {
            Item item = new Item(itemDto.Name, itemDto.Price, itemDto.ExpenseId);
            Item.Add(item);
            SaveChanges();
            return item.Id;
        }

        public int AddExpense(ExpenseDto expenseDto)
        {
            Expense expense = new Expense(expenseDto.Name, expenseDto.PurchaseDate, expenseDto.RoomId);
            Expense.Add(expense);
            SaveChanges();
            return expense.Id;
        }

        public void RemoveExpense(int expenseId)
        {
            var expense = Expense.Where(e => e.Id == expenseId).FirstOrDefault();
            var items = Item.Where(i => i.ExpenseId == expenseId);
            if (items != null)
            {
                foreach (var item in items) {
                    Item.Remove(item);
                }
            }
            SaveChanges();
            if (expense != null)
            {
                Expense.Remove(expense);
            }
            SaveChanges();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Room_RoomUser>()
                .HasKey(rr => new { rr.RoomId, rr.RoomUserId });
        }
    }
}
