using incalltask.Models;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace incalltask.ViewModels
{
    
    public  class HistoryListPageModel:FreshMvvm.FreshBasePageModel
    {
        public ObservableCollection<HistoryModel> HistoryList { get; set; }
        public HistoryModel lastitem { get; set; }
        public ICommand ItemSelectedCommand { get; set; }
        public HistoryListPageModel()
        {
            HistoryList = new ObservableCollection<HistoryModel>();
            ItemSelectedCommand = new Command(ItemSelectedCommandExcute);
            DummyHistoryList();
        }

        private void ItemSelectedCommandExcute(object obj)
        {
            var selecteditem = obj as HistoryModel;

            if (selecteditem == null) return;
            if (selecteditem != lastitem)
            {
                if (lastitem != null)
                {
                    lastitem.makecall = false;
                }
                selecteditem.makecall = true;

            }
            else
            {
                selecteditem.makecall = !selecteditem.makecall;
            }
            lastitem = selecteditem;

        }

        public void DummyHistoryList()
        {
            HistoryList.Add(new HistoryModel { Number = "00966 56 1126458", CallType = "Missed", CallDate = "Oct 22 01:24 PM", makecall = false });
            HistoryList.Add(new HistoryModel { Number = "00966 56 1126458", CallType = "Income", CallDate = "Oct 22 01:24 PM", makecall = false });
            HistoryList.Add(new HistoryModel { Number = "00966 56 1126458", CallType = "outcome", CallDate = "Oct 22 01:24 PM", makecall = false });
            HistoryList.Add(new HistoryModel { Number = "00966 56 1126458", CallType = "Missed", CallDate = "Oct 22 01:24 PM", makecall = false });
            HistoryList.Add(new HistoryModel { Number = "00966 56 1126458", CallType = "Income", CallDate = "Oct 22 01:24 PM", makecall = false });
            HistoryList.Add(new HistoryModel { Number = "00966 56 1126458", CallType = "outcome", CallDate = "Oct 22 01:24 PM", makecall = false });
            HistoryList.Add(new HistoryModel { Number = "00966 56 1126458", CallType = "Missed", CallDate = "Oct 22 01:24 PM", makecall = false });
            HistoryList.Add(new HistoryModel { Number = "00966 56 1126458", CallType = "Income", CallDate = "Oct 22 01:24 PM", makecall = false });
            HistoryList.Add(new HistoryModel { Number = "00966 56 1126458", CallType = "outcome", CallDate = "Oct 22 01:24 PM", makecall = false });
            foreach (var item in HistoryList)
            {
                if (item.CallType == "Income")
                {
                    item.Image = "income.png";
                }
                else if (item.CallType == "outcome")
                {
                    item.Image = "outcome.png";
                }
                else
                {
                    item.Image = "missed.png";
                }

            }
        }

    }
}
   