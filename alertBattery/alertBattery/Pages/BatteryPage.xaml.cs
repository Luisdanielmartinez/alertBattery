using alertBattery.ViewModels;
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
    public partial class BatteryPage : ContentPage
    {
        BatteryViewModel model;
        public BatteryPage()
        {
            InitializeComponent();
            model = new BatteryViewModel(0);
            BindingContext = model;
        }
        protected override void OnAppearing()
        {
           
            base.OnAppearing();
        }
        protected override void OnDisappearing()
        {
            model.CheckedBattery();
            base.OnDisappearing();
        }

    }
}