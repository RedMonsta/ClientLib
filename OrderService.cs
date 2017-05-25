﻿using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System;

namespace OlympFoodClient
{
    public class OrderService
    {
        const string Url = "http://192.168.0.111:52924/api/order/";
        //const string Url = "http://192.168.43.33:52924/api/order/";

        private HttpClient GetClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            return client;
        }

        //public async Task<IEnumerable<Order>> Get()
        //{
        //    HttpClient client = GetClient();          
        //    try
        //    {
        //        string result;
        //        result = await client.GetStringAsync(Url);
        //        return JsonConvert.DeserializeObject<IEnumerable<Order>>(result);
        //    }
        //    catch (HttpRequestException e)
        //    {
        //        var tmplist = new List<Order>();
        //        var tmpord = new Order { Name = "#RequestException#", Dish = "#RequestException#" };
        //        tmplist.Add(tmpord);
        //        return tmplist;
        //    }           
        //}

        public async Task<IEnumerable<Order>> Get(string login, string password)
        {
            HttpClient client = GetClient();
            try
            {
                string result;
                result = await client.GetStringAsync(Url + "/" + login + "/" + Authorizer.EncryptStringByBase64(password));
                return JsonConvert.DeserializeObject<IEnumerable<Order>>(result);
            }
            catch (HttpRequestException e)
            {
                var tmplist = new List<Order>();
                var tmpord = new Order { Name = "#RequestException#", Dish = "#RequestException#" };
                tmplist.Add(tmpord);
                return tmplist;
            }
        }

        public async Task<Order> Add(Order ord, string login, string password)
        {
            HttpClient client = GetClient();
            HttpResponseMessage response;
            var cltord = new ClientPlusOrder { client = new Client { Login = login, Password = Authorizer.EncryptStringByBase64(password) }, order = ord };
            try
            {
                response = await client.PostAsync(Url,
                    new StringContent(
                        JsonConvert.SerializeObject(cltord),
                        Encoding.UTF8, "application/json"));

                if (response.StatusCode != HttpStatusCode.OK)
                    return null;

                return JsonConvert.DeserializeObject<Order>(
                    await response.Content.ReadAsStringAsync());
            }
            catch (HttpRequestException e)
            {
                var order = new Order { Name = "#RequestException#", Dish = "#RequestException#" };
                return order;
            }
        }

        //public async Task<Order> Update(Order ord, string login, string password)
        //{
        //    HttpClient client = GetClient();
        //    HttpResponseMessage response;
        //    var cltord = new ClientPlusOrder { client = new Client { Login = login, Password = password }, order = ord };
        //    try
        //    {
        //        response = await client.PutAsync(Url + "/" + ord.Id,
        //            new StringContent(
        //                JsonConvert.SerializeObject(cltord),
        //                Encoding.UTF8, "application/json"));

        //        if (response.StatusCode != HttpStatusCode.OK)
        //            return null;

        //        return JsonConvert.DeserializeObject<Order>(
        //            await response.Content.ReadAsStringAsync());
        //    }
        //    catch (HttpRequestException e)
        //    {
        //        var order = new Order { Name = "#RequestException#", Dish = "#RequestException#" };
        //        return order;
        //    }
        //}

        public async Task<Order> Delete(int id, string login, string password)
        {
            HttpClient client = GetClient();
            HttpResponseMessage response;
            try
            {
                response = await client.DeleteAsync(Url + "/" + id.ToString() + "/" + login + "/" + Authorizer.EncryptStringByBase64(password));
                if (response.StatusCode != HttpStatusCode.OK)
                    return null;

                return JsonConvert.DeserializeObject<Order>(
                   await response.Content.ReadAsStringAsync());
            }
            catch (HttpRequestException e)
            {
                var order = new Order { Name = "#RequestException#", Dish = "#RequestException#" };
                return order;
            }
        }

    }
}
