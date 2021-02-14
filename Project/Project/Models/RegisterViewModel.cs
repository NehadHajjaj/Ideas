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
            if ( string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password) || string.IsNullOrWhiteSpace(ConfirmPassword))
            {
                await App.Current.MainPage.DisplayAlert("Error", "Please Fill the Fields", "Ok");
           
            } 
            else {
                var isRegistered = await _apiService.RegisterUserAsync(Username ,Email, Password, ConfirmPassword);
                //Settings.Email = Email;
                // Settings.Username = Username;
                // Settings.Password = Password;

                if (isRegistered)
                {
                    await Application.Current.MainPage.Navigation.PushModalAsync(new Home());

                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("sorry! try again", "", "Ok");

                    await Application.Current.MainPage.Navigation.PushModalAsync(new SignUp());

                }
            }
        }
               
        }
        }




    //public event PropertyChangedEventHandler PropertyChanged;
    
