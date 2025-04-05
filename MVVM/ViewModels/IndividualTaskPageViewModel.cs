using System.ComponentModel;
using System.Runtime.CompilerServices;
using TaskManager.MVVM.Views;
using TaskManager.MVVM.Models;
using TaskManager.Security;
using Microsoft.Maui.Dispatching;

namespace TaskManager.MVVM.ViewModels;

public class IndividualTaskPageViewModel : INotifyPropertyChanged
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
    private string buttonText;
    private ProjectTask projectTask;
    private bool sessionActive = false;
    private DateTime? sessionStartTime;

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
        set
        {
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

    public string ButtonText
    {
        get => buttonText;
        set
        {
            if (buttonText != value)
            {
                buttonText = value;
                OnPropertyChanged();
            }
        }
    }



    public Command SessionTimerCommand { get; }

    public IndividualTaskPageViewModel(User user, ProjectTask task)
    {
        SessionTimerCommand = new Command(OnSessionTimer);
        this.projectTask = task;
        this.user = user;
        this.Name = task.Name;
        if (projectTask.StudySessionStartTime != null)
        {
            // Sessie is al bezig, timer hervatten
            sessionStartTime = projectTask.StudySessionStartTime;
            StartUITimer();
        }
        else
        {
            ButtonText = "Start studeersessie";
        }
    }

    private void OnSessionTimer()
    {
        if (!sessionActive)
        {
            // Start sessie
            sessionStartTime = DateTime.Now;
            projectTask.StudySessionStartTime = sessionStartTime;
            App.ProjectTaskRepo.SaveEntity(projectTask);

            sessionActive = true;
            StartUITimer();
        }
        else
        {
            // Stop sessie
            if (sessionStartTime.HasValue)
            {
                TimeSpan duration = DateTime.Now - sessionStartTime.Value;
                StudySession studySession = new StudySession(duration, projectTask.Id, projectTask);
                projectTask.studySessions.Add(studySession);
                App.StudySessionRepo.SaveEntity(studySession);
                projectTask.StudySessionStartTime = null;
                App.ProjectTaskRepo.SaveEntity(projectTask);
            }

            sessionStartTime = null;
            sessionActive = false;
            ButtonText = "Start sessie";
        }
    }
    private void StartUITimer()
    {
        Application.Current!.Dispatcher.StartTimer(TimeSpan.FromSeconds(1), () =>
        {
            if (sessionStartTime.HasValue && sessionActive)
            {
                var elapsed = DateTime.Now - sessionStartTime.Value;
                ButtonText = $"Bezig: {elapsed:hh\\:mm\\:ss}";
                return true; // Blijf timer herhalen
            }

            return false; // Stop timer
        });

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
