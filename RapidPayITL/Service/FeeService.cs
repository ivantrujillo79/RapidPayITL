namespace RapidPayITL.Service
{
    public class FeeService
    {
        private static System.Timers.Timer FeeTimer = new System.Timers.Timer(100);



        public FeeService() 
        {
            FeeTimer.Elapsed += CalculateFeeAmount;
            FeeTimer.Start();
            FeeTimer.Interval = 36000000;

        }

        private void CalculateFeeAmount(object? sender, System.Timers.ElapsedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
