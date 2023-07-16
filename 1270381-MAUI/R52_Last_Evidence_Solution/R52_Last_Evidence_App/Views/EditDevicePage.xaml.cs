using R52_Last_Evidence_App.ViewModels;
using System.Diagnostics;

using System.Net.Http.Headers;


namespace R52_Last_Evidence_App.Views;

[QueryProperty(nameof(DeviceId), "id")]
public partial class EditDevicePage : ContentPage
{ 
    
    DeviceListViewModel list;
    private IFilePicker _filePicker;
    DeviceViewModel vm;
    FileResult result = null;
    public EditDevicePage(DeviceListViewModel list, IFilePicker _filePicker)
	{
		InitializeComponent();
        this.list = list;
        this._filePicker = _filePicker;
	}
    private int _id;
    public int DeviceId
    {
        get => _id;
        set
        {
            _id = value;
            this.BindingContext= this.vm = this.list.Get(_id);
            Debug.WriteLine(vm.DeviceName);
            lbl.Text = vm.DeviceName;
            OnPropertyChanged(nameof(DeviceId));
        }
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        if (result != null)
        {
            var content = new MultipartFormDataContent();
            byte[] bytes;
            using (var fs = File.OpenRead(result.FullPath))
            {
                using (var ms = new MemoryStream())
                {
                    await fs.CopyToAsync(ms);
                    bytes = ms.ToArray();
                    using (var fileContent = new ByteArrayContent(bytes))
                    {
                        fileContent.Headers.ContentType = new MediaTypeHeaderValue(result.ContentType);
                        content.Add(content: fileContent, name: "\"file\"", fileName: result.FileName);
                        var data = await this.list.UploadAsync(content);
                        if (data != null)
                        {
                            this.vm.Picture = data.StoredFileName;
                            
                        }
                    }
                }
            }

        }
        this.vm.Picture = this.vm.Picture.Substring(this.vm.Picture.LastIndexOf("/")+1);
        var done = await this.list.UpdateDevice(this.vm);
        if (done)
        {
            await DisplayAlert("Success", "Data Saved", "OK");


        }
        else
        {
            await DisplayAlert("Error", "Failed to save", "OK");
        }
        string  baseAddress = DeviceInfo.Platform == DevicePlatform.Android ?
                "http://10.0.2.2:5248" : "http://localhost:5248";
        this.vm.RemotePicture = $"{baseAddress}/Pictures/{this.vm.Picture}";
    }

    private async void Button_Clicked_1(object sender, EventArgs e)
    {
        FileResult result = await _filePicker.PickAsync(new PickOptions
        {
            PickerTitle = "Pick a Photo",
            FileTypes = FilePickerFileType.Images
        });

        if (result == null) return;
        this.result = result;
        this.vm.Picture = result.FileName;
    }
}