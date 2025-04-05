using TaskManager.MVVM.ViewModels;
using TaskManager.MVVM.Models;

namespace TaskManager.MVVM.Views;

public partial class UserHome : ContentPage
{
	public UserHome(User user)
	{
		InitializeComponent();
        BindingContext = new UserHomeViewModel(user);
    }
}