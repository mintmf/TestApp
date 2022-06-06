using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Runtime.Caching;

namespace TestApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CurrenciesController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetCurrencies(int pageNumber=1, int numberOfItems=5)
        {
            try
            {
                var responseBody = HttpHelper.GetResponseString("https://www.cbr-xml-daily.ru/daily_json.js");
                Currencies c;

                if (CacheHelper.IsInCache("Currencies"))
                {
                    c = CacheHelper.GetFromCache<Currencies>("Currencies");
                }
                else
                {
                    c = JsonConvert.DeserializeObject<Currencies>(responseBody.Result);
                    var cachingTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59, 999) - DateTime.Now;
                    CacheHelper.SaveToCache("Currencies", c, DateTime.Now.Add(cachingTime));
                }

                var res = new Dictionary<string, Currency>();
                int i = 0;
                foreach (var x in c.Valute)
                {
                    if (i >= ((pageNumber - 1) * numberOfItems) && i < pageNumber * numberOfItems)
                    {
                        res.Add(x.Key, x.Value);
                    }
                    i++;
                }
                
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
