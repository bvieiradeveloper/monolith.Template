using System.ComponentModel.DataAnnotations;

namespace InfraStructure.Model.Payment
{
    public class Transaction
    {
        [Key,Required]
        public string Id { get; set; }
        [Required]
        public string Order_Id { get; set; }
        [Required]
        public decimal Amount { get; set; }
        [Required]
        public string Status { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        [Required]
        public DateTime UpdatedAt { get; set; }
    }
}
