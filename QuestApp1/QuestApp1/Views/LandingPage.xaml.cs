using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuestApp1.DependencyServices;
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
            Navigation.PushAsync(new SignUpPage());
        }

        private void SignIn_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SignInPage());
        }

        protected override bool OnBackButtonPressed()
        {
            Device.BeginInvokeOnMainThread(new Action(async () => {
                var result = await this.DisplayAlert("Alert!", "Do you really want to exit?", "Yes", "No");

                if (result)
                {
                    if (Device.RuntimePlatform == Device.Android)
                        DependencyService.Get<ICloseApplication>().closeApplication();
                }
            }));
            return true;
        }
    }
}