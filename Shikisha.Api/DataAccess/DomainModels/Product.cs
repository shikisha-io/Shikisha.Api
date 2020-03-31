using System;
using System.ComponentModel.DataAnnotations;
using Shikisha.DataAccess;

namespace Shikisha.DataAccess.DomainModels
{
    /// <summary>
    /// Represents a Product that can have projects.
    /// </summary>
    public class Product : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        

        public Product(string name, string description) =>
        (this.Name, this.Description) =
        (name, description);

        public Product() {}
    }
}
