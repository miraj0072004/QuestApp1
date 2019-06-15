using System;
using QuestApp1.Models;
using QuestApp1.ViewModels;
using QuestApp1.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QuestApp1
{
    public partial class App : Application
    {
        //private QuestionViewModel questionViewModel;
        public App()
        {
            InitializeComponent();

            //MainPage = new MainPage();

            //questionViewModel= new QuestionViewModel();
            //MainPage= new QuestionPage(questionViewModel);
            MainPage = new QuestionPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
