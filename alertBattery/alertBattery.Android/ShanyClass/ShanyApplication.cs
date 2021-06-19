using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Shiny;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace alertBattery.Droid.ShanyClass
{
    [Application]
    public class ShanyApplication : Application
    {
        public ShanyApplication(IntPtr handle, JniHandleOwnership transfer) : base(handle, transfer)
        {
        }
        public override void OnCreate()
        {
            base.OnCreate();
            Shiny.Notifications.AndroidOptions.DefaultLaunchActivityFlags = Shiny.Notifications.AndroidActivityFlags.FromBackground;
            Shiny.Notifications.AndroidOptions.DefaultNotificationImportance = Shiny.Notifications.AndroidNotificationImportance.High;
            Shiny.Notifications.AndroidOptions.DefaultSmallIconResourceName = "icono";
            AndroidShinyHost.Init(this, platformBuild: services => services.UseNotifications());

        }
    }
}