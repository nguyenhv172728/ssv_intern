using System.ComponentModel.DataAnnotations;

namespace Intern.Share
{
    public class UploadFileInfo
    {
        //public UploadFileInfo() { }

        //public UploadFileInfo(int teamid, string teamname)
        //{
        //    TeamID = teamid;    
        //    TeamName = teamname;
        //}
        [Key]
        public int FileID { get; set; } 
        public string FileName { get; set; } 
        public byte[] Filse { get; set; }

        public string ContentType { get; set; }
        public string Base64File { get; set; }



    }
}