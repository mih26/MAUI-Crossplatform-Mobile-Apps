using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace R52_Last_Evidence_App.ViewModels
{
    
    public partial class SpecListViewModel:ObservableCollection<SpecViewModel>
    {
        private string baseAddress;
        HttpClient http;

        public SpecListViewModel()
        {
            baseAddress = DeviceInfo.Platform == DevicePlatform.Android ?
                "http://10.0.2.2:5248" : "http://localhost:5248";
            http = new HttpClient { BaseAddress = new Uri(baseAddress) };
        }
       
        public async void GetAllAsync(bool clearList = true, int id=0)
        {
            if (clearList)
                this.Clear();
            if(id == 0)
            {
                var data = await http.GetFromJsonAsync<List<SpecViewModel>>("api/Specs");
                foreach(var item in data)
                {
                    Add(item);
                }
            }
            else
            {
                var data = await http.GetFromJsonAsync<List<SpecViewModel>>($"api/Specs/Device/Specs/{id}");
                foreach (var item in data)
                {
                    Add(item);
                }
            }
           
            
        }
        public async Task<bool> AddSpec(SpecViewModel spec)
        {
            var response = await http.PostAsJsonAsync<SpecViewModel>("api/Specs", spec);
            if (response != null)
            {
                GetAllAsync();
                return true;
            }
            return false;
        }
       
    }
}
