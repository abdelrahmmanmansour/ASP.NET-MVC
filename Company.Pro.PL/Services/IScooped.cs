namespace Company.Pro.PL.Services
{
    public interface IScooped
    {
        public Guid Guid { get; set; }
        string GetGuid();
    }
}
