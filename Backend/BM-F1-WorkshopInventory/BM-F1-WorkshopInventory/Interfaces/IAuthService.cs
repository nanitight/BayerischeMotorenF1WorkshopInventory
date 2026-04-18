using BM_F1_WorkshopInventory.Models.DTO;
using BM_F1_WorkshopInventory.Models.Entities;

namespace BM_F1_WorkshopInventory.Interfaces
{
    public interface IAuthService
    {
        Task<User?> RegisterAsync(UserDTO user);

        Task<string?> LoginAsync(UserDTO user);
    }
}
