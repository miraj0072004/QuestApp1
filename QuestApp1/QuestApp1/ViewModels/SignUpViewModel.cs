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
using Xamarin.Forms;

namespace QuestApp1.ViewModels
{
    public class SignUpViewModel : INotifyPropertyChanged
    {
        private Page _page;

        //backing fields
        private string _message;


        UserService userService = new UserService();
        private string _firstName;

        public UserForRegisterModel UserForRegisterModel { get; set; } = new UserForRegisterModel();

        
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
        }

        public ICommand SignUpCommand
        {
            get
            {
                return new Command(
                    async ()=>
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
                            await App.Current.MainPage.DisplayAlert("Success","Successfully Registered","Ok");
                        }
                        else
                        {
                            Message = "Registration Failed";
                        }

                    }
                    );
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
