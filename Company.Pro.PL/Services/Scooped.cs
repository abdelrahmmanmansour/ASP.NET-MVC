
namespace Company.Pro.PL.Services
{
    public class Scooped : IScooped
    {
        public Scooped()
        {
            Guid = Guid.NewGuid();
        }
        public Guid Guid { get ; set ; }

        public string GetGuid()
        {
           return Guid.ToString();
        }
    }
}
