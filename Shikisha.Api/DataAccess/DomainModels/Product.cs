using System.Collections.Generic;

namespace Shikisha.DataAccess.DomainModels
{
    /// <summary>
    /// Represents a Product that can have projects.
    /// </summary>
    public class Product : EntityBase
    {
        public ICollection<Project> Projects { get; set; }
        public Product(string name, string description) : base(name, description) {}
    }
}
