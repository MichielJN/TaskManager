using System.ComponentModel;
using System.Runtime.CompilerServices;
using TaskManager.MVVM.Views;
using TaskManager.MVVM.Models;
using TaskManager.Security;

namespace TaskManager.MVVM.ViewModels;

public class CreateTaskPageViewModel : INotifyPropertyChanged
{
    private string name;
    private User user;
    private DateTime dateStart;
    private DateTime dateEnd;
    private DateTime timeSpent;
    private DateTime startTimeStudySession;
    private List<TimeSpan> studySessions;
    private int status;
    private string statusText;

    public string Name
    {
        get => name;
        set
        {
            name = value;
            OnPropertyChanged();
        }
    }

    public int Status
    {
        get => status; 
        set { 
            status = value; 
            OnPropertyChanged();
            StatusText = UpdateStatusText();
        }
    }
    public string StatusText
    {
        get => statusText;
        set
        {
            if (statusText != value)
            {
                statusText = value;
                OnPropertyChanged();
            }
        }
    } 



    public Command CreateTaskCommand { get; }

    public CreateTaskPageViewModel(User _user)
    {
        CreateTaskCommand = new Command(OnCreateTask);
        Status = 0;
        this.user = _user;
    }

    private void OnCreateTask()
    {
        ProjectTask _task = new ProjectTask(this.Name, user.Id, user, Status );
        App.ProjectTaskRepo.SaveEntity(_task);
        user.ProjectTasks.Add( _task );
        Application.Current.MainPage.Navigation.PushModalAsync(new UserHome(user));
    }

    public string UpdateStatusText()
    {
        string[] statusNamen = { "Nog niet gestart", "Bezig", "Klaar" };
        return statusNamen[Status];
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string? name = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}