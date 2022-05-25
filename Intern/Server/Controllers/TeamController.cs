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
    public class TeamController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public TeamController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [Route("get")]
        [HttpGet]
        public List<Team> Get()
        {
            string ConnectionString = _configuration.GetConnectionString("Default");
            List<Team> teams = new List<Team>();
            using (MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM team", conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                string? teamname;    
                
                while (reader.Read())
                {
                    int teamid = (int)reader["TeamID"];

                    if (reader["TeamName"] == DBNull.Value)
                    {
                        teamname = null;
                    }
                    else
                    {
                        teamname = (string)reader["TeamName"];
                    }
                    Team team = new Team(teamid,teamname);
                    teams.Add(team);
                }
                reader.Close();
                cmd.Dispose();
                conn.Close();
                return teams;
            }

        }
    }
}   
