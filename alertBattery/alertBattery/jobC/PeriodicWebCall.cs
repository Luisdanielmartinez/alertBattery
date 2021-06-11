using alertBattery.Services.Notifications;
using Matcha.BackgroundService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace alertBattery.jobC
{
    public class PeriodicWebCall : IPeriodicTask
    {
        public PeriodicWebCall(int seconds)
        {
            Interval = TimeSpan.FromSeconds(seconds);
        }

        public TimeSpan Interval { get; set; }

        public async Task<bool> StartJob()
        {
            // YOUR CODE HERE
            // THIS CODE WILL BE EXECUTE EVERY INTERVAL
            await LocalNotification.SendNotificationNow("Informacion", "Bateria baja.");
            return true;
        }

     
    }
}
