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
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
        }

        public void Play_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new QuestionPage());
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}