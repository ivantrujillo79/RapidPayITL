using System.ComponentModel.DataAnnotations;

namespace RapidPayITL.Data.Entity
{
    public class Card
    {
        [Key]
        [Required]
        [StringLength(15, ErrorMessage ="Incorrect Card number, it must be 15 digits long", MinimumLength = 15)]
        [RegularExpression("[0-9]{15}", ErrorMessage = "This card number is invalid")]
        public string CardNumber { get; set; }
        [Required]
        [StringLength(25, ErrorMessage = "The max Card holder name's lenght must be 25 characters.")]
        public string HolderName { get; set; }
        public List<Payment> Payments { get; set; }
    }
}
