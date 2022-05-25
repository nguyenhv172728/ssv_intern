namespace Intern.Share
{
    public class Team
    {
        public Team() { }

        public Team(int teamid, string teamname)
        {
            TeamID = teamid;    
            TeamName = teamname;
        }

        public int TeamID { get; set; } 
        public string? TeamName { get; set; } 
    }
}