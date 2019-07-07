using Newtonsoft.Json;
using QuestApp1.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace QuestApp1.Services
{
    class UserService
    {
        public async Task<bool> SignUpUser(string email, string password, string confirmPassword)
        {
            var client = new HttpClient();

            var registerBindingModel = new RegisterBindingModel()
            {
                Email = email,
                Password = password,
                ConfirmPassword = confirmPassword
            };

            var json = JsonConvert.SerializeObject(registerBindingModel);
            HttpContent content = new StringContent(json);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var response= await client.PostAsync("http://localhost:7171/api/Account/Register", content);
            return response.IsSuccessStatusCode;
            

        }

        public async Task SignInUser (string username, string password)
        {
            
            var keyValues = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("username",username),
                new KeyValuePair<string, string>("password",password),
                new KeyValuePair<string, string>("grant_type","password")
            };

            var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost:7171/Token");
            request.Content = new FormUrlEncodedContent(keyValues);

            var client = new HttpClient();
            var response = await client.SendAsync(request);

            Debug.WriteLine(await response.Content.ReadAsStringAsync());

            



        }
    }
}
