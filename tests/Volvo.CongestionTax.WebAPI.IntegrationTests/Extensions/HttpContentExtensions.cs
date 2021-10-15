using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Volvo.CongestionTax.WebAPI.IntegrationTests.Extensions
{
    public static class HttpContentExtensions
    {
        public static async Task<TModel> DeserializeAsAsync<TModel>(this HttpContent httpContent)
        {
            var json = await httpContent.ReadAsStringAsync();

            var model = JsonConvert.DeserializeObject<TModel>(json);

            return model;
        }
    }
}