using alertBattery.Models;
using Shiny;
using Shiny.Notifications;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace alertBattery.Services.Notifications
{
  public  class LocalNotification
    {
        public static Task SendNotificationNow(string title,string description)
        {
            var notification = new Notification
            {
                Title = title,
                Message = description,
            };

            return ShinyHost.Resolve<INotificationManager>().RequestAccessAndSend(notification);
        }

        public Task ScheduleLocalNotification(in DateTimeOffset scheduleDate)
        {
            var notification = new Notification
            {
                Title = "Testing Local Notifications",
                Message = $"Scheduled for {scheduleDate}",
                ScheduleDate = scheduleDate,
            };

            return ShinyHost.Resolve<INotificationManager>().RequestAccessAndSend(notification);
        }
    }
}
