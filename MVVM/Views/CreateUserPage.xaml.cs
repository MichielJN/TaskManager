using System.Runtime.CompilerServices;
using TaskManager.Security;
using TaskManager.MVVM.Models;
using TaskManager.MVVM.ViewModels;

namespace TaskManager.MVVM.Views;

public partial class CreateUserPage : ContentPage
{
	public CreateUserPage()
	{
		InitializeComponent();
        BindingContext = new CreateUserPageViewModel();

    }

	private void SaveUser_Clicked(object sender, EventArgs e)
	{

		
	}
}