using System;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace Project
{
	/// <summary>
	/// This is the Settings static class that can be used in your Core solution or in any
	/// of your client applications. All settings are laid out the same exact way with getters
	/// and setters. 
	/// </summary>
	public static class Settings
	{
		private static ISettings AppSettings => CrossSettings.Current;

		public static string Username
		{
			get => AppSettings.GetValueOrDefault("Username", "");
			set => AppSettings.AddOrUpdateValue("Username", value);
		}
		public static string Email
		{
			get => AppSettings.GetValueOrDefault("Email", "");
			set => AppSettings.AddOrUpdateValue("Email", value);
		}
		public static string Password
		{
			get => AppSettings.GetValueOrDefault("Password", "");
			set => AppSettings.AddOrUpdateValue("Password", value);
		}
		public static string AccessToken
		{
			get => AppSettings.GetValueOrDefault("AccessToken", "");
			set => AppSettings.AddOrUpdateValue("AccessToken", value);
		}

		public static DateTime AccessTokenExpirationDate
		{
			get => AppSettings.GetValueOrDefault("AccessTokenExpirationDate", DateTime.UtcNow);
			set => AppSettings.AddOrUpdateValue("AccessTokenExpirationDate", value);
		}
	}
}
