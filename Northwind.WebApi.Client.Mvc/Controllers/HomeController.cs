using Microsoft.AspNetCore.Mvc;
using Northwind.WebApi.Client.Mvc.Models;
using System.Diagnostics;
using ALLinONE.Shared; // Product, Customer

namespace Northwind.WebApi.Client.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory clientFactory;

        public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            clientFactory = httpClientFactory;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Route("home/products/{name?}")]
        public async Task<IActionResult> Products(string? name)
        {
            HttpClient client = clientFactory.CreateClient(
                name: "Northwind.WebApi.Service");

            HttpRequestMessage request = new(
                method: HttpMethod.Get, requestUri: $"api/products/{name}");

            HttpResponseMessage response = await client.SendAsync(request);

            IEnumerable<Product>? model = await response.Content    
                .ReadFromJsonAsync<IEnumerable<Product>>();

            ViewData["baseaddress"] = client.BaseAddress;

            return View(model);
        }

        [Route("home/customers/{name?}")]
        public async Task<IActionResult> Customers(string? name)
        {
            HttpClient client = clientFactory.CreateClient(
                name: "Northwind.WebApi.Service");

            HttpRequestMessage request = new(
                method: HttpMethod.Get, requestUri: $"api/Customers/{name}");

            HttpResponseMessage response = await client.SendAsync(request);

            IEnumerable<Customer>? model = await response.Content
                .ReadFromJsonAsync<IEnumerable<Customer>>();

            ViewData["baseaddress"] = client.BaseAddress;

            return View(model);
        }
    }
}
