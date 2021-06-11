using alertBattery.jobC;
using alertBattery.Pages;
using alertBattery.ViewModels;
using Matcha.BackgroundService;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace alertBattery
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            MainPage = new BatteryPage();
        }

        protected override void OnStart()
        {
            //Register Periodic Tasks
            BackgroundAggregatorService.Add(() => new BatteryViewModel(120));
        

            //Start the background service
            BackgroundAggregatorService.StartBackgroundService();
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
