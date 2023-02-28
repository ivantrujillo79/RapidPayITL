namespace RapidPayITL.Service
{
    public class FeeService
    {
        private decimal currentFeeFactor = 0.01M;
        private static System.Timers.Timer FeeTimer = new System.Timers.Timer(100);

        public decimal FeeFactor { get => currentFeeFactor; }



        public FeeService() 
        {
            FeeTimer.Elapsed += CalculateFeeAmount;
            FeeTimer.Start();
            FeeTimer.Interval = 36000000;

        }

        private void CalculateFeeAmount(object? sender, System.Timers.ElapsedEventArgs e)
        {
            this.currentFeeFactor = 500.0M;
        }
    }
}
