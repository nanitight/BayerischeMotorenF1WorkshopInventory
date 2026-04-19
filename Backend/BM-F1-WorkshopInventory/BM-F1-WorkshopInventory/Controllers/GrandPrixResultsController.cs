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
        private readonly double MAX_POINTS = 43.0;
        private readonly int MAX_TEAM_POS = 10;

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
        [HttpGet("Details/{id}")]
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
        public async Task<ActionResult<GrandPrixResult>> Create([Bind("Id,RaceDay,Location,PointsScored,PositionInTeamGrid")] GrandPrixResult grandPrixResult)
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
        public async Task<ActionResult<GrandPrixResult>> Edit(Guid? id, [Bind("RaceDay,Location,PointsScored,PositionInTeamGrid")] GrandPrixResult grandPrixResult)
        {
            if (id == null)
            {
                return BadRequest("id is not recognised");
            }
            ValidateTeamPositionAndPointsScrored(grandPrixResult);
            if (ModelState.IsValid)
            {
                try
                {
                    var existing = await _context.GrandPrixResult.FirstOrDefaultAsync(g => g.Id == id);
                    if (existing == null)
                        return NotFound("GrandPrix Result not found.");

                    existing.Location = grandPrixResult.Location;
                    existing.RaceDay = grandPrixResult.RaceDay;
                    existing.PositionInTeamGrid = grandPrixResult.PositionInTeamGrid;
                    existing.PointsScored = grandPrixResult.PointsScored;

                    _context.Update(existing);
                    await _context.SaveChangesAsync();
                    grandPrixResult = existing;
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
                return Ok(grandPrixResult);
            }
            return BadRequest(ModelState);
        }


        // DELETE: api/GrandPrixResults/Delete/5
        [Authorize(Roles = "ADMIN")]
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var grandPrixResult = await _context.GrandPrixResult.FindAsync(id);
            
            if (grandPrixResult == null)
            {
                return NotFound("Grand Prix Result with id: " + id.ToString() + " not found!");
            }
            _context.GrandPrixResult.Remove(grandPrixResult);

            await _context.SaveChangesAsync();
            return Ok("success");
        }

        private bool GrandPrixResultExists(Guid id)
        {
            return _context.GrandPrixResult.Any(e => e.Id == id);
        }

        private void ValidateTeamPositionAndPointsScrored(GrandPrixResult res)
        {
            if (res.PositionInTeamGrid > MAX_TEAM_POS || res.PositionInTeamGrid <= 0)
            {
                ModelState.AddModelError("validation", "Invalid team position!");
            }
            if (res.PointsScored > MAX_POINTS || res.PointsScored <= 0)
            {
                ModelState.AddModelError("validation", "Invalid points added!");
            }
        }


    }
}
