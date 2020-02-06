using FreshMvvm.Popups;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace incalltask.ViewModels
{
   public class OfflinePopPageModel:FreshMvvm.FreshBasePageModel
    {
        public ICommand ClosePopUpCommand { get; set; }
        public ICommand OkCommand { get; set; }
        public OfflinePopPageModel()
        {
            ClosePopUpCommand = new Command(ClosePopUpCommandExcute);
            OkCommand = new Command(OkCommandExcute);
        }

        private async  void OkCommandExcute(object obj)
        {
            await CoreMethods.PopPopupPageModel();
           // await CoreMethods.PopToRoot(false);
        }

        private async void ClosePopUpCommandExcute(object obj)
        {
            await CoreMethods.PopPopupPageModel();
          //  await CoreMethods.PopToRoot(false);
        }
    }
}
