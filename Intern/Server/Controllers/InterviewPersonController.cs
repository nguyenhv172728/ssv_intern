using Intern.Share;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;
using System.Configuration;


namespace Intern.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InterviewPersonController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public InterviewPersonController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [Route("get")]
        [HttpGet]
        public List<InterviewPerson> Get()
        {
            string ConnectionString = _configuration.GetConnectionString("Default");
            List<InterviewPerson> interviewpersons = new List<InterviewPerson>();
            using (MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM interviewperson", conn);
                MySqlDataReader reader = cmd.ExecuteReader();              
                while (reader.Read())
                {
                    int id = (int)reader["ID"];
                    string name = (string)reader["Name"];
                    string mail = (string)reader["Mail"];
                    string team = (string)reader["Team"];
                    InterviewPerson interviewPerson = new InterviewPerson();
                    interviewPerson.Name = name;
                    interviewPerson.Mail = mail;
                    interviewPerson.Team = team;
                    interviewPerson.ID = id;
                    interviewpersons.Add(interviewPerson);
                }
                reader.Close();
                cmd.Dispose();
                conn.Close();
                return interviewpersons;
            }

        }

        [Route("post")]
        [HttpPost]
        public bool PostProduct([FromBody] InterviewPerson interviewpersons)
        {
            string ConnectionString = _configuration.GetConnectionString("Default");
            using (MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                string sqlText = $"INSERT INTO interviewperson(Name,Team,Mail) VALUES (@Name,@Team,@Mail)";
                MySqlCommand cmd = new MySqlCommand(sqlText, conn);
                cmd.Parameters.AddWithValue("@Name",interviewpersons.Name);
                cmd.Parameters.AddWithValue("@Team", interviewpersons.Team);
                cmd.Parameters.AddWithValue("@Mail",interviewpersons.Mail );
                conn.Open();
                int i = cmd.ExecuteNonQuery();
                conn.Close();
                return i >= 1 ? true : false;

            }
        }


        [Route("put")]
        [HttpPut]
        public bool PutCandidate([FromBody] InterviewPerson interviewperson)
        {
            string ConnectionString = _configuration.GetConnectionString("Default");
            MySqlConnection conn = new MySqlConnection(ConnectionString);
            string sqlText = $"UPDATE interviewperson SET Name='{interviewperson.Name}',Team='{interviewperson.Team}',Mail='{interviewperson.Mail}' WHERE ID={interviewperson.ID}";
            MySqlCommand cmd = new MySqlCommand(sqlText, conn);
            conn.Open();
            int i = cmd.ExecuteNonQuery();
            conn.Close();
            return i >= 1 ? true : false;

        }

        [Route("delete/{Id}")]
        [HttpDelete]
        public bool Delete(int id)
        {
            string ConnectionString = _configuration.GetConnectionString("Default");
            using (MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                string sqlText = "DELETE FROM interviewperson WHERE ID = @ID;";
                MySqlCommand cmd = new MySqlCommand(sqlText, conn);
                cmd.Parameters.AddWithValue("@ID", id);
                conn.Open();
                int i = cmd.ExecuteNonQuery();
                conn.Close();
                return i >= 1 ? true : false;
            }
        }
    }
}   
