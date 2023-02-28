﻿using System.ComponentModel.DataAnnotations;

namespace RapidPayITL.Model
{
    public class Card
    {
        [Required] 
        [StringLength(15, ErrorMessage = "Incorrect Card number, it must be 15 digits long", MinimumLength = 15)]
        [RegularExpression("[0-9]{15}", ErrorMessage ="This card number is invalid")]
        public string CardNumber { get; set; }
        [Required]
        public string HolderName { get; set; }
    }
}
