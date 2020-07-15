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
    public partial class QuestionPage : ContentPage
    {
        public QuestionPage()
        {
            InitializeComponent();
            
        }

        private void Finish_Button_Clicked(object sender, EventArgs e)
        {

            if((sender as Button).Text=="Finish")
            {
                var viewModel = BindingContext as QuestionViewModel;
                
                Navigation.PushAsync(new GameSummaryPage(viewModel));

                //Navigation.PushAsync(
                //     new GameSummaryPage
                //     {
                //         BindingContext = BindingContext as QuestionViewModel
                //     }
                //    );
            }
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