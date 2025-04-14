using System.ComponentModel;
using System.Runtime.CompilerServices;
using TaskManager.MVVM.Models;
using TaskManager.MVVM.Views;

namespace TaskManager.MVVM.ViewModels;

public class CreateSchedulePageViewModel : INotifyPropertyChanged
{
    private float plannedHours = 0;

    public User User { get; set; }

    public float PlannedHours
    {
        get => plannedHours;
        set
        {
            plannedHours = value;
            OnPropertyChanged();
        }
    }

    public Command CreateScheduleCommand { get; }

    public CreateSchedulePageViewModel(User user)
    {
        this.User = user;
        CreateScheduleCommand = new Command(OnCreateSchedule);
    }


    private void OnCreateSchedule()
    {
        Schedule schedule = new Schedule();
        schedule.CreateWeek(User, PlannedHours);
        schedule = App.ScheduleRepo.SaveEntityWithChildren(schedule);


        this.User.Schedule = schedule;
        Application.Current.MainPage.Navigation.PushModalAsync(new UserHome(this.User));

    }

    private void OnCreateTask()
    {
        Application.Current.MainPage.Navigation.PushModalAsync(new CreateTaskPage(User));
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string? name = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}