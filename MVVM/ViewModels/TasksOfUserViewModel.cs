using System.ComponentModel;
using System.Runtime.CompilerServices;
using TaskManager.MVVM.Views;
using TaskManager.MVVM.Models;
using TaskManager.Security;

namespace TaskManager.MVVM.ViewModels;

public class TasksOfUserViewModel : INotifyPropertyChanged
{

    private User user;
    private List<ProjectTask> notStartedTasks;
    private List<ProjectTask> startedTasks;
    private List<ProjectTask> finishedTasks;

    public List<ProjectTask> NotStartedTasks { get; set; } 
    public List<ProjectTask> StartedTasks { get; set; }
    public List<ProjectTask> FinishedTasks { get; set; }




    public Command<ProjectTask> TaskTappedCommand { get; }
    public Command BackCommand { get; }

    public TasksOfUserViewModel(User _user)
    {
        BackCommand = new Command(OnBackPressed);
        TaskTappedCommand = new Command<ProjectTask>(OnTaskTapped);
        this.user = _user;
        this.NotStartedTasks = _user.ProjectTasks.FindAll(x => x.Status == 0).ToList();
        this.StartedTasks = _user.ProjectTasks.FindAll(x => x.Status == 1).ToList();
        this.FinishedTasks = _user.ProjectTasks.FindAll(x => x.Status == 2).ToList();
    }

    private void OnTaskTapped(ProjectTask tappedTask)
    {
        Application.Current.MainPage.Navigation.PushModalAsync(new IndividualTaskPage(user, tappedTask));
    }
    private void OnBackPressed()
    {
        Application.Current.MainPage.Navigation.PushModalAsync(new UserHome(this.user));
    }


    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string? name = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}