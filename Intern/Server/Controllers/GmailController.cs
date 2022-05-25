using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Intern.Share;
using MySql.Data.MySqlClient;

namespace Intern.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    
    public class GmailController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public GmailController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [Route("sendemail")]
        [HttpPost]
        public  async Task SendGmail([FromBody] Gmail gmail)
        {
            MailMessage message = new MailMessage(gmail.from,gmail.to,gmail.sub,gmail.body);
            message.BodyEncoding=System.Text.Encoding.UTF8;
            message.SubjectEncoding=System.Text.Encoding.UTF8;
            message.IsBodyHtml=true;
            message.Sender = new MailAddress(gmail.from);
            using var smtpClient = new SmtpClient("smtp.gmail.com");
            smtpClient.Port = 587;
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new NetworkCredential(gmail.gmail, gmail.password);
            await smtpClient.SendMailAsync(message);

        }

        [Route("note_sended_mail_round1")]
        [HttpPut]
        public bool NoteSendedMailRound1([FromBody] Candidate candidate)
        {
            string ConnectionString = _configuration.GetConnectionString("Default");
            using (MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                string sqlText = $"UPDATE intern SET SendMail='Đã gửi mail vòng 1', CanContact='Chưa', Status={5} WHERE ID={candidate.ID}";
                MySqlCommand cmd = new MySqlCommand(sqlText, conn);
                conn.Open();
                int i = cmd.ExecuteNonQuery();
                conn.Close();
                return i >= 1 ? true : false;

            }
        }

        [Route("note_sended_mail_round2")]
        [HttpPut]
        public bool NoteSendedMailRound2([FromBody] Candidate candidate)
        {
            string ConnectionString = _configuration.GetConnectionString("Default");
            using (MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                string sqlText = $"UPDATE intern SET SendMail='Đã gửi mail vòng 2', CanContact='Chưa',Status={7}  WHERE ID={candidate.ID}";
                MySqlCommand cmd = new MySqlCommand(sqlText, conn);
                conn.Open();
                int i = cmd.ExecuteNonQuery();
                conn.Close();
                return i >= 1 ? true : false;

            }
        }

        [Route("note_planned")]
        [HttpPut]
        public bool NotePlanned([FromBody] Candidate candidate)
        {
            string ConnectionString = _configuration.GetConnectionString("Default");
            using (MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                string sqlText = $"UPDATE intern SET Planned={1}  WHERE ID={candidate.ID}";
                MySqlCommand cmd = new MySqlCommand(sqlText, conn);
                conn.Open();
                int i = cmd.ExecuteNonQuery();
                conn.Close();
                return i >= 1 ? true : false;

            }
        }

        [Route("save_time_and_location_interview")]
        [HttpPut]
        public bool SaveTimeandLocationInterView([FromBody] Candidate candidate)
        {
            string ConnectionString = _configuration.GetConnectionString("Default");
            using (MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                string sqlText = $"UPDATE intern SET TimeInterview=@TimeInterview, LocationInterview='{candidate.LocationInterview}'  WHERE ID={candidate.ID}";
                MySqlCommand cmd = new MySqlCommand(sqlText, conn);
                cmd.Parameters.AddWithValue("@TimeInterView", candidate.TimeInterview);
                conn.Open();
                int i = cmd.ExecuteNonQuery();
                conn.Close();
                return i >= 1 ? true : false;

            }
        }
        [Route("save_interview_people")]
        [HttpPut]
        public bool SaveInterviewPeople([FromBody] Candidate candidate)
        {
            string ConnectionString = _configuration.GetConnectionString("Default");
            using (MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                string sqlText = $"UPDATE intern SET InterviewPeople='{candidate.InterviewPeople}' WHERE ID={candidate.ID}";
                MySqlCommand cmd = new MySqlCommand(sqlText, conn);
                conn.Open();
                int i = cmd.ExecuteNonQuery();
                conn.Close();
                return i >= 1 ? true : false;

            }
        }
    }
}
