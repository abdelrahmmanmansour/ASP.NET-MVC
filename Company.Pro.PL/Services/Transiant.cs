namespace Company.Pro.PL.Services
{
    public class Transiant : ITransiant
    {
        public Transiant()
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
