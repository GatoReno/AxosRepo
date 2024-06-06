 
namespace AxosApp1.Model
{
     public class Country
    {
        public Name Name { get; set; }
        public Flags Flags { get; set; }
    }

    public class Name
    {
        public string Common { get; set; }
    }

    public class Flags
    {
        public string Png { get; set; }
    }
}