namespace Intern.Share
{
    public class Position
    {
        public Position() { }

        public Position(int positionid, string ?positionname)
        {
            PositionID = positionid;    
            PositionName = positionname;
        }

        public int PositionID { get; set; } 
        public string? PositionName { get; set; } 
    }
}