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
			var accessToken = await _apiService.LoginAsync(Username, Password);

			Settings.AccessToken = accessToken;

			//await App.Current.MainPage.DisplayAlert("Welcome", "", "Ok");


			await Application.Current.MainPage.Navigation.PushModalAsync(new Home());

			
			

			
		}
	}
	
}