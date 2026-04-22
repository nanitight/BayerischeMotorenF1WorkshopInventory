using BM_F1_WorkshopInventory.Models.DTO;

namespace BM_F1_WorkshopInventory.Interfaces
{
    public interface IUserService
    {
        public Task<UserInfoDTO?> GetUser(string id);
    }
}
