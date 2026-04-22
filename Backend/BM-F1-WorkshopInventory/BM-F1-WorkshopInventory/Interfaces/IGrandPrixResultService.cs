using BM_F1_WorkshopInventory.Models.Entities;

namespace BM_F1_WorkshopInventory.Interfaces
{
    public interface IGrandPrixResultService
    {
        public Task<GrandPrixResult> CreateResult(GrandPrixResult newResult);
        public Task<GrandPrixResult> EditResult(Guid id,GrandPrixResult editResult);
        public Task<List<GrandPrixResult>> GetResults();
        public Task<bool> DeleteResult(Guid deleteId);
        public Task<GrandPrixResult?> GetResult(Guid id);
        
    }
}
