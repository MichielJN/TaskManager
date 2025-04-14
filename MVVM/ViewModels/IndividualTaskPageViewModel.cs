using System.ComponentModel;
using System.Runtime.CompilerServices;
using TaskManager.MVVM.Views;
using TaskManager.MVVM.Models;
using TaskManager.Security;
using Microsoft.Maui.Dispatching;
using System.Collections.ObjectModel;


namespace TaskManager.MVVM.ViewModels;

public class IndividualTaskPageViewModel : INotifyPropertyChanged
{
    private string name;
    private User user;
    private DateTime dateStart;
    private DateTime dateEnd;
    private DateTime timeSpent;
    private DateTime startTimeStudySession;
    private int status;
    private string statusText;
    private string buttonText;
    private ProjectTask projectTask;
    private bool sessionActive = false;
    private DateTime? sessionStartTime;
    private TimeSpan totalSpentTime = TimeSpan.Zero;

    public ObservableCollection<StudySession> StudySessions { get; set; } = new ObservableCollection<StudySession>();
    public TimeSpan TotalSpentTime
    {
        get => totalSpentTime;
        set
        {
            totalSpentTime = value;
            OnPropertyChanged();
        }
    }
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
            this.projectTask.Status = status;
            App.ProjectTaskRepo.SaveEntity(this.projectTask);
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
    public Command BackCommand  { get; }

    public IndividualTaskPageViewModel(User user, ProjectTask task)
    {

        SessionTimerCommand = new Command(OnSessionTimer);
        BackCommand = new Command(OnBackPressed);
        this.projectTask = App.ProjectTaskRepo.GetEntityWithChildren(task.Id);
       // this.projectTask.studySessions = App.StudySessionRepo.GetEntitiesWithChildren().FindAll(x => x.ProjectTaskId == this.projectTask.Id);
        this.StudySessions = new ObservableCollection<StudySession>(projectTask.studySessions);
        this.user = user;
        this.Name = task.Name;
        if (projectTask.StudySessionStartTime != null)
        {
            sessionStartTime = projectTask.StudySessionStartTime;
            sessionActive = true;
            StartUITimer();
        }
        else
        {
            ButtonText = "Start studeersessie";
        }
    }

    private void OnBackPressed()
    {
        Application.Current.MainPage.Navigation.PushModalAsync(new TasksOfUserView(this.user));
    }

    private void OnSessionTimer()
    {  
        if (!sessionActive)
        {
            
            sessionStartTime = DateTime.Now;
            projectTask.StudySessionStartTime = sessionStartTime;
            App.ProjectTaskRepo.SaveEntity(projectTask);

            sessionActive = true;
            StartUITimer();
        }
        else
        {
         
            if (sessionStartTime.HasValue)
            {
                TimeSpan duration = DateTime.Now - sessionStartTime.Value;
                DateTime timeEnd = DateTime.Now;
                string labelText = $"Gestartte tijd: {projectTask.StudySessionStartTime?.ToString("dd-MM-yyyy HH:mm")} Eindtijd: {timeEnd.ToString("dd-MM-yyyy HH:mm")} Totaal bestede tijd: {duration.ToString("hh\\:mm")}";
                StudySession studySession = new StudySession(duration, projectTask.Id, projectTask, (DateTime)projectTask.StudySessionStartTime, timeEnd, labelText);
                projectTask.studySessions.Add(studySession);
                App.StudySessionRepo.SaveEntity(studySession);
                projectTask.StudySessionStartTime = null;
                App.ProjectTaskRepo.SaveEntity(projectTask);
                this.StudySessions.Add(studySession);
                this.TotalSpentTime += duration;
            }

            sessionStartTime = null;
            sessionActive = false;
            ButtonText = "Start sessie";
        }
        foreach(StudySession studySession in this.StudySessions)
        {
            this.totalSpentTime += studySession.ElapsedTime;
        }
    }
    private void StartUITimer()
    {
        Application.Current!.Dispatcher.StartTimer(TimeSpan.FromSeconds(1), () =>
        {
            if (sessionStartTime.HasValue && sessionActive)
            {
                var elapsed = DateTime.Now - sessionStartTime.Value;
                ButtonText = $"Bezig: {elapsed:hh\\:mm\\:ss}" +
                $"\n tik om te stoppen en tijd op te slaan";
                return true;
            }

            return false; 
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
