using System.Windows.Input;
using Project.Services;
using Project.Views;
using Xamarin.Forms;

namespace Project.Models
{
	public class LoginViewModel
	{
		private readonly ApiService _apiService = new ApiService();

		public LoginViewModel()
		{
			Username = Settings.Username;
			Password = Settings.Password;
			LoginCommand = new Command<string>((arg) => OpenPage(arg));
		}

		public string Username { get; set; }
		public string Password { get; set; }
		public ICommand LoginCommand { get; set; }
	
		private async void OpenPage(string value)
		{

			if (string.IsNullOrWhiteSpace(Username) ||  string.IsNullOrWhiteSpace(Password) )
			{
				await App.Current.MainPage.DisplayAlert("Error", "Please Fill the Fields", "Ok");

			} else {
				var accessToken = await _apiService.LoginAsync(Username, Password);

				Settings.AccessToken = accessToken;




				await Application.Current.MainPage.Navigation.PushModalAsync(new Home());
			}






			}
		}
	
}