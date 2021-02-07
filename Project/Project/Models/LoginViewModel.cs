using System.Windows.Input;
using Project.Services;
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
		}

		public string Username { get; set; }
		public string Password { get; set; }

		public ICommand LoginCommand
		{
			get
			{
				return new Command(async () =>
				{
					var accessToken = await _apiService.LoginAsync(Username, Password);

					Settings.AccessToken = accessToken;
				});
			}
		}
	}
}