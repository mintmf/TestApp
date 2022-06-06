using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Microsoft.Extensions.Caching.Memory;

namespace TestApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CurrencyController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetCurrency(string currencyID)
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
                    CacheHelper.SaveToCache("Currencies", c, DateTime.Now.AddSeconds(60));
                }

                foreach (var x in c.Valute)
                {
                    if (x.Value.Id == currencyID)
                    {
                        return Ok(x.Value);
                    }
                }

                return BadRequest(400);
            }
            catch (Exception m)
            {
                return BadRequest(m.Message);
            }
        }
    }
}
