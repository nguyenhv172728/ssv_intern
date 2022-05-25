namespace Intern.Share
{
    public class HasBeenApply
    {
        public HasBeenApply() { }

        public HasBeenApply(bool applyed)
        {
            Applyed = applyed;  

        }

        public bool Applyed { get; set; } 
        public string? whyignore { get; set; }

    }
}