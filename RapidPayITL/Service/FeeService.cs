namespace RapidPayITL.Service
{
    public class FeeService
    {
        private decimal currentFeeAmount = 0.01M;
        private static System.Timers.Timer FeeTimer = new System.Timers.Timer(100);
        private decimal UEFFactor = 0;
        private double defaultInterval = 36000000;

        public decimal FeeAmount { get => this.currentFeeAmount; }



        public FeeService() 
        {
            FeeTimer.AutoReset = true;
            FeeTimer.Enabled = true;
            FeeTimer.Elapsed += CalculateFeeAmount;
            FeeTimer.Start();
            FeeTimer.Interval = defaultInterval;

        }

        private void CalculateFeeAmount(object? sender, System.Timers.ElapsedEventArgs e)
        {
            var seedGenerator = new Random();
            var seed = (float)seedGenerator.Next(0, 1);
            UEFFactor = (decimal)(seed + seedGenerator.NextSingle());

            var totalAmount = currentFeeAmount * UEFFactor;
            this.currentFeeAmount = totalAmount;
        }
    }
}
