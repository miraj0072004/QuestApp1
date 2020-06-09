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
        //public static string accessUrl = "http://localhost:5000/api/";
        public static string accessUrl = "http://192.168.43.182:45457/api/";
        public App()
        {
            InitializeComponent();

            //MainPage = new MainPage();

            //questionViewModel= new QuestionViewModel();
            //MainPage= new QuestionPage(questionViewModel);

            //MainPage = new QuestionPage();

            //attempt to attach an application wide view model programmatically
            //this.Resources= new ResourceDictionary();
            //var mainViewModel = new QuestionViewModel();
            //Resources.Add("MainViewModel",mainViewModel);

            MainPage = new NavigationPage(new LandingPage());
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
