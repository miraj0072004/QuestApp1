using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuestApp1.DependencyServices;
using QuestApp1.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QuestApp1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
            //BindingContext = new HomeViewModel();
        }

        public void Play_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new QuestionPage());
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

        private void Stats_OnClicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new StatsPage());
        }
    }
}