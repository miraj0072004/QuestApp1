using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using QuestApp1.Helpers;
using QuestApp1.Views;
using Xamarin.Forms;

namespace QuestApp1.ViewModels
{
    public class HomeViewModel
    {
        public HomeViewModel()
        {
            
        }
        public ICommand SignOutCommand
        {
            get
            {
                return new Command(async () =>
                    {
                        var answer = await Application.Current.MainPage.DisplayAlert("Logout verification", "Are you sure you want to logout?", "Yes", "No");
                        if (answer)
                        {
                            Settings.AccessToken = String.Empty;
                            Settings.AccessTokenExpiration = DateTime.UtcNow;

                            IReadOnlyList<Page> navStack = Application.Current.MainPage.Navigation.NavigationStack;

                            if (navStack[0] is HomePage)
                            {
                                Application.Current.MainPage.Navigation.InsertPageBefore(new SignInPage(), navStack[0]);
                            }

                            await Application.Current.MainPage.Navigation.PopAsync();
                        }
                    }
                    );
            }
        }
    }
}
