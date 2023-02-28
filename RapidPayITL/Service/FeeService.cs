using Timer = System.Timers.Timer;

namespace RapidPayITL.Service
{
    public class FeeService
    {
        private decimal currentFeeAmount = 0.01M;
        private double defaultInterval = 36000000;
        private static double creationInterval = 1000;
        private static Timer FeeTimer = new Timer(creationInterval);
        private decimal UEFFactor = 0;
        private int minSeedValue = 0;
        //The exclusive upper bound Ref: https://learn.microsoft.com/en-us/dotnet/api/system.random.next?view=net-7.0#system-random-next(system-int32-system-int32)
        private int maxSeedValue = 2;        

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
            var seed = (float)seedGenerator.Next(minSeedValue, maxSeedValue);
            UEFFactor = (decimal)(seed + seedGenerator.NextSingle());

            var totalAmount = currentFeeAmount * UEFFactor;
            this.currentFeeAmount = totalAmount;
        }
    }
}
