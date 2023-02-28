namespace RapidPayITL.Model
{
    public class CardBalance
    {
        public string CardNumber { get; set; }
        public string HolderName { get; set; }
        public decimal TotalAmount { get; set; }
        public int Transactions { get; set; }
    }
}
