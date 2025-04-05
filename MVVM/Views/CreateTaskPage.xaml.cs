using TaskManager.MVVM.Models;
using TaskManager.MVVM.ViewModels;

namespace TaskManager.MVVM.Views;

public partial class CreateTaskPage : ContentPage
{
	public CreateTaskPage(User user)
	{
		InitializeComponent();
        BindingContext = new CreateTaskPageViewModel(user);
    }
}