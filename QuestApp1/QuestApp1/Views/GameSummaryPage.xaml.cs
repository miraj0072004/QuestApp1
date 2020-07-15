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
    public partial class GameSummaryPage : ContentPage
    {
        

        public GameSummaryPage()
        {
            InitializeComponent();
            
        }

        public GameSummaryPage(QuestionViewModel viewModel)
        {
            //throw new NotImplementedException();
            InitializeComponent();
            this.BindingContext = viewModel;
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

        private async void ToHome_OnClicked(object sender, EventArgs e)
        {
            //await Navigation.PopToRootAsync();
            //await Navigation.PushAsync(new HomePage());

            var existingPages = Navigation.NavigationStack.ToList();

            //existingPages.Reverse();
            //foreach (var page in existingPages)
            //{
            //    if (page is HomePage)
            //        return;

            //    await Navigation.PopAsync();
            //}

            var questionsPage = existingPages[existingPages.Count-2];
            Navigation.RemovePage(questionsPage);
            await Navigation.PopAsync();
        }

        private async void PlayAgain_OnClicked(object sender, EventArgs e)
        {
             await Navigation.PopAsync();
            
        }
    }
}