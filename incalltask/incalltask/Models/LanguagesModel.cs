using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace incalltask.Models
{
    public class LanguagesModel:BaseEntity
    {
        public string Name { get; set; }
    }
    public class DropDownMenuModel:BaseEntity
    {
        public string HeaderName { get; set; }
        public ObservableCollection<LanguagesModel> DropMenuList { get; set; }
    }
}
