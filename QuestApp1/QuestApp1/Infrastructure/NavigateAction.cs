using QuestApp1.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace QuestApp1.Infrastructure
{
    public class NavigateAction : TriggerAction<VisualElement>
    {
        protected override void Invoke(VisualElement sender)
        {
            Application.Current.MainPage.Navigation.PushModalAsync(new HomePage());
        }
    }
}
