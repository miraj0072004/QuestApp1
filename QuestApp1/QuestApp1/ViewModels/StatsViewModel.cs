using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using QuestApp1.Annotations;
using QuestApp1.Helpers;
using QuestApp1.Models;
using QuestApp1.Services;

namespace QuestApp1.ViewModels
{
    public class StatsViewModel: INotifyPropertyChanged
    {
        private UserPerformance _myPerformance;
        private bool _isBusy;

        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                ShowElements = !_isBusy;
                OnPropertyChanged();
            }
        }


        private bool _showElements;

        public bool ShowElements
        {
            get { return _showElements; }
            set
            {
                _showElements = value;
                OnPropertyChanged();
            }
        }


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
            //MyPerformance = new UserPerformance();
            IsBusy = true; 
            GetMyPerformance();
            
            
        }

        private async void GetMyPerformance()
        {
            
            MyPerformance = await _statsService.GetMyStats(Settings.AccessToken);
            var tempCorrectness= (float)MyPerformance.CorrectAnswerCount / MyPerformance.TotalQuestions;
            Correctness = (float) Math.Round((Decimal) tempCorrectness, 2, MidpointRounding.AwayFromZero);

            IsBusy = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
