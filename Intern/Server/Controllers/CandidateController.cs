using Intern.Share;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using static System.Net.Mime.MediaTypeNames;
using System.Drawing;
using System.Drawing.Imaging;
using System.Data;
using System.IO;

namespace Intern.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidateController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public CandidateController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [Route("get")]
        [HttpGet]

        public List<Candidate> Get()
        {
            string ConnectionString = _configuration.GetConnectionString("Default");
            List<Candidate> candidates = new List<Candidate>();
            using (MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM intern inner Join position ON position.PositionID = intern.PositionID inner Join team ON team.TeamID = intern.TeamID", conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                int id;
                string? positionname;
                string? teamname;
                string? name;
                string? birthday;
                string? address;
                string? phone;
                string? email;
                string? Source;
                string? whyignore;
                while (reader.Read())
                {
                    id = (int)reader["ID"];
                    if (reader["PositionName"] == DBNull.Value)
                    {
                        positionname = null;
                    }
                    else
                    {
                        positionname = (string)reader["PositionName"];
                    }

                    if (reader["TeamName"] == DBNull.Value)
                    {
                        teamname = null;
                    }
                    else
                    {
                        teamname = (string)reader["TeamName"];
                    }

                    if (reader["Name"] == DBNull.Value)
                    {
                        name = string.Empty;
                    }
                    else
                    {
                        name = (string)reader["Name"];
                    }

                    if (reader["Birthday"] == DBNull.Value)
                    {
                        birthday = string.Empty;
                    }
                    else
                    {
                        birthday = (string)reader["birthday"];
                    }

                    if (reader["Address"] == DBNull.Value)
                    {
                        address = string.Empty;
                    }
                    else
                    {
                        address = (string)reader["address"];
                    }

                    if (reader["Phone"] == DBNull.Value)
                    {
                        phone = string.Empty;
                    }
                    else
                    {
                        phone = (string)reader["Phone"];
                    }

                    if (reader["Email"] == DBNull.Value)
                    {
                        email = string.Empty;
                    }
                    else
                    {
                        email = (string)reader["Email"];
                    }

                    if (reader["Source"] == DBNull.Value)
                    {
                        Source = string.Empty;
                    }
                    else
                    {
                        Source = (string)reader["Source"];
                    }

                    if (reader["WhyIgnore"] == DBNull.Value)
                    {
                        whyignore = string.Empty;
                    }
                    else
                    {
                        whyignore = (string)reader["WhyIgnore"];
                    }
                    Candidate candidate = new Candidate(id, positionname, teamname, name, birthday, address, phone, email, Source);
                    candidate.WhyIgnore= whyignore; 
                    candidates.Add(candidate);
                }
                reader.Close();
                cmd.Dispose();
                conn.Close();
                return candidates;
            }
        }

        [Route("getcandidateneedtohandle")]
        [HttpGet]

        public List<Candidate> GetCandidateNeedtoHandle()
        {
            string ConnectionString = _configuration.GetConnectionString("Default");
            List<Candidate> candidates = new List<Candidate>();
            using (MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM intern inner Join position ON position.PositionID = intern.PositionID inner Join team ON team.TeamID = intern.TeamID where status=0", conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int id = (int)reader["ID"];
                    string? positionname = (string)reader["PositionName"];
                    string? teamname = (string)reader["TeamName"];
                    string? name = (string)reader["Name"];
                    string? birthday = (string)reader["birthday"];
                    string? address = (string)reader["address"];
                    string? phone = (string)reader["Phone"];
                    string? email = (string)reader["Email"];
                    string? Source = (string)reader["Source"];
                    Candidate candidate = new Candidate(id, positionname, teamname, name, birthday, address, phone, email, Source);
                    candidates.Add(candidate);
                }
                reader.Close();
                cmd.Dispose();
                conn.Close();
                return candidates;
            }
        }

        [Route("getcandidatepasscv")]
        [HttpGet]

        public List<Candidate> GetCandidatePassCV()
        {
            string ConnectionString = _configuration.GetConnectionString("Default");
            List<Candidate> candidates = new List<Candidate>();
            using (MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM intern inner Join position ON position.PositionID = intern.PositionID inner Join team ON team.TeamID = intern.TeamID where status<>0 and status<>1", conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                string? cancontact;
                DateTime? timeinterview;
                string? locationinterview;
                string? note;
                string? testpoint;

                while (reader.Read())
                {
                    int id = (int)reader["ID"];
                    string? positionname = (string)reader["PositionName"];
                    string? teamname = (string)reader["TeamName"];
                    string? name = (string)reader["Name"];
                    string? birthday = (string)reader["birthday"];
                    string? address = (string)reader["address"];
                    string? phone = (string)reader["Phone"];
                    string? email = (string)reader["Email"];
                    string? Source = (string)reader["Source"];
                    string? sendmail = (string)reader["SendMail"];
                    int status = (int)reader["Status"];
                    if (reader["CanContact"] == DBNull.Value)
                    {
                        cancontact = string.Empty;
                    }
                    else
                    {
                        cancontact = (string)reader["Cancontact"];
                    }


                    if (reader["TimeInterView"] == DBNull.Value)
                    {
                        timeinterview = null;
                    }
                    else
                    {
                        timeinterview = (DateTime)reader["TimeInterView"];
                    }


                    if (reader["LocationInterView"] == DBNull.Value)
                    {
                        locationinterview = string.Empty;
                    }
                    else
                    {
                        locationinterview = (string)reader["LocationInterView"];
                    }


                    if (reader["Note"] == DBNull.Value)
                    {
                        note = string.Empty;
                    }
                    else
                    {
                        note = (string)reader["Note"];
                    }


                    if (reader["TestPoint"] == DBNull.Value)
                    {
                        testpoint = string.Empty;
                    }
                    else
                    {
                        testpoint = (string)reader["TestPoint"];
                    }
                    Candidate candidate = new Candidate(id, positionname, teamname, name, birthday, address, phone, email, Source);
                    candidate.Status = status;
                    candidate.CanContact = cancontact;
                    candidate.TimeInterview = timeinterview;
                    candidate.LocationInterview = locationinterview;
                    candidate.Note = note;
                    candidate.TestPoint = testpoint;
                    candidate.SendEmail = sendmail;
                    candidates.Add(candidate);
                }
                reader.Close();
                cmd.Dispose();
                conn.Close();
                return candidates;
            }
        }


        [Route("getcandidatecancontact")]
        [HttpGet]

        public List<Candidate> GetCandidateCanContact()
        {
            string ConnectionString = _configuration.GetConnectionString("Default");
            List<Candidate> candidates = new List<Candidate>();
            using (MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM intern inner Join position ON position.PositionID = intern.PositionID inner Join team ON team.TeamID = intern.TeamID where CanContact='Đã liên hệ'", conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                string? cancontact;
                DateTime? timeinterview;
                string? locationinterview;
                string? note;
                string? testpoint;
                while (reader.Read())
                {
                    int id = (int)reader["ID"];
                    string? positionname = (string)reader["PositionName"];
                    string? teamname = (string)reader["TeamName"];
                    string? name = (string)reader["Name"];
                    string? birthday = (string)reader["birthday"];
                    string? address = (string)reader["address"];
                    string? phone = (string)reader["Phone"];
                    string? email = (string)reader["Email"];
                    string? Source = (string)reader["Source"];
                    string? SendEmail = (string)reader["SendMail"];

                    int status = (int)reader["Status"];
                    if (reader["CanContact"] == DBNull.Value)
                    {
                        cancontact = string.Empty;
                    }
                    else
                    {
                        cancontact = (string)reader["Cancontact"];
                    }


                    if (reader["TimeInterView"] == DBNull.Value)
                    {
                        timeinterview = null;
                    }
                    else
                    {
                        timeinterview = (DateTime)reader["TimeInterView"];
                    }


                    if (reader["LocationInterView"] == DBNull.Value)
                    {
                        locationinterview = string.Empty;
                    }
                    else
                    {
                        locationinterview = (string)reader["LocationInterView"];
                    }


                    if (reader["Note"] == DBNull.Value)
                    {
                        note = string.Empty;
                    }
                    else
                    {
                        note = (string)reader["Note"];
                    }


                    if (reader["TestPoint"] == DBNull.Value)
                    {
                        testpoint = string.Empty;
                    }
                    else
                    {
                        testpoint = (string)reader["TestPoint"];
                    }
                    Candidate candidate = new Candidate(id, positionname, teamname, name, birthday, address, phone, email, Source);
                    candidate.Status = status;
                    candidate.CanContact = cancontact;
                    candidate.TimeInterview = timeinterview;
                    candidate.LocationInterview = locationinterview;
                    candidate.Note = note;
                    candidate.TestPoint = testpoint;
                    candidate.SendEmail = SendEmail;
                    candidates.Add(candidate);
                }
                reader.Close();
                cmd.Dispose();
                conn.Close();
                return candidates;
            }
        }


        [Route("getcandidatesendedmail")]
        [HttpGet]

        public List<Candidate> GetCandidateSendedMail()
        {
            string ConnectionString = _configuration.GetConnectionString("Default");
            List<Candidate> candidates = new List<Candidate>();
            using (MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand($"SELECT * FROM intern inner Join position ON position.PositionID = intern.PositionID inner Join team ON team.TeamID = intern.TeamID where (Status={5} or Status={7}) and Planned={0}", conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                string? cancontact;
                DateTime? timeinterview;
                string? locationinterview;
                string? note;
                string? testpoint;
                while (reader.Read())
                {
                    int id = (int)reader["ID"];
                    string? positionname = (string)reader["PositionName"];
                    string? teamname = (string)reader["TeamName"];
                    string? name = (string)reader["Name"];
                    string? birthday = (string)reader["birthday"];
                    string? address = (string)reader["address"];
                    string? phone = (string)reader["Phone"];
                    string? email = (string)reader["Email"];
                    string? Source = (string)reader["Source"];
                    string? SendEmail = (string)reader["SendMail"];

                    int status = (int)reader["Status"];
                    if (reader["CanContact"] == DBNull.Value)
                    {
                        cancontact = string.Empty;
                    }
                    else
                    {
                        cancontact = (string)reader["Cancontact"];
                    }


                    if (reader["TimeInterView"] == DBNull.Value)
                    {
                        timeinterview = null;
                    }
                    else
                    {
                        timeinterview = (DateTime)reader["TimeInterView"];
                    }


                    if (reader["LocationInterView"] == DBNull.Value)
                    {
                        locationinterview = string.Empty;
                    }
                    else
                    {
                        locationinterview = (string)reader["LocationInterView"];
                    }


                    if (reader["Note"] == DBNull.Value)
                    {
                        note = string.Empty;
                    }
                    else
                    {
                        note = (string)reader["Note"];
                    }


                    if (reader["TestPoint"] == DBNull.Value)
                    {
                        testpoint = string.Empty;
                    }
                    else
                    {
                        testpoint = (string)reader["TestPoint"];
                    }
                    Candidate candidate = new Candidate(id, positionname, teamname, name, birthday, address, phone, email, Source);
                    candidate.Status = status;
                    candidate.CanContact = cancontact;
                    candidate.TimeInterview = timeinterview;
                    candidate.LocationInterview = locationinterview;
                    candidate.Note = note;
                    candidate.TestPoint = testpoint;
                    candidate.SendEmail = SendEmail;
                    candidates.Add(candidate);
                }
                reader.Close();
                cmd.Dispose();
                conn.Close();
                return candidates;
            }
        }

        [Route("getcandidate_calendered")]
        [HttpGet]

        public List<Candidate> GetCandidateCalendered()
        {
            string ConnectionString = _configuration.GetConnectionString("Default");
            List<Candidate> candidates = new List<Candidate>();
            using (MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand($"SELECT * FROM intern inner Join position ON position.PositionID = intern.PositionID inner Join team ON team.TeamID = intern.TeamID where  Planned={1}", conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                string? cancontact;
                DateTime? timeinterview;
                string? locationinterview;
                string? roominterview;
                string? note;
                string? testpoint;
                string? interviewpeople;
                while (reader.Read())
                {
                    int id = (int)reader["ID"];
                    string? positionname = (string)reader["PositionName"];
                    string? teamname = (string)reader["TeamName"];
                    string? name = (string)reader["Name"];
                    string? birthday = (string)reader["birthday"];
                    string? address = (string)reader["address"];
                    string? phone = (string)reader["Phone"];
                    string? email = (string)reader["Email"];
                    string? Source = (string)reader["Source"];
                    string? SendEmail = (string)reader["SendMail"];

                    int status = (int)reader["Status"];
                    if (reader["CanContact"] == DBNull.Value)
                    {
                        cancontact = string.Empty;
                    }
                    else
                    {
                        cancontact = (string)reader["Cancontact"];
                    }


                    if (reader["TimeInterView"] == DBNull.Value)
                    {
                        timeinterview = null;
                    }
                    else
                    {
                        timeinterview = (DateTime)reader["TimeInterView"];
                    }


                    if (reader["LocationInterView"] == DBNull.Value)
                    {
                        locationinterview = string.Empty;
                    }
                    else
                    {
                        locationinterview = (string)reader["LocationInterView"];
                    }


                    if (reader["Note"] == DBNull.Value)
                    {
                        note = string.Empty;
                    }
                    else
                    {
                        note = (string)reader["Note"];
                    }

                    if (reader["TestPoint"] == DBNull.Value)
                    {
                        testpoint = string.Empty;
                    }
                    else
                    {
                        testpoint = (string)reader["TestPoint"];
                    }

                    if (reader["RoomInterview"] == DBNull.Value)
                    {
                        roominterview = string.Empty;
                    }
                    else
                    {
                        roominterview = (string)reader["RoomInterview"];
                    }

                    if (reader["InterviewPeople"] == DBNull.Value)
                    {
                        interviewpeople = string.Empty;
                    }
                    else
                    {
                        interviewpeople = (string)reader["InterviewPeople"];
                    }

                    Candidate candidate = new Candidate(id, positionname, teamname, name, birthday, address, phone, email, Source);
                    candidate.Status = status;
                    candidate.CanContact = cancontact;
                    candidate.TimeInterview = timeinterview;
                    candidate.LocationInterview = locationinterview;
                    candidate.Note = note;
                    candidate.TestPoint = testpoint;
                    candidate.SendEmail = SendEmail;
                    candidate.RoomInterview = roominterview;     
                    candidate.InterviewPeople = interviewpeople;
                    candidates.Add(candidate);
                }
                reader.Close();
                cmd.Dispose();
                conn.Close();
                return candidates;
            }
        }


        [Route("getcandidatepasscv/{ID}")]
        [HttpGet]

        public Candidate GetCandidatePassCVWithID(int ID)
        {
            string ConnectionString = _configuration.GetConnectionString("Default");
           Candidate candidate = new Candidate();
            using (MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand($"SELECT * FROM intern inner Join position ON position.PositionID = intern.PositionID inner Join team ON team.TeamID = intern.TeamID where ID={ID}", conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                string? cancontact;
                DateTime? timeinterview;
                string? locationinterview;
                string? note;
                string? testpoint;
                byte[] cv;

                while (reader.Read())
                {
                    int id = (int)reader["ID"];
                    string? positionname = (string)reader["PositionName"];
                    string? teamname = (string)reader["TeamName"];
                    string? name = (string)reader["Name"];
                    string? birthday = (string)reader["birthday"];
                    string? address = (string)reader["address"];
                    string? phone = (string)reader["Phone"];
                    string? email = (string)reader["Email"];
                    string? Source = (string)reader["Source"];
                    int status = (int)reader["Status"];
                    if (reader["CanContact"] == DBNull.Value)
                    {
                        cancontact = string.Empty;
                    }
                    else
                    {
                        cancontact = (string)reader["Cancontact"];
                    }


                    if (reader["TimeInterView"] == DBNull.Value)
                    {
                        timeinterview = null;
                    }
                    else
                    {
                        timeinterview = (DateTime)reader["TimeInterView"];
                    }


                    if (reader["LocationInterView"] == DBNull.Value)
                    {
                        locationinterview = string.Empty;
                    }
                    else
                    {
                        locationinterview = (string)reader["LocationInterView"];
                    }


                    if (reader["Note"] == DBNull.Value)
                    {
                        note = string.Empty;
                    }
                    else
                    {
                        note = (string)reader["Note"];
                    }


                    if (reader["TestPoint"] == DBNull.Value)
                    {
                        testpoint = string.Empty;
                    }
                    else
                    {
                        testpoint = (string)reader["TestPoint"];
                    }
                    candidate = new Candidate(id, positionname, teamname, name, birthday, address, phone, email, Source);
                    candidate.Status = status;
                    candidate.CanContact = cancontact;
                    candidate.TimeInterview = timeinterview;
                    candidate.LocationInterview = locationinterview;
                    candidate.Note = note;
                    candidate.TestPoint = testpoint;
                    
                }
                reader.Close();
                cmd.Dispose();
                conn.Close();
                return candidate;
            }
        }



        [Route("search/{searchstring}")]
        [HttpGet]
        public List<Candidate> Search(string searchstring)
        {
            string ConnectionString = _configuration.GetConnectionString("Default");
            List<Candidate> candidates = new List<Candidate>();
            using (MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand($"select * from intern inner Join position ON position.PositionID = intern.PositionID inner Join team ON team.TeamID = intern.TeamID where name like '%{searchstring}%'", conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int id = (int)reader["ID"];
                    string? positionname = (string)reader["PositionName"];
                    string? teamname = (string)reader["TeamName"];
                    string? name = (string)reader["Name"];
                    string? birthday = (string)reader["birthday"];
                    string? address = (string)reader["address"];
                    string? phone = (string)reader["Phone"];
                    string? email = (string)reader["Email"];
                    string? Source = (string)reader["Source"];

                    Candidate candidate = new Candidate(id, positionname, teamname, name, birthday, address, phone, email, Source);
                    candidates.Add(candidate);
                }
                reader.Close();
                cmd.Dispose();
                conn.Close();
                return candidates;
            }
        }


        [Route("get/{Id}")]
        [HttpGet]
        public Candidate Get(int Id)
        {
            string ConnectionString = _configuration.GetConnectionString("Default");
            using (MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM intern inner Join position ON position.PositionID = intern.PositionID inner Join team ON team.TeamID = intern.TeamID WHERE ID=@ID", conn);
                cmd.Parameters.AddWithValue("@ID", Id);
                conn.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                Candidate candidate = new Candidate();
                while (reader.Read())
                {
                    int id;
                    int positionid;
                    string? positionname;
                    int teamid;
                    string? teamname;
                    string? name;
                    string? birthday;
                    string? address;
                    string? phone;
                    string? email;
                    string? Source;
                    byte[]? Cv;
                    id = (int)reader["ID"];
                    positionid = (int)reader["PositionID"];
                    teamid = (int)reader["TeamID"];

                    if (reader["PositionName"] == DBNull.Value)
                    {
                        positionname = null;
                    }
                    else
                    {
                        positionname = (string)reader["PositionName"];
                    }

                    if (reader["TeamName"] == DBNull.Value)
                    {
                        teamname = null;
                    }
                    else
                    {
                        teamname = (string)reader["TeamName"];
                    }

                    if (reader["Name"] == DBNull.Value)
                    {
                        name = string.Empty;
                    }
                    else
                    {
                        name = (string)reader["Name"];
                    }

                    if (reader["Birthday"] == DBNull.Value)
                    {
                        birthday = string.Empty;
                    }
                    else
                    {
                        birthday = (string)reader["birthday"];
                    }

                    if (reader["Address"] == DBNull.Value)
                    {
                        address = string.Empty;
                    }
                    else
                    {
                        address = (string)reader["address"];
                    }

                    if (reader["Phone"] == DBNull.Value)
                    {
                        phone = string.Empty;
                    }
                    else
                    {
                        phone = (string)reader["Phone"];
                    }

                    if (reader["Email"] == DBNull.Value)
                    {
                        email = string.Empty;
                    }
                    else
                    {
                        email = (string)reader["Email"];
                    }

                    if (reader["Source"] == DBNull.Value)
                    {
                        Source = string.Empty;
                    }
                    else
                    {
                        Source = (string)reader["Source"];
                    }

                    Cv = (byte[])reader["CV"];
                    candidate = new Candidate(id, positionid, positionname, teamid, teamname, name, birthday, address, phone, email, Source, Cv);
                }
                reader.Close();
                cmd.Dispose();
                conn.Close();
                return candidate;
            }
        }

        [Route("getCV/{Id}")]
        [HttpGet]
        public byte[] GetCV(int Id)
        {
            string ConnectionString = _configuration.GetConnectionString("Default");
            using (MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                MySqlCommand cmd = new MySqlCommand($"select CV from intern where ID={Id}", conn);
                conn.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                Candidate candidate = new Candidate();
                byte[] Cv = null;
                while (reader.Read())
                {
                    Cv = (byte[])reader["CV"];
                }
                reader.Close();
                cmd.Dispose();
                conn.Close();
                return Cv;
            }
        }

        [Route("post")]
        [HttpPost]
        public bool PostProduct([FromBody] Candidate candidate)
        {
            string ConnectionString = _configuration.GetConnectionString("Default");
            using (MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                string sqlText = $"INSERT INTO intern(PositionID,TeamID,CV,Source,Name,Phone,Birthday,Email,Address,Status,CanContact,SendMail,Planned) VALUES (@PositionID,@TeamID,@CV,@Source,@Name,@Phone,@Birthday,@Email,@Address,{0},'Chưa','Chưa',{0})";
                MySqlCommand cmd = new MySqlCommand(sqlText, conn);
                cmd.Parameters.AddWithValue("@PositionID", candidate.PositionID);
                cmd.Parameters.AddWithValue("@TeamID", candidate.TeamID);
                cmd.Parameters.AddWithValue("@CV", candidate.Cv);
                cmd.Parameters.AddWithValue("@Source", candidate.Source);
                cmd.Parameters.AddWithValue("@Name", candidate.Name);
                cmd.Parameters.AddWithValue("@Phone", candidate.Phone);
                cmd.Parameters.AddWithValue("@Birthday", candidate.Birthday);
                cmd.Parameters.AddWithValue("@Email", candidate.Email);
                cmd.Parameters.AddWithValue("@Address", candidate.Address);
                conn.Open();
                int i = cmd.ExecuteNonQuery();
                conn.Close();
                return i >= 1;

            }
        }

        [Route("delete/{Id}")]
        [HttpDelete]
        public bool Delete(int id)
        {
            string ConnectionString = _configuration.GetConnectionString("Default");
            using (MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                string sqlText = "DELETE FROM intern WHERE ID = @ID;";
                MySqlCommand cmd = new MySqlCommand(sqlText, conn);
                cmd.Parameters.AddWithValue("@ID", id);
                conn.Open();
                int i = cmd.ExecuteNonQuery();
                conn.Close();
                return i >= 1 ? true : false;
            }
        }


        [Route("put")]
        [HttpPut]
        public bool PutCandidate([FromBody] Candidate candidate)
        {
            string ConnectionString = _configuration.GetConnectionString("Default");
            MySqlConnection conn = new MySqlConnection(ConnectionString);
            string sqlText = "UPDATE intern SET PositionID=@Position,TeamID=@Team,CV=@CV,Source=@Source,Name=@Name,Phone=@Phone,Birthday=@Birthday,Email=@Email,Address=@Address WHERE ID=@ID";
            MySqlCommand cmd = new MySqlCommand(sqlText, conn);
            cmd.Parameters.AddWithValue("@ID", candidate.ID);
            cmd.Parameters.AddWithValue("@Position", candidate.PositionID);
            cmd.Parameters.AddWithValue("@Team", candidate.TeamID);
            cmd.Parameters.AddWithValue("@CV", candidate.Cv);
            cmd.Parameters.AddWithValue("@Source", candidate.Source);
            cmd.Parameters.AddWithValue("@Name", candidate.Name);
            cmd.Parameters.AddWithValue("@Phone", candidate.Phone);
            cmd.Parameters.AddWithValue("@Birthday", candidate.Birthday);
            cmd.Parameters.AddWithValue("@Email", candidate.Email);
            cmd.Parameters.AddWithValue("@Address", candidate.Address);
            conn.Open();
            int i = cmd.ExecuteNonQuery();
            conn.Close();
            return i >= 1 ? true : false;

        }

        [Route("updatestatus")]
        [HttpPut]
        public bool UpdateCandidate([FromBody] Candidate candidate)
        {
            string ConnectionString = _configuration.GetConnectionString("Default");
            MySqlConnection conn = new MySqlConnection(ConnectionString);
            string sqlText = "UPDATE intern SET CanContact=@CanContact,Status=@Status,TimeInterView=@TimeInterView,LocationInterView=@LocationInterView,Note=@Note,TestPoint=@TestPoint,WhyIgnore=@WhyIgnore WHERE ID=@ID";
            MySqlCommand cmd = new MySqlCommand(sqlText, conn);
            cmd.Parameters.AddWithValue("@ID", candidate.ID);
            cmd.Parameters.AddWithValue("@CanContact", candidate.CanContact);
            cmd.Parameters.AddWithValue("@Status", candidate.Status);
            cmd.Parameters.AddWithValue("@TimeInterView", candidate.TimeInterview);
            cmd.Parameters.AddWithValue("@LocationInterView", candidate.LocationInterview);
            cmd.Parameters.AddWithValue("@Note", candidate.Note);
            cmd.Parameters.AddWithValue("@TestPoint", candidate.TestPoint);
            cmd.Parameters.AddWithValue("@WhyIgnore", candidate.WhyIgnore);
            conn.Open();
            int i = cmd.ExecuteNonQuery();
            conn.Close();
            return i >= 1 ? true : false;

        }

        [Route("update_calender")]
        [HttpPut]
        public bool UpdateCalender([FromBody] Candidate candidate)
        {
            string ConnectionString = _configuration.GetConnectionString("Default");
            MySqlConnection conn = new MySqlConnection(ConnectionString);
            string sqlText = "UPDATE intern SET TimeInterView=@TimeInterView,LocationInterView=@LocationInterView,RoomInterview=@RoomInterview WHERE ID=@ID";
            MySqlCommand cmd = new MySqlCommand(sqlText, conn);
            cmd.Parameters.AddWithValue("@ID", candidate.ID);
            cmd.Parameters.AddWithValue("@TimeInterView", candidate.TimeInterview);
            cmd.Parameters.AddWithValue("@LocationInterView", candidate.LocationInterview);
            cmd.Parameters.AddWithValue("@RoomInterView", candidate.RoomInterview);
            conn.Open();
            int i = cmd.ExecuteNonQuery();
            conn.Close();
            return i >= 1 ? true : false;

        }

        [Route("delete_from_calender")]
        [HttpPut]
        public bool DeleteFromCalender([FromBody] Candidate candidate)
        {
            string ConnectionString = _configuration.GetConnectionString("Default");
            using (MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                string sqlText = $"UPDATE intern SET Planned={0}  WHERE ID={candidate.ID}";
                MySqlCommand cmd = new MySqlCommand(sqlText, conn);
                conn.Open();
                int i = cmd.ExecuteNonQuery();
                conn.Close();
                return i >= 1 ? true : false;

            }
        }
    }
}
