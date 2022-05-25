namespace Intern.Share
{
    public class Gmail
    {
        public Gmail() { }

        public Gmail(string _from, string _to, string _sub, string _body, string _gmail, string _password)
        {
            from = _from;
            to = _to;   
            sub = _sub; 
            body = _body;   
            gmail = _gmail; 
            password = _password;                    
        }
        public Gmail( string _sub, string _body)
        {
            from = "kesandianguc205@gmail.com";
            to = "nguyen.hv172728@gmail.com";
            sub = _sub;
            body = _body;
            gmail = "kesandianguc205@gmail.com";
            password = "waittodie0802";
        }

        public Gmail(string _to,string _sub, string _body)
        {
            from = "kesandianguc205@gmail.com";
            to = _to;
            sub = _sub;
            body = _body;
            gmail = "kesandianguc205@gmail.com";
            password = "waittodie0802";
        }

        public string? from{get;set;}
        public string? to { get; set; }
        public string? sub { get; set; }
        public string? body { get; set; }
        public string? gmail { get; set; }
        public string? password { get; set; }


    }
}