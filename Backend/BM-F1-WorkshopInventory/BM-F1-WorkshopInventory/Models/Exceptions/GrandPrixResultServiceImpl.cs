using BM_F1_WorkshopInventory.Data;
using BM_F1_WorkshopInventory.Interfaces;
using BM_F1_WorkshopInventory.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BM_F1_WorkshopInventory.Models.Exceptions
{
    public class GrandPrixResultServiceImpl : IGrandPrixResultService
    {
        private readonly BMF1DbContext _context;

        public GrandPrixResultServiceImpl(BMF1DbContext context)
        {
            this._context = context;
        }
        public async Task<GrandPrixResult> CreateResult(GrandPrixResult newResult)
        {
            newResult.Id = Guid.NewGuid();
            _context.Add(newResult);
           _ = await _context.SaveChangesAsync();
            return newResult;
        }

        public async Task<bool> DeleteResult(Guid deleteId)
        {
            var grandPrixResult = await GetResult(deleteId);
            if (grandPrixResult == null) return false;

            _context.GrandPrixResult.Remove(grandPrixResult);

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<GrandPrixResult> EditResult(Guid id,GrandPrixResult editResult)
        {
            try
            {
                var existing = await GetResult(id);
                if (existing == null)
                    throw new ResultNotFoundException("GrandPrix Result not found.");

                existing.Location = editResult.Location;
                existing.RaceDay = editResult.RaceDay;
                existing.PositionInTeamGrid = editResult.PositionInTeamGrid;
                existing.PointsScored = editResult.PointsScored;

                _context.Update(existing);
                await _context.SaveChangesAsync();
                return existing;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (GetResult(editResult.Id) == null)
                {
                    throw new ResultNotFoundException("Id not found please try again.");
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<GrandPrixResult?> GetResult(Guid id)
        {
            try
            {
                return await _context.GrandPrixResult.FirstAsync(m => m.Id == id);
            }
            catch
            {
                return null;
            }

        }
        public Task<List<GrandPrixResult>> GetResults()
        {
            return _context.GrandPrixResult.ToListAsync();
        }

    }
}
