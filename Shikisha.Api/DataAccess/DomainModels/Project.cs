
namespace Shikisha.DataAccess.DomainModels
{
    /// <summary>
    /// Represents a Project.
    /// </summary>
    public class Project : EntityBase
    {
        public Project(string name, string description) : base(name, description) {}
        public Project() : base() {}

        public Product Product { get; set; }
    }
}
