using Project.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Project
{
    public partial class App : Application
    {
        public App()
        {
           
            InitializeComponent();
            Device.SetFlags(new string[]
{   "RadioButton_Experimental",
    "AppTheme_Experimental",
    "Markup_Experimental",
    "Expander_Experimental"
    });
            MainPage = new MainPage();
           

        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
