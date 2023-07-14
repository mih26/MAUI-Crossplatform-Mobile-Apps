using R52_Last_Evidence_App.ViewModels;
using System.Net.Http.Headers;

namespace R52_Last_Evidence_App.Views;

public partial class AddDevicePage : ContentPage
{
	private IFilePicker _filePicker;
    private readonly DeviceListViewModel list;
    DeviceViewModel model;
    FileResult result = null;
    public AddDevicePage (IFilePicker filePicker, DeviceListViewModel list)
	{
        InitializeComponent();
        this._filePicker = filePicker;
		this.list = list;
        this.model = new();
        model.ReleaseDate = DateTime.Now;
        this.BindingContext = model;
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
                            this.model.Picture = data.StoredFileName;
                            var done = await this.list.AddDevice(this.model);
                            if (done)
                            {
                                await DisplayAlert("Success", "Data Saved", "OK");
                                this.model = new();
                                model.ReleaseDate = DateTime.Now;
                            }
                            else
                            {
                                await DisplayAlert("Error", "Failed to save", "OK");
                            }
                        }
                    }
                }
            }

        }
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
        this.model.Picture = result.FileName;
    }
}