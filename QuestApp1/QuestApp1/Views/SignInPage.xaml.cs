﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuestApp1.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QuestApp1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignInPage : ContentPage
    {
        public SignInPage()
        {
            InitializeComponent();
            BindingContext = new SignInViewModel(this);
        }

        private async void AccessToken_TextChanged(object sender, TextChangedEventArgs e)
        {
            //if ((sender as Entry).Text.Length != 0 )
            //{

            //    //Navigation.PushAsync(new HomePage());

            //    //Navigation.InsertPageBefore(new HomePage(), this);
            //    //await Navigation.PopAsync();

            //    Application.Current.MainPage=new NavigationPage(new HomePage());

            //}

            
        }
    }
}