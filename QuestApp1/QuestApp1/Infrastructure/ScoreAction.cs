using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace QuestApp1.Infrastructure
{
    class ScoreAction : TriggerAction<Label>
    {
        public Color TextColor { get; set; }
        public double Scale { get; set; }

        public ScoreAction()
        {
            TextColor=Color.LimeGreen;
            Scale = 1;
        }
        protected  override async void Invoke(Label sender)
        {
            
           
            sender.TextColor = TextColor;
            //sender.Opacity = 0;
            //sender.TranslationX = 0;
            //sender.TranslationY = -200;
            //sender.IsVisible = true;
            //await Task.WhenAny<bool>(
                 
            //    sender.FadeTo(0, 200),
            //    sender.FadeTo(1,500)
                
            //    );

            await sender.FadeTo(0, 200);

            await Task.WhenAll<bool>(
                sender.ScaleTo(Scale, 300, Easing.SinIn),
                sender.FadeTo(1, 300)
            );
            
            sender.ScaleTo(1, 200, Easing.SinOut);
            //sender.FadeTo(0, 700);
            //sender.IsVisible = false;
        }
    }
}
