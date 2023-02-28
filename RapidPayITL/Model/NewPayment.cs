using System.ComponentModel.DataAnnotations;

namespace RapidPayITL.Model
{
    public class NewPayment
    {
        [Required]
        [StringLength(15, ErrorMessage = "Incorrect Card number, it must be 15 digits long", MinimumLength = 15)]
        [RegularExpression("[0-9]{15}", ErrorMessage = "This card number is invalid")]
        public string CardNumber { get; set; }

        [Required, Range(0.01, 999999999.99, ErrorMessage = "Amount must be a decimal value between 0.01 and 999999999.99")]
        public decimal Amount { get; set; }

        public string PaymentBeneficiary { get; set; }

        [Required]
        public DateTime PaymentDate { get; set; }
    }
}
