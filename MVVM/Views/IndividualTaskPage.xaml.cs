using TaskManager.MVVM.Models;
using TaskManager.MVVM.ViewModels;

namespace TaskManager.MVVM.Views;

public partial class IndividualTaskPage : ContentPage
{
	public IndividualTaskPage(User user, ProjectTask task)
	{
		InitializeComponent();
        BindingContext = new IndividualTaskPageViewModel(user, task);
    }
}