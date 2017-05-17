using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OlympFoodClient
{
    public class OrderService
    {
        const string Url = "http://192.168.0.110:52924/api/order/";
        //const string Url = "http://192.168.43.33:52924/api/order/";

        private HttpClient GetClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            return client;
        }

        public async Task<IEnumerable<Order>> Get()
        {
            HttpClient client = GetClient();
            string result = await client.GetStringAsync(Url);
            return JsonConvert.DeserializeObject<IEnumerable<Order>>(result);
        }


        public async Task<Order> Add(Order ord)
        {
            HttpClient client = GetClient();

            //ord.Status = "Processing";              // ЗАХАРДКОЖЕНО!!!
            //ord.Nickname = "RedMonsta";

            var response = await client.PostAsync(Url,
                new StringContent(
                    JsonConvert.SerializeObject(ord),
                    Encoding.UTF8, "application/json"));

            if (response.StatusCode != HttpStatusCode.OK)
                return null;

            return JsonConvert.DeserializeObject<Order>(
                await response.Content.ReadAsStringAsync());
            //Order tmpord = JsonConvert.DeserializeObject<Order>(await response.Content.ReadAsStringAsync());
            //tmpord.Status = "Processing";
            //tmpord.Nickname = "RedMonsta";
            //return tmpord;
        }

        public async Task<Order> Update(Order ord)
        {
            HttpClient client = GetClient();

            //ord.Status = "Processing";              // ЗАХАРДКОЖЕНО!!!
            //ord.Nickname = "RedMonsta";

            var response = await client.PutAsync(Url + "/" + ord.Id,
                new StringContent(
                    JsonConvert.SerializeObject(ord),
                    Encoding.UTF8, "application/json"));

            if (response.StatusCode != HttpStatusCode.OK)
                return null;

            //Order tmpord = JsonConvert.DeserializeObject<Order>( await response.Content.ReadAsStringAsync());
            //tmpord.Status = "Processing";
            //tmpord.Nickname = "RedMonsta";
            //return tmpord;
            return JsonConvert.DeserializeObject<Order>(
                await response.Content.ReadAsStringAsync());
        }

        public async Task<Order> Delete(int id)
        {
            HttpClient client = GetClient();
            var response = await client.DeleteAsync(Url + "/" + id);
            if (response.StatusCode != HttpStatusCode.OK)
                return null;

            return JsonConvert.DeserializeObject<Order>(
               await response.Content.ReadAsStringAsync());
        }

    }
}
