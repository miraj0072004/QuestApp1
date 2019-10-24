using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            return true;
        }

        private async void ToHome_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PopToRootAsync();
        }

        private async void PlayAgain_OnClicked(object sender, EventArgs e)
        {
             await Navigation.PopAsync();
            
        }
    }
}