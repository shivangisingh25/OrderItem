using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OrderItem;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace order.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        readonly string Baseurl = "http://20.189.120.18/";
        [HttpGet]
        [Route("Add/{menuitemid}")]
        public async Task<String> Get(int menuitemid)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync("api/MenuItem/Get/" + menuitemid);
                if (response.IsSuccessStatusCode)
                {
                    var name = response.Content.ReadAsStringAsync().Result;
                    //string obj = JsonConvert.DeserializeObject<string>(name);
                    return name;
                }
                return null;
            }
        }
        [HttpPost]
        [Route("value/{menuitemid}")]
        public async Task<Cart> post(int menuitemid)
        {
            Task<String> res = Get(menuitemid);
            string r = await res;
            int l = r.Length;
            string value = r.Substring(1, l - 2);
            Cart cart = new Cart()
            {
                Id = 1,
                userId = 1,
                menuItemId = menuitemid,
                menuItemName = value
            };
            return cart;
        }
    }
}