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
    public class ImageController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public ImageController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //public byte[] GetImage(string str)
        //{
        //    byte[] bytes = null;
        //    if (!string.IsNullOrEmpty(str))
        //    {
        //       bytes = Convert.FromBase64String(str);
        //    }
        //    return bytes;
        //}

        [Route("getsaved")]
        [HttpGet]
        public List<Image> Get()
        {
            string ConnectionString = _configuration.GetConnectionString("Default");
            List<Image> images = new List<Image>();
            using (MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM test", conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                byte[]? image;
                


                while (reader.Read())
                {
                    //image = (byte[])reader["Image"];

                    if (reader["Image"] == DBNull.Value)
                    {
                        image = null;
                    }
                    else
                    {
                        image = (byte[])reader["Image"];
                    }

                    Image image1 = new Image(image);
                    images.Add(image1);
                }
                reader.Close();
                cmd.Dispose();
                conn.Close();
               // images[0].Image1 = Convert.FromBase64String(Convert.ToBase64String(images[0].Image1));
               // images[0].Src = string.Format("data:image/jpg;base64," + Convert.ToBase64String(images[0].Image1));
                return images;
            }

        }


        [Route("post")]
        [HttpPost]

        public bool Save([FromBody] Image image)
        {
            string ConnectionString = _configuration.GetConnectionString("Default");
            using (MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                string sqlText = "INSERT INTO test(Image) VALUES (@Image)";
                MySqlCommand cmd = new MySqlCommand(sqlText, conn);
                cmd.Parameters.AddWithValue("@Image", image.Image1);
               // cmd.Parameters.AddWithValue("@Src", image.Src);

                conn.Open();
                int i = cmd.ExecuteNonQuery();
                conn.Close();
                return i >= 1 ? true : false;

            }
        }

        [Route("savechange")]
        [HttpPost]

        public bool SaveChange(byte[] filebyte, [FromQuery] Image image)
        {
            image.Image1 = filebyte;

            return Save(image);


        }

        //public byte[] GetImage(string str)
        //{
        //    byte[] bytes = null;
        //    if (!string.IsNullOrEmpty(str))
        //    {
        //        bytes = Convert.FromBase64String(str);  
        //    }
        //    return bytes;   
        //}'



        [HttpGet]
        [Route("load")]
        public Test PhysicalLocation()
        {
            string physicalPath = "wwwroot/data/fi.pdf";
            byte[] pdfBytes = System.IO.File.ReadAllBytes(physicalPath);
            string imgSrc = $"data:application/pdf;base64,{Convert.ToBase64String(pdfBytes)}";
            Test test = new Test();
            test.Src = imgSrc;  
            return test;    
        }

    }
}
