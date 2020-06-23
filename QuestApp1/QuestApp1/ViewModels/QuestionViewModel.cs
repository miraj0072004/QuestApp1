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
        private QuestionRevised _questionRetrieved;
        private bool _answerChosen = false;
        private HashSet<int> _questionsUsed=new HashSet<int>();
        private List<int> _allQuestionIds = new List<int>();

        private readonly QuestionService _questionService;
        private int _selectedAnswerIndex = -1;
        private int _selectedTopAnswerIndex = -1;
        private int _correctAnswerIndex = -1;
        private int _totalQuestions=-1;
        private int _attemptedQuestionCount = 0;
        private bool _questionsCompleted = false;
        private int _gameScore;
        private int _correctAnswersCount=0;

        private readonly string _accessToken = Settings.AccessToken;
        private bool _radioEnabled =true;
        private int _correctAnswer=-1;


        public int CorrectAnswersCount
        {
            get => _correctAnswersCount;
            //set => _correctAnswersCount = value;
        }

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

        public ICommand RestartGameCommand { private set; get; }


        public bool AnswerChosen
        {
            get => _answerChosen;
            set
            {
                _answerChosen = value;
                OnPropertyChanged();
            }
        }

        public int CorrectAnswer
        {
            get => _correctAnswer;
            set
            {
                _correctAnswer = value;
                OnPropertyChanged();
            }
        }


        public bool AnswerSubmitted
        {
            get => _answerSubmitted;
            set
            {
                _answerSubmitted = value;
                RadioEnabled = !value;
                OnPropertyChanged();
            } 
        }

        public bool RadioEnabled
        {
            get => _radioEnabled;
            set
            {
                _radioEnabled = value;
                OnPropertyChanged();
            } 
        }

        public QuestionRevised QuestionRetrieved
        {
            get => _questionRetrieved;
            set
            {
                _questionRetrieved = value;
                OnPropertyChanged();
            } 
        }

        public int AttemptedQuestionCount
        {
            get => _attemptedQuestionCount;
            set
            {
                _attemptedQuestionCount = value;
                OnPropertyChanged();
            }
                
        }

        public bool QuestionsCompleted
        {
            get => _questionsCompleted;
            set
            {
                _questionsCompleted = value;
                OnPropertyChanged();
            }
        }

        public int GameScore
        {
            get => _gameScore;
            set
            {
                _gameScore = value;
                OnPropertyChanged();

            }
        }

        public int SelectedTopAnswerIndex
        {
            get => _selectedTopAnswerIndex;
            set
            {
                _selectedTopAnswerIndex = value;
                OnPropertyChanged();
            }
        }

        public QuestionViewModel()
        {
            _gameScore = 0;
            _questionService = new QuestionService();
             GetNextQuestionAsync();
            //Initialize ICommand Properties
            SubmitAnswerCommand = new Command(
                () =>
                {
                    AttemptedQuestionCount++;
                    if (AttemptedQuestionCount == _totalQuestions)
                    {
                        _totalQuestions = 0;
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
                    SelectedTopAnswerIndex = SelectedAnswerIndex - 1;
                    RefreshCanExecutes();
                }
                );

            NextQuestionCommand= new Command(async () =>
                {
                    //QuestionRetrieved = await GetNextQuestionAsync();
                    if (!QuestionsCompleted)
                    {
                        GetNextQuestionAsync();
                        ResetButtons();
                        RefreshCanExecutes();
                    }
                    else
                    {
                        //UserPerformance userPerformance=new UserPerformance();
                        //userPerformance.UserId = Settings.Email;
                        //userPerformance.CorrectAnswerCount = _correctAnswersCount;
                        //userPerformance.TotalQuestions = _attemptedQuestionCount;
                        //await _questionService.SaveUserPerformance(_accessToken,Settings.Email, userPerformance);

                        await _questionService.SaveUserPerformanceRevised(_attemptedQuestionCount,
                            _correctAnswersCount);
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

            RestartGameCommand= new Command(this.RestartGame);

        }

        private void CheckAnswer()
        {
            //CorrectAnswerIndex = QuestionRetrieved.Answers.Find((a) => (a.Correctness == true)).QuestionAnswerId;
            CorrectAnswerIndex = QuestionRetrieved.CorrectAnswerIndex;

            if (CorrectAnswerIndex==SelectedAnswerIndex)
            {
                CorrectAnswer = 1;
                GameScore += 10;
                _correctAnswersCount += 1;
            }
            else
            {
                CorrectAnswer = 0;
                GameScore -= 5;
            }
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
            SelectedTopAnswerIndex = -1;
            AnswerChosen = false;
            AnswerSubmitted = false;
            CorrectAnswer = -1;
        }

        private async void GetNextQuestionAsync()
        {
            //var accessToken = Settings.AccessToken;
            if (_totalQuestions==-1)
            {
                
                AllQuestionIds = await _questionService.GetAllQuestionIds(_accessToken);
                _totalQuestions = AllQuestionIds.Count;
            }
            
            //AllQuestionIds= await questionService.GetAllQuestionIds(accessToken);

            //var range = Enumerable.Range(1, totalQuestions).Where(i => !QuestionsUsed.Contains(i));
            var range = AllQuestionIds.Where(i => !QuestionsUsed.Contains(i));

            var rand = new System.Random();
            int index = rand.Next(0, _totalQuestions - QuestionsUsed.Count-1);            
            var questionId = range.ElementAt(index);
            QuestionsUsed.Add(questionId);
            QuestionRetrieved = await _questionService.GetQuestionById(_accessToken, questionId);
            


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

        public void RestartGame()
        {
            _totalQuestions = -1;
            AllQuestionIds.Clear();
            QuestionsUsed.Clear();
            QuestionsCompleted = false;
            GameScore = 0;
            AttemptedQuestionCount = 0;
            _correctAnswersCount = 0;
            ResetButtons();
            RefreshCanExecutes();

        }

        
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
