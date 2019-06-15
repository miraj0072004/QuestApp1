using QuestApp1.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using QuestApp1.Annotations;
using QuestApp1.Services;
using Xamarin.Forms;

namespace QuestApp1.ViewModels
{
    public class QuestionViewModel :INotifyPropertyChanged
    {
        private bool _answerSubmitted = false;
        private Question _questionRetrieved;
        private bool _answerChosen = false;


        public ICommand SubmitAnswerCommand {private  set; get; }
        public ICommand ChooseAnswerCommand {private set; get; }
        public ICommand NextQuestionrCommand { private set; get; }

        public bool AnswerChosen
        {
            get => _answerChosen;
            set
            {
                _answerChosen = value;
                OnPropertyChanged();
            }
        }

        public bool AnswerSubmitted
        {
            get => _answerSubmitted;
            set
            {
                _answerSubmitted = value;
                OnPropertyChanged();
            } 
        }

        public Question QuestionRetrieved
        {
            get => _questionRetrieved;
            set
            {
                _questionRetrieved = value;
                OnPropertyChanged();
            } 
        }

        public QuestionViewModel()
        {
            QuestionService questionService = new QuestionService();
            QuestionRetrieved = questionService.GetQuestionById(1);
            //Initialize ICommand Properties
            SubmitAnswerCommand = new Command(
                () =>
                {
                    AnswerSubmitted = true;
                    RefreshCanExecutes();

                },
                () => AnswerChosen && !AnswerSubmitted
                );
            ChooseAnswerCommand = new Command(
                () =>
                {
                    AnswerChosen = true;
                    RefreshCanExecutes();
                }
                );

            NextQuestionrCommand= new Command(
                () => { RefreshCanExecutes(); },
                () => AnswerSubmitted
                );

        }

        void RefreshCanExecutes()
        {
            ((Command)SubmitAnswerCommand).ChangeCanExecute();
            ((Command)ChooseAnswerCommand).ChangeCanExecute();
            ((Command)NextQuestionrCommand).ChangeCanExecute();

        }

        

        
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
