using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QuestApp1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LandingPage : ContentPage
    {
        public LandingPage()
        {
            InitializeComponent();
        }

        private void SignUp_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SignUpView());
        }

        private void SignIn_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SignInView());
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}