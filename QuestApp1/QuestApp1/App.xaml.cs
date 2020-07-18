using System;
using System.Threading.Tasks;
using QuestApp1.Helpers;
using QuestApp1.Models;
using QuestApp1.Services;
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
        public static string accessUrl = "http://192.168.43.182:45455/api/";
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

            

           // MainPage = new NavigationPage(new LandingPage());

            SetMainPage();
        }

        private void SetMainPage()
        {
            //For testing purposes
            //Settings.Email = null;
            //Settings.Password = null;
            if (!string.IsNullOrEmpty(Settings.AccessToken))
            {
                //var isTokenValid = Task.Run(() => UserService.CheckTokenIsValid()).Result;
                //if (isTokenValid)
                //{
                //    MainPage = new NavigationPage(new HomePage());
                //}
                //else
                //{
                //    Settings.AccessToken = String.Empty;
                //    MainPage = new NavigationPage(new SignInPage());
                //}

                if (DateTime.UtcNow.AddHours(1) > Settings.AccessTokenExpiration)
                {
                    //var vm = new SignInViewModel();
                    UserForSignInModel userForSignInModel = new UserForSignInModel{Password = Settings.Password, Username = Settings.Email};
                    UserService userService = new UserService();
                    var loginSuccessful = Task.Run(()=>userService.Login(userForSignInModel)).Result;

                    if (loginSuccessful)
                    {

                        MainPage = new NavigationPage(new HomePage());
                    }
                    else
                    {

                        MainPage = new NavigationPage(new SignInPage());
                    }


                }
                else
                {
                    MainPage = new NavigationPage(new HomePage());
                }
                
            }
            else if (!string.IsNullOrEmpty(Settings.Email) && !string.IsNullOrEmpty(Settings.Password))
            {
                MainPage = new NavigationPage(new SignInPage());
            }
            else
            {
                MainPage = new NavigationPage(new LandingPage());
            }
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
