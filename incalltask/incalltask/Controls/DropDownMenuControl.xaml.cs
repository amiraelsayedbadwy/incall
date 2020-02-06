using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace incalltask.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DropDownMenuControl 
    {
        public static readonly BindableProperty HeaderTextProperty =
       BindableProperty.Create(nameof(HeaderText), typeof(string), typeof(DropDownMenuControl), string.Empty, BindingMode.TwoWay);
        public static readonly BindableProperty ListSourceProperty =
             BindableProperty.Create(nameof(ListSource), typeof(IEnumerable), typeof(DropDownMenuControl), null, BindingMode.TwoWay);
       
      
        public string HeaderText
        {
            get
            {
                return (string)GetValue(HeaderTextProperty);
            }
            set
            {
                if (value != null)
                    SetValue(HeaderTextProperty, value);
            }
        }
     
        public IEnumerable ListSource
        {
            get
            {
                return (IEnumerable)GetValue(ListSourceProperty);
            }
            set
            {
                if (value != null)
                {
                    SetValue(ListSourceProperty, value);
                }
            }
        }
      
        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            if (!BottomStack.IsVisible)
            {
                headerImage.Source = "droparrow.png";

                BottomStack.IsVisible = true;
                await BottomStack.TranslateTo(0, 0, 250);
            }
            else
            {
                headerImage.Source = "droparrow.png";

                BottomStack.IsVisible = false;
                await BottomStack.TranslateTo(0, -10, 250);
            }
        }

     public DropDownMenuControl()
        {
            InitializeComponent();
        }

    }
}