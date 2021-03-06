﻿using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using QuestApp1.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using QuestApp1.Helpers;


namespace QuestApp1.Services
{
    class UserService
    {
        //private string accessUrl = "http://10.5.42.37:45455/";
        //private string accessUrl = "http://192.168.1.2:45455/";
        //private string accessUrl = "http://192.168.43.1:5000/api/";
        //public async Task<bool> SignUpUser(string email, string password, string confirmPassword)
        //{
        //    var client = new HttpClient();

        //    var registerBindingModel = new RegisterBindingModel()
        //    {
        //        Email = email,
        //        Password = password,
        //        ConfirmPassword = confirmPassword
        //    };

        //    var json = JsonConvert.SerializeObject(registerBindingModel);
        //    HttpContent content = new StringContent(json);
        //    content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

        //    var response= await client.PostAsync(App.accessUrl+"api/Account/Register", content);
        //    return response.IsSuccessStatusCode;
            

        //}

        //public async Task<string> SignInUser (string username, string password)
        //{
            
        //    var keyValues = new List<KeyValuePair<string, string>>
        //    {
        //        new KeyValuePair<string, string>("username",username),
        //        new KeyValuePair<string, string>("password",password),
        //        new KeyValuePair<string, string>("grant_type","password")
        //    };

        //    //var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost:7171/Token");
        //    var request = new HttpRequestMessage(HttpMethod.Post, App.accessUrl + "Token");
        //    request.Content = new FormUrlEncodedContent(keyValues);

        //    var client = new HttpClient();
        //    var response = await client.SendAsync(request);

        //    var jwt = await response.Content.ReadAsStringAsync();
        //    JObject jwtDynamic = JsonConvert.DeserializeObject<dynamic>(jwt);

        //    var accessToken = jwtDynamic.Value<string>("access_token");

        //    Debug.WriteLine(await response.Content.ReadAsStringAsync());

        //    return accessToken;
        //}

        public async Task<bool> Login(UserForSignInModel userForSignInModel)
        {
           

            
            //var request = new HttpRequestMessage(HttpMethod.Post, accessUrl + "auth/login");
            //request.Content = new FormUrlEncodedContent(keyValues);

            var client = new HttpClient();
            //var response = await client.SendAsync(request);

            var loginUser = new User();
            loginUser.Username = userForSignInModel.Username;
            loginUser.Password = userForSignInModel.Password;

            var json = JsonConvert.SerializeObject(loginUser);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(new Uri(App.accessUrl + "auth/login"), stringContent);

            var jwt = await response.Content.ReadAsStringAsync();
            JObject jwtDynamic = JsonConvert.DeserializeObject<dynamic>(jwt);

            var accessToken = jwtDynamic.Value<string>("token");
            
            var userId = jwtDynamic.Value<string>("user");

            if (accessToken == null)
            {
                return false;
            }
            var accessTokenExpiration = jwtDynamic.Value<DateTime>("expiry");
            Settings.AccessTokenExpiration = accessTokenExpiration;
            Settings.LoggedInUserId = userId;
            Settings.AccessToken = accessToken;
            Settings.Email = userForSignInModel.Username;
            Settings.Password = userForSignInModel.Password;


            //Debug.WriteLine(await response.Content.ReadAsStringAsync());

            return true;
        }

        public async Task<bool> SignUpUserRevised(UserForRegisterModel userForRegisterModel)
        {
            var client = new HttpClient();


            var json = JsonConvert.SerializeObject(userForRegisterModel);
            HttpContent content = new StringContent(json);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(App.accessUrl + "auth/register", content);
            return response.IsSuccessStatusCode;


        }

        public static async Task<bool> CheckTokenIsValid()
        {
            var client = new HttpClient();
            HttpContent content = new StringContent("application/json");
            var response = await client.PostAsync(App.accessUrl + "auth/validatetoken", content);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }

            return false;
        }
    }
}
