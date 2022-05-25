namespace Intern.Share
{
    public class Image
    {
        public Image() { }

        public Image(byte[]? image1)
        {
            Image1 = image1;    
           // Src = src;
        }

        public byte[]? Image1 { get; set; } 
       // public string? Src { get; set; } 
    }
}