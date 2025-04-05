using System.ComponentModel;
using System.Runtime.CompilerServices;
using TaskManager.MVVM.Views;
using TaskManager.MVVM.Models;
using TaskManager.Security;

namespace TaskManager.MVVM.ViewModels;

public class LoginViewModel : INotifyPropertyChanged
{
    private string _userName;
    public string UserName
    {
        get => _userName;
        set { _userName = value; OnPropertyChanged(); }
    }

    private string _password;
    public string Password
    {
        get => _password;
        set { _password = value; OnPropertyChanged(); }
    }


    public Command LogInCommand { get; }
    public Command CreateUserCommand { get; }

    public LoginViewModel()
    {
        LogInCommand = new Command(OnLogIn);
        CreateUserCommand = new Command(OnCreateUser);
    }

    private void OnLogIn()
    {
        User user = App.UserRepo.GetEntityByName(UserName);
        List<User> users = App.UserRepo.GetEntities();
        if (user != null && (Password != null && user.Password == PasswordHasher.HashPassword(Password, user.Salt)))
        {
            Application.Current.MainPage.Navigation.PushModalAsync(new UserHome(user));
        }
        else
        {
            Password = "";
            Application.Current.MainPage.DisplayAlert("Ongeldige gegevens", "Gebruiker bestaat niet of gebruikersnaam en wachtwoord komen niet overeen", "OK");
        }
    }
    private void OnCreateUser()
    {
        Application.Current.MainPage.Navigation.PushModalAsync(new CreateUserPage());
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string? name = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}