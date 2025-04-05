using System.ComponentModel;
using System.Runtime.CompilerServices;
using TaskManager.MVVM.Models;
using TaskManager.MVVM.Views;

namespace TaskManager.MVVM.ViewModels;

public class UserHomeViewModel : INotifyPropertyChanged
{
    public User User { get; set; }

    public Command ViewAllTasksCommand { get; }
    public Command ViewProjectsCommand { get; }
    public Command CreateTaskCommand { get; }

    public UserHomeViewModel(User user)
    {
        this.User = user;
        ViewAllTasksCommand = new Command(OnViewAllTasks);
        ViewProjectsCommand = new Command(OnViewProjects);
        CreateTaskCommand = new Command(OnCreateTask);
    }

    private void OnViewAllTasks()
    {

        Application.Current.MainPage.Navigation.PushModalAsync(new TasksOfUserView(User));
    }

    private void OnViewProjects()
    {


    }

    private void OnCreateTask()
    {
        Application.Current.MainPage.Navigation.PushModalAsync(new CreateTaskPage(User));
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string? name = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}