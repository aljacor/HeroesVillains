using HeroesVillains.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace HeroesVillains.Services
{
    public class HeroVillainService<T> : IApi<T>
    {
        private readonly HttpClient httpClient;
        public HeroVillainService() {
            httpClient = new HttpClient
            {
                Timeout = TimeSpan.FromSeconds(120)
            };
        }

        public async Task<T> CallApi(string url)
        {
            try
            {
                if (string.IsNullOrEmpty(url))
                    throw new ArgumentException("Url can't be null or empty");

                T getCallResult = default;

                var response = httpClient.GetAsync(url);

                if (response.Result.IsSuccessStatusCode)
                {
                    var json = await response.Result.Content.ReadAsStringAsync();
                    getCallResult = JsonConvert.DeserializeObject<T>(json);
                }

                return getCallResult;

            }
            catch (ArgumentException ex)
            {
                throw ex;
            }
        }
    }
}
