using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Customer
    {
        
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        [NotMapped]
        public IEnumerable<Address> Addresses { get; set; }
    }
}
