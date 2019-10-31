using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using QuestApp1.Annotations;
using QuestApp1.Helpers;
using QuestApp1.Models;
using QuestApp1.Services;

namespace QuestApp1.ViewModels
{
    public class StatsViewModel: INotifyPropertyChanged
    {
        private UserPerformance _myPerformance;

        public float Correctness
        {
            get => _correctness;
            set
            {
                _correctness = value;
                OnPropertyChanged();
            } 
        }

        private readonly StatsService _statsService;
        private float _correctness;

        public UserPerformance MyPerformance
        {
            get => _myPerformance;
            private set
            {
                _myPerformance = value;
                OnPropertyChanged();
            } 
        }

        public StatsViewModel()
        {
            _statsService = new StatsService();
            GetMyPerformance();
            
        }

        private async void GetMyPerformance()
        {
            
            MyPerformance =await _statsService.GetMyStats(Settings.AccessToken);
             var tempCorrectness= (float)MyPerformance.CorrectAnswerCount / MyPerformance.TotalQuestions;
             Correctness = (float) Math.Round((Decimal) tempCorrectness, 2, MidpointRounding.AwayFromZero);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
