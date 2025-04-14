using TaskManager.Data.Repositories;
using TaskManager.MVVM.Models;

namespace TaskManager
{
    public partial class App : Application
    {
        public static BaseRepository<User>? UserRepo { get; private set; }
        public static BaseRepository<Stage>? StageRepo { get; private set; }
        public static BaseRepository<ProjectTask>? ProjectTaskRepo { get; private set; }
        public static BaseRepository<Project>? ProjectRepo { get; private set; }
        public static BaseRepository<StudySession>? StudySessionRepo { get; private set; }
        public static BaseRepository<TaskType>? TaskTypeRepo { get; private set; }
        public static BaseRepository<SuccessorPredecessorTaskIds>? SuccessorPredecessorTaskIdsRepo { get; private set; }
        public static BaseRepository<Schedule>? ScheduleRepo { get; private set; }


        public App()
        {

            InitializeComponent();
            UserRepo = new BaseRepository<User>();
            ProjectTaskRepo = new BaseRepository<ProjectTask>();
            StageRepo = new BaseRepository<Stage>();
            ProjectRepo = new BaseRepository<Project>();
            StudySessionRepo = new BaseRepository<StudySession>();
            TaskTypeRepo = new BaseRepository<TaskType>();
            SuccessorPredecessorTaskIdsRepo = new BaseRepository<SuccessorPredecessorTaskIds>();
            ScheduleRepo = new BaseRepository<Schedule>();

        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}