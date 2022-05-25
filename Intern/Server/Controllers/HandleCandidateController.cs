using Intern.Share;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;


namespace Intern.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HandleCandidateController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public HandleCandidateController(IConfiguration configuration)
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
                MySqlCommand cmd = new MySqlCommand($"select * from intern inner Join position ON position.PositionID = intern.PositionID inner Join team ON team.TeamID = intern.TeamID where name like '%{searchstring}%' and status ={0} ", conn);
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
                byte[]? Cv;

                while (reader.Read())
                {
                    id = (int)reader["ID"];
                    positionname = (string)reader["PositionName"];
                    teamname = (string)reader["TeamName"];
                    name = (string)reader["Name"];
                    birthday = (string)reader["birthday"];
                    address = (string)reader["address"];
                    phone = (string)reader["Phone"];
                    email = (string)reader["Email"];
                    Source = (string)reader["Source"];
                    Candidate candidate = new Candidate(id, positionname, teamname, name, birthday, address, phone, email, Source);
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
