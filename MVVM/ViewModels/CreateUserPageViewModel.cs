using System.ComponentModel;
using System.Runtime.CompilerServices;
using TaskManager.MVVM.Models;
using TaskManager.Security;
using TaskManager.MVVM.Views;

namespace TaskManager.MVVM.ViewModels;

public class CreateUserPageViewModel : INotifyPropertyChanged
{
    private string _password = "";
    private string _userName = "";
    private string _passwordRepeat = "";
    public string UserName
    {
        get => _userName;
        set { _userName = value; OnPropertyChanged(); }
    }
    public string Password
    {
        get => _password;
        set { _password = value; OnPropertyChanged(); }
    }
    public string PasswordRepeat
    {
        get => _passwordRepeat;
        set { _passwordRepeat = value; OnPropertyChanged(); }
    }



    public Command CreateUserCommand { get; }

    public CreateUserPageViewModel()
    {
         CreateUserCommand = new Command(OnCreateUser);
    }

    private void OnCreateUser()
    {
        User user1 = App.UserRepo.GetEntityByName(_userName);
        if (App.UserRepo.GetEntityByName(_userName) == null && (Password != "" && PasswordRepeat != ""))
        {
            string salt = PasswordHasher.GenerateSalt();

            string hash1 = PasswordHasher.HashPassword(Password, salt);
            string hash2 = PasswordHasher.HashPassword(PasswordRepeat, salt);

            if(hash1 == hash2)
            {
                User user = new User(UserName, hash1, salt);
                App.UserRepo.SaveEntity(user);
                Application.Current.MainPage.DisplayAlert("Succes", "Succesfully created user", "OK");
                Application.Current.MainPage.Navigation.PushModalAsync(new LoginPage());
            }
            else
            {
                Password = "";
                PasswordRepeat = "";
                Application.Current.MainPage.DisplayAlert("Invalid password", "Passwords do not match ", "OK");
            }

        }
        else
        {
            if(Password == "" | PasswordRepeat == "")
            {
                Application.Current.MainPage.DisplayAlert("No password", "Enter a password or confirm the password", "OK");
            }
            else
            {
                Application.Current.MainPage.DisplayAlert("Invalid username", "Name already in use", "OK");
            }
                
        }




    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string? name = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}