using System.Windows.Input;
using Project.Services;
using Xamarin.Forms;

using System.ComponentModel;

using Project.Views;

namespace Project.Models
{
    public class RegisterViewModel
    {
        private readonly ApiService _apiService = new ApiService();

        public string ConfirmPassword { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public ICommand RegisterCommand { get; set; }
        public RegisterViewModel (){
            RegisterCommand = new Command<string>((arg) => OpenPage(arg));}
        private async void OpenPage(string value)
        {
            var isRegistered = await _apiService.RegisterUserAsync(Username, Email, Password);
            //Settings.Email = Email;
            // Settings.Username = Username;
            // Settings.Password = Password;

            if (isRegistered)
            {
                await Application.Current.MainPage.Navigation.PushModalAsync(new Home());

            }
            else
            {
               
                await Application.Current.MainPage.Navigation.PushModalAsync(new SignUp());

            }
        }
        }




    //public event PropertyChangedEventHandler PropertyChanged;
    }
