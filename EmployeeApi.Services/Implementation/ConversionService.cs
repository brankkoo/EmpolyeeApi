using EmployeeApi.Services.Base;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.Http.Headers;

namespace EmployeeApi.Services.Implementation
{
    public class ConversionService : IConversionService
    {
        public async Task<List<float>> ConvertToEurAndUsd(float rsd)
        {
            //failing request FIX ASAP 
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("apikey", "QOrSVVDRdu0IGpMRR87bXKeKG6NulLzy");
                var responseEur = await client.GetAsync($"https://api.apilayer.com/currency_data/convert?to=EUR&from=RSD&amount={rsd}");
                var responseUsd = await client.GetAsync($"https://api.apilayer.com/currency_data/convert?to=USD&from=RSD&amount={rsd}");
                if (responseEur.IsSuccessStatusCode & responseUsd.IsSuccessStatusCode)
                {
                    var jsonStringEur = await responseEur.Content.ReadAsStringAsync();
                    var jsonStringUsd = await responseUsd.Content.ReadAsStringAsync();

                    JObject jsonEur = JObject.Parse(jsonStringEur);
                    JObject jsonUsd = JObject.Parse(jsonStringUsd);

                    float valueEur = jsonEur["result"].Value<float>();
                    float valueUsd = jsonUsd["result"].Value<float>();

                    return new List<float> { valueEur, valueUsd };
                }
                else
                    throw new Exception("Request failed");
            }

        }
    }
}
