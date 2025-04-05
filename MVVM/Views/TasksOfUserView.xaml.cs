using TaskManager.MVVM.Models;
using TaskManager.MVVM.ViewModels;

namespace TaskManager.MVVM.Views;

public partial class TasksOfUserView : ContentPage
{
	public TasksOfUserView(User user)
	{
		InitializeComponent();
        BindingContext = new TasksOfUserViewModel(user);
    }
}