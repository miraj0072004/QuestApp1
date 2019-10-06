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
    }
}