using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shikisha.DataAccess
{
    /// <summary>
    /// Abstract class used to setup standard properties (such as inserted/update datetime)
    /// for all entities in the data context.
    /// </summary>
    public abstract class EntityBase
    {
        [Key]
        public Guid Id { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime InsertedUtc { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdatedUtc { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public EntityBase(string name, string description) =>
        (this.Name, this.Description) =
        (name, description);

        public EntityBase(){}
    }
}