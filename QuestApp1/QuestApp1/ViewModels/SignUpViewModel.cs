using QuestApp1.Annotations;
using QuestApp1.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace QuestApp1.ViewModels
{
    public class SignUpViewModel : INotifyPropertyChanged
    {
        //backing fields
        private string _message;


        UserService userService = new UserService();
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

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

        public ICommand SignUpCommand
        {
            get
            {
                return new Command(
                    async ()=>
                    {
                        var isSuccess = await userService.SignUpUser(Email, Password, ConfirmPassword);

                        if (isSuccess)
                        {
                            Message = "User Registered Successfully";
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
