using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using QuestApp1.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;


namespace QuestApp1.Services
{
    class UserService
    {
        //private string accessUrl = "http://10.5.42.37:45455/";
        private string accessUrl = "http://192.168.1.2:45455/";
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

            var response= await client.PostAsync(accessUrl+"api/Account/Register", content);
            return response.IsSuccessStatusCode;
            

        }

        public async Task<string> SignInUser (string username, string password)
        {
            
            var keyValues = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("username",username),
                new KeyValuePair<string, string>("password",password),
                new KeyValuePair<string, string>("grant_type","password")
            };

            //var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost:7171/Token");
            var request = new HttpRequestMessage(HttpMethod.Post, accessUrl+"Token");
            request.Content = new FormUrlEncodedContent(keyValues);

            var client = new HttpClient();
            var response = await client.SendAsync(request);

            var jwt = await response.Content.ReadAsStringAsync();
            JObject jwtDynamic = JsonConvert.DeserializeObject<dynamic>(jwt);

            var accessToken = jwtDynamic.Value<string>("access_token");

            Debug.WriteLine(await response.Content.ReadAsStringAsync());

            return accessToken;

            



        }
    }
}
