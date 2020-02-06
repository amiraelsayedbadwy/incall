using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace incalltask.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomeScreenPage : TabbedPage
    {
        public HomeScreenPage()
        {
            InitializeComponent();
            DeviceDisplay.KeepScreenOn = !DeviceDisplay.KeepScreenOn;
        }
    }
}