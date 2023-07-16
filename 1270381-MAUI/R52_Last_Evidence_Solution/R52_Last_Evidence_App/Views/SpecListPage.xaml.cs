using R52_Last_Evidence_App.ViewModels;

namespace R52_Last_Evidence_App.Views;
[QueryProperty(nameof(DeviceId), "id")]
public partial class SpecListPage : ContentPage
{
    SpecListViewModel model;
	public SpecListPage(SpecListViewModel model)
	{
		InitializeComponent();
		this.model = model;
		//this.model.GetAllAsync();
		this.BindingContext = this.model;
	}
    private int _id;
    public int DeviceId
    {
        get => this._id;
        set
        {
            this._id = value;
            this.model.GetAllAsync(true, this._id);
        }

    }

    private async void Add_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"AddSpecPage?id={DeviceId}");
    }

    private async void Back_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"DeviceListPage");
    }
}