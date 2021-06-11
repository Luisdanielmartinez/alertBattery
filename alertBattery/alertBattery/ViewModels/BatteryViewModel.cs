
using alertBattery.Pages;
using alertBattery.Services.Notifications;
using GalaSoft.MvvmLight.Command;
using Matcha.BackgroundService;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace alertBattery.ViewModels
{
   public class BatteryViewModel : BaseViewModel, IPeriodicTask
    {
        //#8BC34A
        private Color colorBattery;
        private double levelLow;
        private double levelFull;
        public Color ColorBattery
        {
            get => colorBattery;
            set => SetProperty(ref colorBattery, value);
        }
     
        private bool isCharging;
        public bool IsCharging
        {
            get => isCharging;
            set => SetProperty(ref isCharging, value);
        }
        private double level;
        public double Level
        {
            get => level;
            set => SetProperty(ref level, value);
        }
        private double changedLevelColor;
        public double ChangedLevelColor
        {
            get => changedLevelColor;
            set => SetProperty(ref changedLevelColor, value);
        }
        public ICommand OpenSettingPopupCommand => new RelayCommand(OpenSetting);
        public BatteryViewModel(int seconds)
        {
            levelLow = 10.0;
            levelFull = 100;
           ColorBattery = Color.FromHex("#8BC34A");
           Interval = seconds!=0?TimeSpan.FromSeconds(seconds): TimeSpan.FromSeconds(120);
            
            IsCharging = false;
        }
        public async void OpenSetting()
        {
           await PopupNavigation.PushAsync(new SettingPage(),true);
        }
        public TimeSpan Interval { get; set; }

        public async Task<bool> StartJob()
        {
            // YOUR CODE HERE
            // THIS CODE WILL BE EXECUTE EVERY INTERVAL
            CheckedBattery();
            return true;
        }
        public async void CheckedBattery()
        {
            bool hasKey = Preferences.ContainsKey("levelLow");
            if (hasKey)
            {
                levelFull= Double.Parse( Preferences.Get("levelFull", "default_value"));
                levelLow = Double.Parse(Preferences.Get("levelLow", "default_value"));
            }
            else
            {
                Preferences.Set("levelLow", levelLow);
                Preferences.Set("levelFull", levelFull);
            }
            var level = Battery.ChargeLevel;
            ColorBattery = level<10.0? Color.FromHex("#FA4A24") : Color.FromHex("#8BC34A");
               Level = level;
            ChangedLevelColor = 1.5 * level;
            var state = Battery.State;
      
            if (level<= levelLow)
            {
                ColorBattery = Color.FromHex("#FA4A24");
                await LocalNotification.SendNotificationNow("Informacion", "Bateria baja.");
            }else if (level > 25.0 && level <= 50.0)
            {
                ColorBattery = Color.FromHex("#FAD624");
            }else if (level > 50.0 && level <= 75.0)
            {
                ColorBattery = Color.FromHex("#8BC34A");
            }else if (level == levelFull)
            {
              await  LocalNotification.SendNotificationNow("Informacion", "Se ha cargado la bateria");
            }

            switch (state)
            {
                case BatteryState.Charging:
                    // Currently charging
                    IsCharging = true;
                    break;
                case BatteryState.Full:
                    // Battery is full
                    ColorBattery = Color.FromHex("#8BC34A");
                    break;
                case BatteryState.Discharging:
                case BatteryState.NotCharging:
                    // Currently discharging battery or not being charged
                    break;
                case BatteryState.NotPresent:
                    // Battery doesn't exist in device (desktop computer)
                    break;
                case BatteryState.Unknown:
                    // Unable to detect battery state
                    break;
            }
        }
    }
}
