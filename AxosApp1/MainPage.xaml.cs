using AxosApp1.ViewModels;

namespace AxosApp1;

public partial class MainPage : ContentPage
{
	int count = 0;
	MainViewModel vm;
	public MainPage(MainViewModel viewModel)
	{
		BindingContext = vm = viewModel;
		InitializeComponent();
	}

	private void OnCounterClicked(object sender, EventArgs e)
	{
		vm.SelectImageCommand.Execute(null);

	 
	}
}

