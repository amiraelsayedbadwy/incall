using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace incalltask.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WrongUserPopupPage : PopupPage
    {
        public WrongUserPopupPage()
        {
            InitializeComponent();
        }
    }
}