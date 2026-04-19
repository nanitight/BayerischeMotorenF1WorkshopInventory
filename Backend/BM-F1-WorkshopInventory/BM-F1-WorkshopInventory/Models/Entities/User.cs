using Microsoft.AspNetCore.Identity;

namespace BM_F1_WorkshopInventory.Models.Entities
{
    public class User 
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Role { get; set; } = "USER";

    }
}
