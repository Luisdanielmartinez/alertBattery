
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
            get => level*100;
            set => SetProperty(ref level, value);
        }
        private double changedLevelColor;
        public double ChangedLevelColor
        {
            get => changedLevelColor;
            set => SetProperty(ref changedLevelColor, value);
        }
        public ICommand OpenSettingPopupCommand => new RelayCommand(OpenSetting);
        public BatteryState state;
        public BatteryState State
        {
            get => state;
            set => SetProperty(ref state, value);
        }
        public BatteryViewModel(int seconds)
        {
            levelLow = 0.1;
            levelFull = 1.0;
            ColorBattery = Color.FromHex("#8BC34A");
            Interval = seconds!=0?TimeSpan.FromSeconds(seconds): TimeSpan.FromSeconds(120);
            Battery.BatteryInfoChanged += Battery_BatteryInfoChanged;
            State = Battery.State;
            Level = Battery.ChargeLevel;
            IsCharging = Battery.State.Equals(BatteryState.Charging);
            CheckedBattery();
        }
        void Battery_BatteryInfoChanged(object sender, BatteryInfoChangedEventArgs e)
        {
            Level = e.ChargeLevel;
            State = e.State;
            CheckedBattery();
            Console.WriteLine($"Reading: Level: {level}, State: {state}");
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
                levelFull = 0.01* Double.Parse( Preferences.Get("levelFull", "default_value"));
                levelLow = 0.01* Double.Parse(Preferences.Get("levelLow", "default_value"));
            }
            else
            {
                Preferences.Set("levelLow", levelLow);
                Preferences.Set("levelFull", levelFull);
            }
            var level = Battery.ChargeLevel;
            ColorBattery = level<0.1? Color.FromHex("#FA4A24") : Color.FromHex("#8BC34A");
               Level = level;
            ChangedLevelColor = 100 * level * 1.4;
            var state = Battery.State;
            
            if (level<= 0.1)
            {
                ColorBattery = Color.FromHex("#FA4A24");
            }else if (level > 0.1 && level <= 0.50)
            {
                ColorBattery = Color.FromHex("#FAD624");
            }else if (level > 0.50 && level <= 1.0)
            {
                ColorBattery = Color.FromHex("#8BC34A");
            }else if (level == 1.0)
            {
              await  LocalNotification.SendNotificationNow("Informacion", "Se ha cargado la bateria");
            }
            else
            {
                ColorBattery = Color.FromHex("#000000");
            }

            if(level<= levelLow/100)
            {
                await LocalNotification.SendNotificationNow("Informacion", "La bateria ha alcanzado el nivel minimo marcado");
            }
            if (level >= levelFull)
            {
                await LocalNotification.SendNotificationNow("Informacion", "La bateria ha alcanzado el nivel maximo marcado");
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
                    IsCharging = false;
                    break;
                case BatteryState.Discharging:
                case BatteryState.NotCharging:
                    // Currently discharging battery or not being charged
                    IsCharging = false;
                    break;
                case BatteryState.NotPresent:
                    // Battery doesn't exist in device (desktop computer)
                    break;
                case BatteryState.Unknown:
                    // Unable to detect battery state
                    IsCharging = false;
                    break;
            }
        }
    }
}
