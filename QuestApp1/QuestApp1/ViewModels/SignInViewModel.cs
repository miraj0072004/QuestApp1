using QuestApp1.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace QuestApp1.ViewModels
{
    public class SignInViewModel
    {
        UserService userService = new UserService();
        public string Username { get; set; }
        public string Password { get; set; }

        public ICommand SingInCommand
        {
            get
            {
                return new Command
                    (
                       async ()=>
                        {
                            await userService.SignInUser(Username, Password);
                        }
                    );
            }
        }
    }
}
