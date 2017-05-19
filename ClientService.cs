using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OlympFoodClient
{
    public class ClientService
    {
        const string Url = "http://192.168.0.110:52924/api/client/";
        //const string Url = "http://192.168.43.33:52924/api/client/";

        private HttpClient GetClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            return client;
        }

        public async Task<bool> CheckConnection()
        {
            HttpClient client = GetClient();
            HttpResponseMessage response;           
            try
            {
                response = await client.GetAsync(Url + "/#DefaultUsername#");
                return false; //Соединение есть, IsOfflineMode = false;
            }
            catch (HttpRequestException e)
            {
                return true; //Соединение есть, IsOfflineMode = true;
            }
        }

        public async Task<bool> Check(Client clt)
        {
            HttpClient client = GetClient();
            HttpResponseMessage response;

            try
            {
                response = await client.GetAsync(Url + "/" + clt.Login);
                if (response.StatusCode == HttpStatusCode.OK)
                {

                    var getclt = JsonConvert.DeserializeObject<Client>(await response.Content.ReadAsStringAsync());
                    //return true;
                    if (getclt == null || getclt.Login == "#NullClient#") return false;
                    if (clt.Password == getclt.Password) return true;
                    else return false;
                }
                else return false;
            }
            catch (HttpRequestException e)
            {
                return false;
            }          
        }      

        public async Task<Client> Registrate(Client clt)
        {
            HttpClient client = GetClient();
            HttpResponseMessage response;

            try
            {
                response = await client.PostAsync(Url,
                    new StringContent(
                        JsonConvert.SerializeObject(clt),
                        Encoding.UTF8, "application/json"));

                if (response.StatusCode != HttpStatusCode.OK)
                    return null;
                return JsonConvert.DeserializeObject<Client>(
                   await response.Content.ReadAsStringAsync());
            }
            catch (HttpRequestException e)
            {
                var tempclt = new Client { Login = "#RequestException#", Password = "#RequestException#" };
                return tempclt;
            }

            
        }
    }
}
