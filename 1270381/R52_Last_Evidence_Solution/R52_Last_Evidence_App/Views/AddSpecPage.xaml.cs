
using R52_Last_Evidence_App.ViewModels;

namespace R52_Last_Evidence_App.Views;
[QueryProperty(nameof(DeviceId), "id")]
public partial class AddSpecPage : ContentPage
{
	private readonly SpecListViewModel list;
	private SpecViewModel model;
	public AddSpecPage(SpecListViewModel list)
	{
		InitializeComponent();
		this.list = list;
		this.model = new();
		this.BindingContext = this.model;
	}
	int _id;
	public int DeviceId
	{
		get => _id;
		set
		{
			_id = value;
			this.model.DeviceId = _id;
		}
	}

    private  async void Button_Clicked(object sender, EventArgs e)
    {
		var done =await	this.list.AddSpec(this.model);
		if(done)
		{
			this.model = new();
			this.model.DeviceId = this.DeviceId;
			await Shell.Current.DisplayAlert("Success", "Data saved","Ok");
			await Shell.Current.GoToAsync($"SpecListPage?id={DeviceId}");
		}
		else
		{
            await Shell.Current.DisplayAlert("Faled", "Data save failed", "Ok");
        }
    }
}