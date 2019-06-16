using QuestApp1.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using QuestApp1.Annotations;
using QuestApp1.Services;
using QuestApp1.Views;
using Xamarin.Forms;

namespace QuestApp1.ViewModels
{
    public class QuestionViewModel :INotifyPropertyChanged
    {
        private bool _answerSubmitted = false;
        private Question _questionRetrieved;
        private bool _answerChosen = false;
        private List<int> _questionsUsed=new List<int>();

        private QuestionService questionService;
        private int _selectedAnswerIndex = -1;
        private int _correctAnswerIndex = -1;


        public int CorrectAnswerIndex
        {
            get => _correctAnswerIndex;
            set
            {
                _correctAnswerIndex = value;
                OnPropertyChanged();
            } 
        }

        public int SelectedAnswerIndex
        {
            
            get => _selectedAnswerIndex;
            set
            {
                _selectedAnswerIndex = value;
                OnPropertyChanged();
            } 
        }

        public List<int> QuestionsUsed
        {
            get => _questionsUsed;
            set => _questionsUsed = value;
        }

        public ICommand SubmitAnswerCommand {private  set; get; }
        public ICommand ChooseAnswerCommand {private set; get; }
        public ICommand NextQuestionCommand { private set; get; }
        public ICommand ExplanationCommand { private set; get; }


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
            questionService = new QuestionService();
            QuestionRetrieved = GetNextQuestion();
            //Initialize ICommand Properties
            SubmitAnswerCommand = new Command(
                () =>
                {
                    AnswerSubmitted = true;
                    CheckAnswer();
                    RefreshCanExecutes();

                },
                () => AnswerChosen && !AnswerSubmitted
                );
            ChooseAnswerCommand = new Command<string>(
                (chosenAnswerIndex) =>
                {
                    AnswerChosen = true;
                    SelectedAnswerIndex = int.Parse(chosenAnswerIndex);
                    RefreshCanExecutes();
                }
                );

            NextQuestionCommand= new Command(
                () =>
                {
                    QuestionRetrieved = GetNextQuestion();
                    ResetButtons();
                    RefreshCanExecutes();
                    
                },
                () => AnswerSubmitted
                );

            ExplanationCommand= new Command(
                () =>
                {
                    var explanationPage = new ExplanationPage
                    {
                        BindingContext = QuestionRetrieved
                    };
                    RefreshCanExecutes();
                    Application.Current.MainPage.Navigation.PushModalAsync(explanationPage);
                
                },
                () => AnswerSubmitted
                
                );

        }

        private void CheckAnswer()
        {
            CorrectAnswerIndex = QuestionRetrieved.Answers.Find((a) => (a.Correctness == true)).Id;
        }

        void RefreshCanExecutes()
        {
            ((Command)SubmitAnswerCommand).ChangeCanExecute();
            ((Command)ChooseAnswerCommand).ChangeCanExecute();
            ((Command)NextQuestionCommand).ChangeCanExecute();
            ((Command)ExplanationCommand).ChangeCanExecute();

        }

        void ResetButtons()
        {
            CorrectAnswerIndex = -1;
            SelectedAnswerIndex = -1;
            AnswerChosen = false;
            AnswerSubmitted = false;
        }

        private Question GetNextQuestion()
        {
            
            if (QuestionsUsed.Count==1)
            {
                QuestionsUsed.Add(2);
                return questionService.GetQuestionById(2);
            }
            QuestionsUsed.Add(1);
            return questionService.GetQuestionById(1);

        }

        
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
