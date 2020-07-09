using QuestApp1.Annotations;
using QuestApp1.Helpers;
using QuestApp1.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using QuestApp1.Models;
using QuestApp1.Views;
using Xamarin.Forms;

namespace QuestApp1.ViewModels
{
    public class SignInViewModel: INotifyPropertyChanged
    {
        private readonly Page _page;
        UserService userService = new UserService();

        private bool _isBusy;

        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                OnPropertyChanged();
                ShowElements = !_isBusy;
            }
        }

        public bool ShowElements { get; set; }

        public string Username
        {
            get => _username;
            set
            {
                if (value == _username) return;
                _username = value;
                OnPropertyChanged();
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                if (value == _password) return;
                _password = value;
                OnPropertyChanged();
            }
        }


        public UserForSignInModel UserForSignInModel { get; set; } = new UserForSignInModel();

        private string _accessToken;
        private string _username;
        private string _password;

        public string AccessToken
        {
            get => _accessToken;
            set
            {
                _accessToken = value;
                OnPropertyChanged();
            }
        }

        public SignInViewModel(Page page)
        {
            _page = page;

            //Getting the credentials if they are already stored
            UserForSignInModel.Username = Settings.Email;
            UserForSignInModel.Password = Settings.Password;
        }

        public ICommand SignInCommand
        {
            get
            {
                return new Command
                    (
                       async ()=>
                       {
                           IsBusy = true;


                           if (_page != null)
                           {
                               if (!ValidationHelper.IsFormValid(UserForSignInModel, _page))
                               {
                                   IsBusy = false;
                                   return;
                               } 
                           }
                           //AccessToken = await userService.SignInUser(Username, Password);

                            var loginSuccessful = await userService.Login(UserForSignInModel);

                            if (loginSuccessful)
                            {
                               IsBusy = false;
                               await Application.Current.MainPage.Navigation.PushAsync(new HomePage());
                            }
                            else
                            {
                                IsBusy = false;
                                await Application.Current.MainPage.DisplayAlert("Error", "There was an error trying to log you in. Please retry", "Ok");
                            }



                        }
                    );
            }
        }

        public SignInViewModel()
        {
            //Username = Settings.Email.Length==0?"miraj0072004@gmail.com": Settings.Email;
            //Password = Settings.Password.Length==0?"Shahrukh0072004$":Settings.Password;

            //Username = Settings.Email.Length == 0 ? "miraj0072004" : Settings.Email;
            //Password = Settings.Password.Length == 0 ? "shahrukh0072004" : Settings.Password;

            UserForSignInModel.Username = Settings.Email;
            UserForSignInModel.Password = Settings.Password;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
