using CommunityToolkit.Mvvm.Input;
using R52_Last_Evidence_App.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace R52_Last_Evidence_App.ViewModels
{
    [QueryProperty(nameof(Id), "id")]
    public partial class DeviceListViewModel : ObservableCollection<DeviceViewModel>
    {
        private string baseAddress;
        HttpClient http;

        public DeviceListViewModel()
        {
            baseAddress = DeviceInfo.Platform == DevicePlatform.Android ?
                "http://10.0.2.2:5248" : "http://localhost:5248";
            http = new HttpClient { BaseAddress = new Uri(baseAddress) };
        }
        private int _id;
        public int Id { get=>_id; set 
            { 
               _id = value;
                
            } }
        public async void GetAllAsync(bool clearList = true)
        {
            if (clearList)
                this.Clear();
            var data = await http.GetFromJsonAsync<List<DeviceViewModel>>("api/devices");
            foreach (var item in data)
            {
                item.RemotePicture = $"{baseAddress}/Pictures/{item.Picture}";
                this.Add(item);
            }
        }
        public  DeviceViewModel Get(int id)
        {
            
            var data= this.FirstOrDefault(x=> x.DeviceId == id);
          
                
            return data;
           
        }
        public async Task<bool> AddDevice(DeviceViewModel device)
        {
            var response = await http.PostAsJsonAsync<DeviceViewModel>("api/Devices", device);
            if (response != null)
            {
                GetAllAsync();
                return true;
            }
            return false;
        }
        public async Task<bool> UpdateDevice(DeviceViewModel device)
        {
            var response = await http.PutAsJsonAsync<DeviceViewModel>($"api/Devices/{device.DeviceId}", device);
            if (response.IsSuccessStatusCode)
            {
                GetAllAsync();
                return true;
            }
            return false;
        }
        public async Task<ImageUploadResponse> UploadAsync(MultipartFormDataContent fileContent)
        {
            var response = await http.PostAsync("api/Devices/Upload", fileContent);
            var r = await response.Content.ReadFromJsonAsync<ImageUploadResponse>();
            return r == null ? new ImageUploadResponse() : r;
        }
        [RelayCommand]
        public async Task DisplayAction(DeviceViewModel device)
        {
            var response = await AppShell.Current.DisplayActionSheet("Select Option", "OK", null,"Spec List",  "Edit", "Delete");
            if (response == "Edit")
            {
                var navParam = new Dictionary<string, object>();
                try
                {
                   
                    navParam.Add("Device", device);

                    await Shell.Current.GoToAsync($"EditDevicePage?id={device.DeviceId}");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }




            }
            else if (response == "Delete")
            {
                var  answer= await AppShell.Current.DisplayAlert("Confirm?", "Are your sure to delete?", "Yes", "No");
                if (answer)
                {
                    var r = await this.http.DeleteAsync($"/api/Devices/{device.DeviceId}");
                    this.GetAllAsync();
                    if (r.IsSuccessStatusCode)
                    {
                        await AppShell.Current.DisplayAlert("Success", "Data deleted", "Ok");
                    }
                }

            }
            else if(response == "Spec List")
            {
                await Shell.Current.GoToAsync($"SpecListPage?id={device.DeviceId}");
            }
        }
        

    }
}
