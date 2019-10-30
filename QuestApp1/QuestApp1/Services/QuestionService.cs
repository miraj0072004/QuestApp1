using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using QuestApp1.Models;
using Xamarin.Forms;

namespace QuestApp1.Services
{
    class QuestionService
    {
        //private string questionUrl= "http://10.5.42.37:45455/api/Questions/";
        private string questionUrl = "http://192.168.1.3:45455/api/Questions/";
        private string userPerformanceUrl = "http://192.168.1.3:45455/api/UserPerformances/";
        private List<Question> Questions = new List<Question> {
                new Question()
                {
                    Id = 1,
                    QuestionText = "What is 2 + 2 ?",
                    Answers = new List<Answer>()
                    {
                        new Answer()
                        {
                            Id = 1,
                            AnswerText = "3",
                            Correctness = false
                        },
                        new Answer()
                        {
                            Id = 2,
                            AnswerText = "4",
                            Correctness = true
                        },
                        new Answer()
                        {
                            Id = 3,
                            AnswerText = "5",
                            Correctness = false
                        }
                    },
                    AnswerDescription = "This is definitional you idiot!"

                },
                new Question()
                {
                    Id = 2,
                    QuestionText = "Who is the father of modern philosophy ?",
                    Answers = new List<Answer>()
                    {
                        new Answer()
                        {
                            Id = 1,
                            AnswerText = "René Descartes",
                            Correctness = true
                        },
                        new Answer()
                        {
                            Id = 2,
                            AnswerText = "Aristotle",
                            Correctness = false
                        },
                        new Answer()
                        {
                            Id = 3,
                            AnswerText = "David Hume",
                            Correctness = false
                        }
                    },
                    AnswerDescription = "René Descartes is generally considered the father of modern philosophy. He was the first major figure in the philosophical movement known as rationalism, a method of understanding the world based on the use of reason as the means to attain knowledge.!"

                }
            };

        public async Task<Question> GetQuestionById(string accessToken,int id)
        //=> new Question()
        //{
        //    Id = 1,
        //    QuestionText = "What is 2 + 2 ?",
        //    Answers = new List<Answer>()
        //    {
        //        new Answer()
        //        {
        //            Id = 1,
        //            AnswerText = "3",
        //            Correctness = false
        //        },
        //        new Answer()
        //        {
        //            Id = 1,
        //            AnswerText = "4",
        //            Correctness = true
        //        },
        //        new Answer()
        //        {
        //            Id = 1,
        //            AnswerText = "5",
        //            Correctness = false
        //        }
        //    },
        //    AnswerDescription = "This is definitional you idiot!"

        //};
        //=> this.Questions.Find(q => q.Id == id);
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",accessToken);
            var json = await client.GetStringAsync(questionUrl + id);
            Question question = JsonConvert.DeserializeObject<Question>(json.Substring(1,json.Length-2));
            //var question = JsonConvert.DeserializeObject(json);
            return question;
        }


        //public QuestionService()
        //{
        //    HttpClient httpClient = new HttpClient();

        //}

        public async Task<int> GetTotalQuestionCount(string accessToken)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var questionCount = await client.GetStringAsync(questionUrl+"Count");
            
            return Int32.Parse(questionCount.ToString());

        }

        public async Task<List<int>> GetAllQuestionIds(string accessToken)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var allQuestions = await client.GetStringAsync(questionUrl + "All");

            return GetListFromString(allQuestions);

        }


        private List<int> GetListFromString(string allIds)
        {
            string stripped = allIds.Substring(1, allIds.Length - 2);
            List<int> allIdsList = new List<int>(Array.ConvertAll(stripped.Split(','),Int32.Parse));
            return allIdsList;
        }

        public async Task<bool> SaveUserPerformance(string accessToken,string userName,UserPerformance userPerformance)
        {
            var client = new HttpClient();
            var json = JsonConvert.SerializeObject(userPerformance);
            HttpContent httpContent=new StringContent(json);
            httpContent.Headers.ContentType=new MediaTypeHeaderValue("application/json");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var result = await client.PutAsync(userPerformanceUrl+ "PutMyPerformance", httpContent);
            return result.IsSuccessStatusCode;

        }

    }
}
