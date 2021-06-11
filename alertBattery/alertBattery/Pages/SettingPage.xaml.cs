using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace alertBattery.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingPage : PopupPage
    {
        SettingViewModel model;
        public SettingPage()
        {
            InitializeComponent();
            model = new SettingViewModel();
            BindingContext = model;
        }
        void OnSliderValueChanged(object sender, ValueChangedEventArgs args)
        {
            double value = args.NewValue;
            model.LevelLow = value;
            
        }
        void OnSliderValueChangedFull(object sender, ValueChangedEventArgs args)
        {
            double value = args.NewValue;
            model.LevelFull = value;

        }
    }
}