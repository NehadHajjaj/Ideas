using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Project.Models;


namespace Project.Services
{
	public class ApiService
	{
		public async Task<bool> RegisterUserAsync(
String username, String email, String password)
		{
		
				//var client = new HttpClient();
				var client = new HttpClient(new System.Net.Http.HttpClientHandler());

			var model = new RegisterBindingModel
			{
				Email = email,
		
				Password = password,
				Username = username

			};

			var json = JsonConvert.SerializeObject(model);

			HttpContent httpContent = new StringContent(json);

			httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

			var response = await client.PostAsync(
				Constants.BaseApiAddress + "api/Account/Register", httpContent);
			Debug.WriteLine(response.IsSuccessStatusCode);
		
			if (response.IsSuccessStatusCode)
			{
				 return true;
		
			}

			return false;
		}



		public async Task<string> LoginAsync(string userName, string password)
		{
			var keyValues = new List<KeyValuePair<string, string>>
			{
				new KeyValuePair<string, string>("username", userName),
				new KeyValuePair<string, string>("password", password),
				new KeyValuePair<string, string>("grant_type", "password")
			};

			var request = new HttpRequestMessage(
				HttpMethod.Post, Constants.BaseApiAddress + "Token");

			request.Content = new FormUrlEncodedContent(keyValues);

			//var client = new HttpClient();
			var client = new HttpClient(new System.Net.Http.HttpClientHandler());
			var response = await client.SendAsync(request);

			var content = await response.Content.ReadAsStringAsync();
			//var chRequest = JsonConvert.DeserializeObject(content);
			dynamic jwtDynamic = JObject.Parse(content);
			var accessTokenExpiration = jwtDynamic.Value<DateTime>(".expires");
			var accessToken = jwtDynamic.Value<string>("access_token");

			Settings.AccessTokenExpirationDate = accessTokenExpiration;

			//Debug.WriteLine(accessTokenExpiration);

			//Debug.WriteLine(content);

			return accessToken;
		}

       
    }
}
