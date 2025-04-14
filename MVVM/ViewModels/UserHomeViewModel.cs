using System.ComponentModel;
using System.Runtime.CompilerServices;
using TaskManager.MVVM.Models;
using TaskManager.MVVM.Views;

namespace TaskManager.MVVM.ViewModels;

public class UserHomeViewModel : INotifyPropertyChanged
{
    public User User { get; set; }

    public Schedule Schedule { get; set; }

    public Command ViewAllTasksCommand { get; }
    public Command ViewProjectsCommand { get; }
    public Command CreateTaskCommand { get; }
    public Command CreateScheduleCommand { get; }

    public UserHomeViewModel(User user)
    {
        this.User = App.UserRepo.GetEntityWithChildren(user.Id);
        if(User.Schedule != null)
        {
            if (User.Schedule.Stages == null | user.Schedule.Stages.Count == 0)
            {
                Schedule schedule = new Schedule();
                schedule.Name = "";
                Stage stage = new Stage();
                stage.Name = "Geen rooster";
                schedule.Stages.Add(stage);
                this.User.Schedule = schedule;
                this.Schedule = this.User.Schedule;
            }
            this.User.Schedule = App.ScheduleRepo.GetEntityWithChildren(user.Schedule.Id);
            this.Schedule = User.Schedule;
            if(User.Schedule.Stages == null)
            {
                Stage stage = new Stage();
                stage.Name = "Geen dagen";
                user.Schedule.Stages.Add(stage);
            }
        }
        else
        {
            Schedule schedule = new Schedule();
            schedule.Name = "";
            Stage stage = new Stage();
            stage.Name = "Geen rooster";
            schedule.Stages.Add(stage);
            this.User.Schedule = schedule;
            this.Schedule = this.User.Schedule;
        }

            ViewAllTasksCommand = new Command(OnViewAllTasks);
        ViewProjectsCommand = new Command(OnViewProjects);
        CreateTaskCommand = new Command(OnCreateTask);
        CreateScheduleCommand = new Command(OnCreateSchedule);
    }

    private void OnViewAllTasks()
    {

        Application.Current.MainPage.Navigation.PushModalAsync(new TasksOfUserView(User));
    }

    private void OnCreateSchedule()
    {
        Application.Current.MainPage.Navigation.PushModalAsync(new CreateSchedulePage(User));
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