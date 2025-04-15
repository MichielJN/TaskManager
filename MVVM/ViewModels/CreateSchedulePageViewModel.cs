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
        schedule = App.ScheduleRepo.SaveEntity(schedule);
        for(int i = 0; i < 7; i++)
        {
            schedule.Days[i].ProjectId = 0;
            schedule.Days[i].ScheduleId = schedule.Id;
            App.StageRepo.SaveEntity(schedule.Days[i]);
        }
        schedule = App.ScheduleRepo.SaveEntity(schedule);

        this.User.Schedule = schedule;
        this.User.ScheduleId = schedule.Id;
        App.UserRepo.SaveEntityWithChildren(this.User);
        Application.Current.MainPage.Navigation.PushModalAsync(new UserHome(this.User));

    }


    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string? name = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}