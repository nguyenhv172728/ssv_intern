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
    public class PositionController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public PositionController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [Route("get")]
        [HttpGet]
        public List<Position> Get()
        {
            string ConnectionString = _configuration.GetConnectionString("Default");
            List<Position> positions = new List<Position>();
            using (MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM position", conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                string? nameposition;    
                
                while (reader.Read())
                {
                    int positionid = (int)reader["PositionID"];

                    if (reader["PositionName"] == DBNull.Value)
                    {
                        nameposition = null;
                    }
                    else
                    {
                        nameposition = (string)reader["PositionName"];
                    }
                    Position position = new Position(positionid,nameposition);
                    positions.Add(position);
                }
                reader.Close();
                cmd.Dispose();
                conn.Close();
                return positions;
            }

        }
    }
}   
