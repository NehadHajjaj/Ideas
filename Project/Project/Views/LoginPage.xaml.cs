using Project.Models;
using Project.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Project.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {

        public LoginPage()
        {
            

            InitializeComponent();
            this.BindingContext = new LoginViewModel();
        }


      
        public async void Guest(object sender, EventArgs e)
        {

            Navigation.PushModalAsync(new Home());
        }
        public async void Signup(object sender, EventArgs e)
        {

            Navigation.PushModalAsync(new SignUp());
        }

    }
}