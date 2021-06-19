using alertBattery.ViewModels;
using GalaSoft.MvvmLight.Command;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;

namespace alertBattery.Pages
{
    public class SettingViewModel : BaseViewModel
    {
        private double levelLow;
        public double LevelLow
        {
            get => levelLow;
            set => SetProperty(ref levelLow, value);
        }
        private double levelFull;
        public double LevelFull
        {
            get => levelFull;
            set => SetProperty(ref levelFull, value);
        }
        private bool isRunning;
        public bool IsRunning
        {
            get => isRunning;
            set => SetProperty(ref isRunning, value);
        }
        private bool isVisible;
        public bool IsVisible
        {
            get => isVisible;
            set => SetProperty(ref isVisible, value);
        }
        private bool isEnabled;
        public bool IsEnabled
        {
            get => isEnabled;
            set => SetProperty(ref isEnabled, value);
        }
        public ICommand ClosePopupCommand => new RelayCommand(Close);
        public ICommand SaveCommand => new RelayCommand(Save);
        public SettingViewModel()
        {
            IsRunning = false;
            isVisible = false;
            IsEnabled = true;
            LoadLevels();
        }
        private async void Close()
        {
            await PopupNavigation.PopAsync();
        }
        private void LoadLevels()
        {
           
            LevelFull = Double.Parse(Preferences.Get("levelFull", "default_value"));
            LevelLow = Double.Parse(Preferences.Get("levelLow", "default_value"));
        }
        private async void Save()
        {
            IsEnabled = false;
            IsRunning = true;
            isVisible = true;
            Preferences.Set("levelLow", LevelLow);
            Preferences.Set("levelFull", LevelFull);   
            Close();
        }
    }
}
