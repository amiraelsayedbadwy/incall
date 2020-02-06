using FreshMvvm.Popups;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace incalltask.ViewModels
{
   public class WrongUserPopupPageModel : FreshMvvm.FreshBasePageModel
    {
         
        public string wrongmessage { get; set; }
        public ICommand OkCommand { get; set; }
        public WrongUserPopupPageModel()
        {
            OkCommand = new Command(OkCommandExcute);
        }

        private async void OkCommandExcute(object obj)
        {
            await CoreMethods.PopPopupPageModel();
            
        }
        public override void Init(object initData)
        {
            base.Init(initData);

            wrongmessage = initData.ToString();


        }
    }
}
