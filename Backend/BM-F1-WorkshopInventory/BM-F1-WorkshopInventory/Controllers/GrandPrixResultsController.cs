using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BM_F1_WorkshopInventory.Data;
using BM_F1_WorkshopInventory.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using BM_F1_WorkshopInventory.Interfaces;
using System.Security.Claims;
using BM_F1_WorkshopInventory.Models.DTO;

namespace BM_F1_WorkshopInventory.Controllers
{
    [Authorize(Roles = "ADMIN,USER")]
    [Route("api/[controller]")]
    [ApiController]
    public class GrandPrixResultsController : ControllerBase
    {
        private readonly double MAX_POINTS = 43.0;
        private readonly int MAX_TEAM_POS = 10;
        private readonly IGrandPrixResultService service;
        private readonly IUserService userService;

        public GrandPrixResultsController(IGrandPrixResultService service, IUserService authService)
        {
            this.service = service;
            this.userService = authService;
        }

        // GET: api/GrandPrixResults
        [HttpGet]
        public async Task<ActionResult<List<GrandPrixResult>>> Index()
        {
            return Ok(await service.GetResults());
        }

        // GET: api/GrandPrixResults/Details/5
        [HttpGet("Details/{id}")]
        public async Task<ActionResult<GrandPrixResult>> Details(Guid id)
        {
            if (id.Equals(Guid.Empty))
            {
                return NotFound();
            }

            var grandPrixResult = await service.GetResult(id);
            if (grandPrixResult == null)
            {
                return NotFound();
            }

            return Ok(grandPrixResult);
        }

        // POST: GrandPrixResults/Create
        [HttpPost("Create")]
        public async Task<ActionResult<GrandPrixResult>> Create([Bind("RaceDay,Location,PointsScored,PositionInTeamGrid")] GrandPrixResult grandPrixResult)
        {
            ValidateTeamPositionAndPointsScrored(grandPrixResult);
            if (ModelState.IsValid)
            {
                Claim? idClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                if (idClaim == null)
                {
                    return Unauthorized("User ID claim is missing from token");
                }   
                UserInfoDTO? user = await userService.GetUser(idClaim.Value);
                if (user == null)
                {
                    return Unauthorized("User ID could not be verified");
                }
                var res = service.CreateResult(grandPrixResult);
                return Ok(grandPrixResult);
            }
            return BadRequest(ModelState);
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
                    Guid guid = id != null ? Guid.Parse(id.ToString()) : Guid.Empty;
                    await service.EditResult(guid,grandPrixResult); 
                    return Ok(grandPrixResult);
                }
                catch(Exception e)
                {
                    return NotFound(e.Message);
                }
            }
            return BadRequest(ModelState);
        }


        // DELETE: api/GrandPrixResults/Delete/5
        [Authorize(Roles = "ADMIN")]
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var deleted = await service.DeleteResult(id);
            if (deleted == false)
            {
                return NotFound("Grand Prix Result with id: " + id.ToString() + " not found!");
            }
            return Ok("success");
        }

      

        private void ValidateTeamPositionAndPointsScrored(GrandPrixResult res)
        {
            if (res.PositionInTeamGrid > MAX_TEAM_POS || res.PositionInTeamGrid <= 0)
            {
                ModelState.AddModelError("validation", "Invalid team position!");
            }
            if (res.PointsScored > MAX_POINTS || res.PointsScored < 0)
            {
                ModelState.AddModelError("validation", "Invalid points added!");
            }
        }


    }
}
