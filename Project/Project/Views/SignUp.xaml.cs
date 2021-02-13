using Project.Models;
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
    public partial class SignUp : ContentPage
    {
       
        public SignUp()
        {
            Device.SetFlags(new string[] { "RadioButton_Experimental" });
            InitializeComponent();
            this.BindingContext = new RegisterViewModel();
        }
        public void UseDarkTheme_Tapped(object sender, EventArgs args)
        {
           // RadioButton rb = sender as RadioButton;
           // choiceTextBlock.Text = "You chose: " + rb.GroupName;
        }
     // public async void Register_OnClicked(object sender, EventArgs e)
     //  {

          //  Navigation.PushModalAsync(new LoginPage());
      //  }
    }
}