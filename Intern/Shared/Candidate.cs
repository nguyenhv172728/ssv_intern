using System.ComponentModel.DataAnnotations;

namespace Intern.Share
{
    public class Candidate
    {
        public Candidate() { Count = 0; }

        public Candidate(int id, string? positionname, string? teamname, string? name, string? birthday, string? address, string? phone, string? email, string? source)
        {
            ID = id;
            PositionName = positionname;
            TeamName = teamname;
            Name = name;
            Birthday = birthday;
            Address = address;
            Phone = phone;
            Email = email;
            Source = source;
        }

        public Candidate(int id, int postitionid, string? positionname, int teamid, string? teamname, string name, string birthday, string address, string phone, string email, string source, byte[]? cv)
        {
            ID = id;
            PositionID = postitionid;
            PositionName = positionname;
            Name = name;
            Birthday = birthday;
            Address = address;
            Phone = phone;
            Email = email;
            Source = source;
            Cv = cv;
            TeamID = teamid;
            TeamName = teamname;
        }

        public int ID { get; set; }
        public int Count { get; set; }
        public int? PositionID { get; set; }
        public string? PositionName { get; set; }
        public int? TeamID { get; set; }
        public string? TeamName { get; set; }
        public string? Birthday { get; set; }
        public string? Name { get; set; }
        public string? Location { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Source { get; set; }
        public string? HasBeenApply { get; set; }
        public string? CanContact { get; set; }
        public DateTime? TimeInterview { get; set; }
        public string? LocationInterview { get; set; }
        public string? RoomInterview { get; set; }
        public string? Note { get; set; }
        public int? Status { get; set; }
        public string? TestPoint { get; set; }
        public byte[]? Cv { get; set; }
        public string? Email { get; set; }
        public string? WhyIgnore { get; set; }
        public string? SendEmail { get; set; }
        public string? EmailTitle { get; set; }
        public string? EmailContent { get; set; }
        public string? InterviewPeople { get; set; }
        public bool CheckBox { get; set; }
    }
}