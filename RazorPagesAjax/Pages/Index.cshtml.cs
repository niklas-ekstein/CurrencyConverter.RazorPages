using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

namespace RazorPagesAjax.Pages
{
    //This class has the values off the currencies and sends a request to get the latest currency rates.
    public class IndexModel : PageModel
    {
        public string TestData { get; set; }
        

        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        //The currency values that are choosed between to send a request for the current rate.
        public void OnGet()
        {
            List<SelectListItem> listCurrency = new List<SelectListItem>() {
                        
                    new SelectListItem {
                        Text = "Choose currency", Value = "0"
                    },   
                    new SelectListItem {
                       Text = "Swedish krona", Value = "SEK"
                    },
                    new SelectListItem {
                        Text = "Bulgarian lev", Value = "BGN"
                    },
                    new SelectListItem {
                        Text = "New Zealand dollar", Value = "NZD"
                    },
                    new SelectListItem {
                        Text = "Israeli new shekel", Value = "ILS"
                    },
                    new SelectListItem {
                        Text = "Russian ruble", Value = "RUB"
                    },
                    new SelectListItem {
                        Text = "Canadian dollar", Value = "CAD"
                    },
                    new SelectListItem {
                        Text = "United States dollar", Value = "USD"
                    },
                    new SelectListItem {
                        Text = "Philippine peso", Value = "PHP"
                    },
                    new SelectListItem {
                        Text = "Swiss franc", Value = "CHF"
                    },
                    new SelectListItem {
                        Text = "Australian dollar", Value = "AUD"
                    },
                    new SelectListItem {
                        Text = "Japanese yen", Value = "JPY"
                    },
                    new SelectListItem {
                        Text = "Turkish lira", Value = "TRY"
                    },
                    new SelectListItem {
                        Text = "Hong Kong dollar", Value = "HKD"
                    },
                    new SelectListItem {
                        Text = "Malaysian ringgit", Value = "MYR"
                    },
                    new SelectListItem {
                        Text = "Croatian kuna", Value = "HRK"
                    },
                    new SelectListItem {
                        Text = "Czech koruna", Value = "CZK"
                    },
                    new SelectListItem {
                        Text = "Indonesian rupiah", Value = "IDR"
                    },
                    new SelectListItem {
                        Text = "Dansih Krone", Value = "DKK"
                    },
                    new SelectListItem {
                        Text = "Norwegian krone", Value = "NOK"
                    },
                    new SelectListItem {
                        Text = "Hungarian forint", Value = "HUF"
                    },
                    new SelectListItem {
                        Text = "British pound", Value = "GBP"
                    },
                    new SelectListItem {
                        Text = "Mexican peso", Value = "MXN"
                    },
                    new SelectListItem {
                        Text = "Thai baht", Value = "THB"
                    },
                    new SelectListItem {
                        Text = "Icelandic króna", Value = "ISK"
                    },
                    new SelectListItem {
                        Text = "South African rand", Value = "ZAR"
                    },
                    new SelectListItem {
                        Text = "Brazilian real", Value = "BRL"
                    },
                    new SelectListItem {
                        Text = "Polish złoty", Value = "PLN"
                    },
                    new SelectListItem {
                        Text = "Singapore dollar", Value = "SGD"
                    },
                    new SelectListItem {
                        Text = "South Korean won", Value = "KRW"
                    },
                    new SelectListItem {
                        Text = "Indian rupee", Value = "INR"
                    },
                    new SelectListItem {
                        Text = "Romanian leu", Value = "RON"
                    },
                    new SelectListItem {
                        Text = "Chinese yuan", Value = "CNY"
                    },
                    new SelectListItem {
                        Text = "Euro", Value = "EUR"
                    },
                    };
                        ViewData["ListOfCurrency"] = listCurrency;
                    }


        /// <summary>
        /// A request for the current exchange rate is sent based on the values choosen.
        /// The currency's exchange rate is than returned.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="number"></param>
        /// <returns></returns>
        public async Task<IActionResult> OnPostGetTimeAsync(string name) //, int number)
        {
            CurrencyModel currency = new CurrencyModel();

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://api.exchangeratesapi.io/latest");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            String response = await client.GetStringAsync("?symbols=" + name);
            JObject data = JObject.Parse(response);
            JObject currencyRates = (JObject)data.GetValue("rates");
            currency.Rate = currencyRates.ToString();
            currency.Rate = Regex.Replace(currency.Rate, "[^0-9.]", "");

            return new JsonResult(currency);
        }

    }
}
