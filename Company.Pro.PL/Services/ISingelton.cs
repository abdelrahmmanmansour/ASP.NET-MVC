namespace Company.Pro.PL.Services
{
    public interface ISingelton
    {
        public Guid Guid { get; set; }
        string GetGuid();
    }
}
