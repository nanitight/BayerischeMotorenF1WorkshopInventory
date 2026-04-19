using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BM_F1_WorkshopInventory.Data;
using BM_F1_WorkshopInventory.Models.Entities;
using Microsoft.AspNetCore.Authorization;

namespace BM_F1_WorkshopInventory.Controllers
{
    [Authorize(Roles = "ADMIN,USER")]
    [Route("api/[controller]")]
    [ApiController]
    public class GrandPrixResultsController : ControllerBase
    {
        private readonly BMF1DbContext _context;

        public GrandPrixResultsController(BMF1DbContext context)
        {
            _context = context;
        }

        // GET: api/GrandPrixResults
        [HttpGet]
        public async Task<ActionResult<List<GrandPrixResult>>> Index()
        {
            return Ok(await _context.GrandPrixResult.ToListAsync());
        }

        // GET: api/GrandPrixResults/Details/5
        [HttpGet("/Details/{id}")]
        public async Task<ActionResult<GrandPrixResult>> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grandPrixResult = await _context.GrandPrixResult
                .FirstOrDefaultAsync(m => m.Id == id);
            if (grandPrixResult == null)
            {
                return NotFound();
            }

            return Ok(grandPrixResult);
        }

        // POST: GrandPrixResults/Create
        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RaceDay,Location,PointsScored,PositionInTeamGrid")] GrandPrixResult grandPrixResult)
        {
            if (ModelState.IsValid)
            {
                grandPrixResult.Id = Guid.NewGuid();
                _context.Add(grandPrixResult);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return Ok(grandPrixResult);
        }

        // PUT: api/GrandPrixResults/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPut("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,RaceDay,Location,PointsScored,PositionInTeamGrid")] GrandPrixResult grandPrixResult)
        {
            if (id != grandPrixResult.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(grandPrixResult);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GrandPrixResultExists(grandPrixResult.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return Ok(grandPrixResult);
        }


        // DELETE: api/GrandPrixResults/Delete/5
        [Authorize(Roles = "ADMIN")]
        [HttpDelete, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var grandPrixResult = await _context.GrandPrixResult.FindAsync(id);
            if (grandPrixResult != null)
            {
                _context.GrandPrixResult.Remove(grandPrixResult);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GrandPrixResultExists(Guid id)
        {
            return _context.GrandPrixResult.Any(e => e.Id == id);
        }
    }
}
