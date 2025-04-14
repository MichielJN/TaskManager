using TaskManager.MVVM.Models;
using TaskManager.MVVM.ViewModels;

namespace TaskManager.MVVM.Views;

public partial class CreateSchedulePage : ContentPage
{
	public CreateSchedulePage(User user)
	{
		InitializeComponent();
		BindingContext = new CreateSchedulePageViewModel(user);
    }
}