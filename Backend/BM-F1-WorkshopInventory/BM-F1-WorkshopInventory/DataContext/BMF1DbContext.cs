using BM_F1_WorkshopInventory.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BM_F1_WorkshopInventory.Data
{
    public class BMF1DbContext : DbContext
    {
        public BMF1DbContext(DbContextOptions<BMF1DbContext> options) : base(options){ }

        public DbSet<User> Users { get; set; }
    }
}
