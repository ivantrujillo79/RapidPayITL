using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RapidPayITL.Data.Entity
{
    public class Payment
    {
        [Key] 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(15)]
        public string CardNumber { get; set; }

        [Required]
        public decimal Amount { get; set; }

        public decimal FeeAmount { get; set; }

        public string PaymentBeneficiary { get; set; }

        public DateTime PaymentDate { get; set; }

    }
}
