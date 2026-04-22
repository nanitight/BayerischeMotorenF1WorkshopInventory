using BM_F1_WorkshopInventory.Models.Entities;

namespace BM_F1_WorkshopInventory.Interfaces
{
    public interface IUserService
    {
        public Task<User?> GetUser(string id);
    }
}
