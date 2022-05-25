using Intern.Share;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;


namespace Intern.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HandleAcceptedCVCandidateController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public HandleAcceptedCVCandidateController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [Route("checkhasbeenapply/{phone}")]
        [HttpGet]
        public HasBeenApply CheckHasbeenApply(string phone)
        {
            string ConnectionString = _configuration.GetConnectionString("Default");
            using (MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand($"SELECT * FROM intern where phone='{phone}'", conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                int i = 0;
                while (reader.Read())
                {
                    i++;
                }
                reader.Close();
                cmd.Dispose();
                conn.Close();
                HasBeenApply hasBeenApply = new HasBeenApply();
                if (i > 1)
                {
                    hasBeenApply.Applyed = true;
                }
                else
                {
                    hasBeenApply.Applyed = false;
                }

                return hasBeenApply;
            }
        }


        [Route("getwhyignore/{phone}")]
        [HttpGet]
        public HasBeenApply GetWhyIgnore(string phone)
        {
            string ConnectionString = _configuration.GetConnectionString("Default");
            using (MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                MySqlCommand cmd = new MySqlCommand($"SELECT WhyIgnore FROM intern where phone='{phone}' and status <>{0}", conn);
                conn.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                HasBeenApply hasBeenApply = new HasBeenApply();
                string? whyignore="";
                while (reader.Read())
                {
                    if (reader["WhyIgnore"] == DBNull.Value)
                    {
                        whyignore = string.Empty;
                    }
                    else
                    {
                        whyignore = (string)reader["WhyIgnore"];
                    }
                }
                reader.Close();
                cmd.Dispose();
                conn.Close();
                hasBeenApply.whyignore = whyignore;
                return hasBeenApply;

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
                MySqlCommand cmd = new MySqlCommand($"select * from intern inner Join position ON position.PositionID = intern.PositionID inner Join team ON team.TeamID = intern.TeamID where name like '%{searchstring}%' and status<>0 and status<>1 ", conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                string? cancontact;
                DateTime? timeinterview;
                string? locationinterview;
                string? note;
                string? testpoint;

                while (reader.Read())
                {
                   int  id = (int)reader["ID"];
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
                    string? positionname = (string)reader["PositionName"];
                    string? teamname = (string)reader["TeamName"];
                    string? name = (string)reader["Name"];
                    string? birthday = (string)reader["birthday"];
                    string? address = (string)reader["address"];
                    string? phone = (string)reader["Phone"];
                    string? email = (string)reader["Email"];
                    string? Source = (string)reader["Source"];
                    Candidate candidate = new Candidate(id, positionname, teamname, name, birthday, address, phone, email, Source);
                    candidate.Status = status;
                    candidate.CanContact = cancontact;
                    candidate.TimeInterview = timeinterview;
                    candidate.LocationInterview = locationinterview;
                    candidate.Note = note;
                    candidate.TestPoint = testpoint;
                    candidates.Add(candidate);
                }
                reader.Close();
                cmd.Dispose();
                conn.Close();
                return candidates;
            }
        }


        [Route("ignorecv/{id}")]
        [HttpPut]
        public bool IgnoreCV(int id)
        {
            string ConnectionString = _configuration.GetConnectionString("Default");
            using (MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                string sqlText1 = $"UPDATE intern SET Status={1} WHERE ID={id}";
                string sqlText2 = $"UPDATE intern SET WhyIgnore='CV không đạt yêu cầu' WHERE ID={id}";
                MySqlCommand cmd1 = new MySqlCommand(sqlText1, conn);
                MySqlCommand cmd2 = new MySqlCommand(sqlText2, conn);
                conn.Open();
                int i1 = cmd1.ExecuteNonQuery();
                int i2 = cmd2.ExecuteNonQuery();
                conn.Close();
                return i1 >= 1 ? true : false;

            }
        }


        [Route("acceptcv/{id}")]
        [HttpPut]
        public bool AcceptCV(int id)
        {
            string ConnectionString = _configuration.GetConnectionString("Default");
            using (MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                string sqlText1 = $"UPDATE intern SET Status={2} WHERE ID={id}";
                MySqlCommand cmd1 = new MySqlCommand(sqlText1, conn);
                conn.Open();
                int i1 = cmd1.ExecuteNonQuery();
                conn.Close();
                return i1 >= 1 ? true : false;

            }
        }

    }
}
