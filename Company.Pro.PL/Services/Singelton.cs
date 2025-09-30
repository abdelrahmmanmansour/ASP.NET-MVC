namespace Company.Pro.PL.Services
{
    public class Singelton : ISingelton
    {
        public Singelton()
        {
            Guid = Guid.NewGuid();
        }
        public Guid Guid { get; set; }

        public string GetGuid()
        {
            return Guid.ToString();
        }
    }
}
