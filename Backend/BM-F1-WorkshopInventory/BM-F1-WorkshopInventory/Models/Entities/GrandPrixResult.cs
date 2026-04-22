namespace BM_F1_WorkshopInventory.Models.Entities
{
    public class GrandPrixResult
    {
        public Guid Id { get; set; }

        public DateOnly RaceDay {get; set ;}

        public string Location { get; set; } = "Silverstone";

        public double PointsScored { get; set; }
        
        public int PositionInTeamGrid { get; set; }

        public User CreatedBy { get; set; } = null!;
    }
}
