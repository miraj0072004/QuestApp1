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
using QuestApp1.Helpers;
using System.Threading.Tasks;
using System.Linq;

namespace QuestApp1.ViewModels
{
    public class QuestionViewModel :INotifyPropertyChanged
    {
        private bool _answerSubmitted = false;
        private Question _questionRetrieved;
        private bool _answerChosen = false;
        private HashSet<int> _questionsUsed=new HashSet<int>();
        private List<int> _allQuestionIds = new List<int>();

        private QuestionService questionService;
        private int _selectedAnswerIndex = -1;
        private int _correctAnswerIndex = -1;
        private int totalQuestions=-1;
        private int attmptedQuestionCount = 0;
        private bool questionsCompleted = false;

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

        public HashSet<int> QuestionsUsed
        {
            get => _questionsUsed;
            set => _questionsUsed = value;
        }

        public List<int> AllQuestionIds { get => _allQuestionIds; set => _allQuestionIds = value; }

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

        public int AttmptedQuestionCount
        {
            get => attmptedQuestionCount;
            set
            {
                attmptedQuestionCount = value;
                OnPropertyChanged();
            }
                
        }

        public bool QuestionsCompleted
        {
            get => questionsCompleted;
            set
            {
                questionsCompleted = value;
                OnPropertyChanged();
            }
        }

        public QuestionViewModel()
        {
            questionService = new QuestionService();
            GetNextQuestionAsync();
            //Initialize ICommand Properties
            SubmitAnswerCommand = new Command(
                () =>
                {
                    AttmptedQuestionCount++;
                    if (AttmptedQuestionCount == totalQuestions)
                    {
                        totalQuestions = 0;
                        QuestionsCompleted = true;
                    }
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
                    //QuestionRetrieved = await GetNextQuestionAsync();
                    if (!QuestionsCompleted)
                    {
                        GetNextQuestionAsync();
                        ResetButtons();
                        RefreshCanExecutes();
                    }
                    

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

        private async void GetNextQuestionAsync()
        {
            var accessToken = Settings.AccessToken;
            if (totalQuestions==-1)
            {
                
                AllQuestionIds = await questionService.GetAllQuestionIds(accessToken);
                totalQuestions = AllQuestionIds.Count;
            }
            
            //AllQuestionIds= await questionService.GetAllQuestionIds(accessToken);

            //var range = Enumerable.Range(1, totalQuestions).Where(i => !QuestionsUsed.Contains(i));
            var range = AllQuestionIds.Where(i => !QuestionsUsed.Contains(i));

            var rand = new System.Random();
            int index = rand.Next(0, totalQuestions - QuestionsUsed.Count-1);            
            var questionId = range.ElementAt(index);
            QuestionsUsed.Add(questionId);
            QuestionRetrieved = await questionService.GetQuestionById(accessToken, questionId);
            


            //if (QuestionsUsed.Count==1)
            //{
            //    QuestionsUsed.Add(2);
            //    QuestionRetrieved = await questionService.GetQuestionById(accessToken, 2);

            //}
            //else
            //{
            //    QuestionsUsed.Add(1);
            //    QuestionRetrieved = await questionService.GetQuestionById(accessToken, 1);
            //}



            //return await questionService.GetQuestionById(accessToken,1);

        }

        
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
