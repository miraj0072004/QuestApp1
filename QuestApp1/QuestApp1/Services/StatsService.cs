using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using QuestApp1.Models;

namespace QuestApp1.Services
{
    class StatsService
    {
        private string userPerformanceUrl = "http://192.168.1.3:45455/api/UserPerformances/";

        public async Task<UserPerformance> GetMyStats(string accessToken)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var json = await client.GetStringAsync(userPerformanceUrl+ "MyPerformance");
            UserPerformance userPerformance = JsonConvert.DeserializeObject<UserPerformance>(json);
            return userPerformance;



        }
    }
}
