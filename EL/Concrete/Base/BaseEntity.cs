using System.ComponentModel.DataAnnotations;

namespace EL.Concrete.Base
{
    public class BaseEntity
    {
        [Key]
        public Guid Key { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow.AddHours(3);
    }
}
