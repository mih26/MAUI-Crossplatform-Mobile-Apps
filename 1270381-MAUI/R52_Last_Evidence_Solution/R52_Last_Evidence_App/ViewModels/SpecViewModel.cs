using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R52_Last_Evidence_App.ViewModels
{
    public partial class SpecViewModel:ObservableObject
    {
        [ObservableProperty] public int specId;
        [ObservableProperty] public string specName;
        [ObservableProperty] public string value;
        [ObservableProperty] public int deviceId;
        
    }
}
