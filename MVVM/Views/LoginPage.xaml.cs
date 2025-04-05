using TaskManager.Security;
using TaskManager.MVVM.Views;
using TaskManager.MVVM.ViewModels;

namespace TaskManager.MVVM.Views;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
        BindingContext = new LoginViewModel();
    }
}