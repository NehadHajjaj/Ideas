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
            //var vm = new LoginViewModel();
            //this.BindingContext = vm;

            InitializeComponent();
        }


        public async void Login_OnClicked(object sender, EventArgs e)
        {
             var vm = new LoginViewModel();
             this.BindingContext = vm;
           
          //  vm.LoginCommand.Execute(null);
            Navigation.PushModalAsync(new Home());
        }

    }
}