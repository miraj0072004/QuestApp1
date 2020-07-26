using QuestApp1.Annotations;
using QuestApp1.Helpers;
using QuestApp1.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using QuestApp1.Models;
using Xamarin.Forms;

namespace QuestApp1.ViewModels
{
    public class SignUpViewModel : INotifyPropertyChanged
    {
        private Page _page;

        //backing fields
        private string _message;

        private string username;

        public string Username
        {
            get => username;
            set
            {
                username = value;
                UserForRegisterModel.Username = value;
                OnPropertyChanged();
                ((Command)SignUpCommand).ChangeCanExecute();
            }
        }

        private string firstname;

        public string Firstname
        {
            get => firstname;
            set
            {
                firstname = value;
                UserForRegisterModel.FirstName = value;
                OnPropertyChanged();
                ((Command)SignUpCommand).ChangeCanExecute();
            }
        }


        private string lastname;

        public string Lastname
        {
            get => lastname;
            set
            {
                lastname = value;
                UserForRegisterModel.LastName = value;
                OnPropertyChanged();
                ((Command)SignUpCommand).ChangeCanExecute();
            }
        }

        private string password;

        public string Password
        {
            get => password;
            set
            {
                password = value;
                UserForRegisterModel.Password = value;
                OnPropertyChanged();
                ((Command)SignUpCommand).ChangeCanExecute();
            }
        }





        UserService userService = new UserService();
        /*
                private string _firstName;
        */

        public ICommand SignUpCommand { private set; get; }
        public UserForRegisterModel UserForRegisterModel { get; set; }


        public string Message {
            get
            {
                return _message;
            }
            set
            {
                _message = value;
                OnPropertyChanged();
            }
        }

        public SignUpViewModel(Page page)
        {
            _page = page;
            username = "";
            password = "";
            firstname = "";
            lastname = "";
            UserForRegisterModel = new UserForRegisterModel
            {
                Username = "",
                FirstName = "",
                LastName = "",
                Password = ""
            };

            SignUpCommand = new Command(
                async () =>
                {
                    if (!ValidationHelper.IsFormValid(UserForRegisterModel, _page))
                    {
                        return;
                    }

                    var isSuccess = await userService.SignUpUserRevised(UserForRegisterModel);

                    if (isSuccess)
                    {
                        Message = "User Registered Successfully";
                        Settings.Email = UserForRegisterModel.Username;
                        Settings.Password = UserForRegisterModel.Password;
                        await App.Current.MainPage.DisplayAlert("Success", "Successfully Registered", "Ok");
                    }
                    else
                    {
                        Message = "Registration Failed";
                    }

                },
                () =>
                {
                    var canExec = Username != "" && Firstname != "" && Lastname != "" && Password != "";
                    return canExec;
                });
        }

        //public ICommand SignUpCommand
        //{
        //    get
        //    {
        //        return new Command(
        //            async () =>
        //            {
        //                if (!ValidationHelper.IsFormValid(UserForRegisterModel, _page))
        //                {
        //                    return;
        //                }

        //                var isSuccess = await userService.SignUpUserRevised(UserForRegisterModel);

        //                if (isSuccess)
        //                {
        //                    Message = "User Registered Successfully";
        //                    Settings.Email = UserForRegisterModel.Username;
        //                    Settings.Password = UserForRegisterModel.Password;
        //                    await App.Current.MainPage.DisplayAlert("Success", "Successfully Registered", "Ok");
        //                }
        //                else
        //                {
        //                    Message = "Registration Failed";
        //                }

        //            },
        //             () =>
        //             {
        //                 var canExec = Username != "";
        //                 return canExec;
        //             });


        //    }
        //}

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
