using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OlympFoodClient
{
    public class DishService
    {

        const string Url = "http://192.168.0.110:52924/api/dish/";
        //const string Url = "http://192.168.43.33:52924/api/dish/";

        private HttpClient GetClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            return client;
        }

        public async Task<IEnumerable<Dish>> Get()
        {
            HttpClient client = GetClient();
            string result = await client.GetStringAsync(Url);

            //var tmp = JsonConvert.DeserializeObject<IEnumerable<Dish>>(result);

            return JsonConvert.DeserializeObject<IEnumerable<Dish>>(result);
        }

    }
}
