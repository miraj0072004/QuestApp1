using QuestApp1.Annotations;
using QuestApp1.Helpers;
using QuestApp1.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using QuestApp1.Views;
using Xamarin.Forms;

namespace QuestApp1.ViewModels
{
    public class SignInViewModel: INotifyPropertyChanged
    {
        UserService userService = new UserService();

        private bool _isBusy;

        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                ShowElements = !_isBusy;
            }
        }

        public bool ShowElements { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        private string _accessToken;

        public string AccessToken
        {
            get => _accessToken;
            set
            {
                _accessToken = value;
                OnPropertyChanged();
            }
        }

        public ICommand SignInCommand
        {
            get
            {
                return new Command
                    (
                       async ()=>
                        {
                            //AccessToken = await userService.SignInUser(Username, Password);

                            AccessToken = await userService.Login(Username, Password);
                            Settings.AccessToken = AccessToken;
                            Settings.Email = Username;
                            await Application.Current.MainPage.Navigation.PushAsync(new HomePage());



                        }
                    );
            }
        }

        public SignInViewModel()
        {
            //Username = Settings.Email.Length==0?"miraj0072004@gmail.com": Settings.Email;
            //Password = Settings.Password.Length==0?"Shahrukh0072004$":Settings.Password;

            Username = Settings.Email.Length == 0 ? "miraj0072004" : Settings.Email;
            Password = Settings.Password.Length == 0 ? "shahrukh0072004" : Settings.Password;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
