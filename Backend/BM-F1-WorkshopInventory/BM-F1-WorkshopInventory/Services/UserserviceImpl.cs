using BM_F1_WorkshopInventory.Data;
using BM_F1_WorkshopInventory.Interfaces;
using BM_F1_WorkshopInventory.Models.DTO;
using BM_F1_WorkshopInventory.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BM_F1_WorkshopInventory.Services
{
    public class UserserviceImpl : IUserService
    {
        private readonly BMF1DbContext _context;

        public UserserviceImpl(BMF1DbContext context)
        {
            _context = context;
        }
        public async Task<User?> GetUser(string id)
        {
            try
            {
                Guid checkId = Guid.Parse(id);
                User userDb = await _context.Users.FirstAsync<User>(e => e.Id == checkId);
                if (userDb == null) return null;
                return userDb;
            }
            catch
            {
                return null;
            }
        }
    }
}
