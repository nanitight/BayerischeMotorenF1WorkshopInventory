using BM_F1_WorkshopInventory.Models.Entities;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BM_F1_WorkshopInventory.Models.DTO
{
    public class GrandPrixResultDTO
    {

        public string Id { get; set; } = string.Empty;

        public DateOnly RaceDay { get; set; }

        public string Location { get; set; } = "Silverstone";

        public double PointsScored { get; set; }

        public int PositionInTeamGrid { get; set; }

        public UserInfoDTO CreatedBy { get; set; } = null!;

        public GrandPrixResultDTO(string id, DateOnly raceDay , string location , double pointsScored , int positionInTeamGrid , UserInfoDTO createdBy)
        {
            Id = id;
            RaceDay = raceDay;
            Location = location;
            PointsScored = pointsScored;
            PositionInTeamGrid = positionInTeamGrid;
            CreatedBy = createdBy;
        }
        public GrandPrixResultDTO(GrandPrixResult result)
        {
            Id = result.Id.ToString();
            RaceDay = result.RaceDay;
            Location = result.Location;
            PointsScored = result.PointsScored;
            PositionInTeamGrid = result.PositionInTeamGrid; ;
            CreatedBy = new UserInfoDTO(result.CreatedBy);
        }
    }
}
