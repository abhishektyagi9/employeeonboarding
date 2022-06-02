using employeeonboardingmvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;

namespace employeeonboardingmvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration Configuration;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            Configuration = configuration;
            
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        // POST: EmployeeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(employeeformdata collection, IFormFile drivinglicense, IFormFile socialsecurity)
            {
            try
            {
                try
                {
                    documents objemployeeformdata = new documents();
                    //objemployeeformdata = collection;
                    using (var memoryStream = new MemoryStream())
                    {
                        await drivinglicense.CopyToAsync(memoryStream);
                        objemployeeformdata.DLimagefile =  memoryStream.ToArray();
                    }
                    using (var memoryStream = new MemoryStream())
                    {
                        await socialsecurity.CopyToAsync(memoryStream);
                        objemployeeformdata.socialimagefile = memoryStream.ToArray();
                    }
                    collection.docs = objemployeeformdata;

                    var Url  = Configuration["AzureFunctionURL"];

                    Uri u = new Uri(Url);
                    //using (var memoryStream = new MemoryStream())
                    //{
                    //    await drivinglicense.CopyToAsync(memoryStream);
                    //    collection.Files[""]. = memoryStream.ToArray();
                    //}
                    //using (var memoryStream = new MemoryStream())
                    //{
                    //    await socialsecurity.CopyToAsync(memoryStream);
                    //    collection.docs.socialimagefile = memoryStream.ToArray();
                    //}
                    var jsonToReturn = JsonConvert.SerializeObject(collection);
                    var response = string.Empty;
                    HttpContent c = new StringContent(jsonToReturn, Encoding.UTF8, "application/json");
                    using (var client = new HttpClient())
                    {
                        HttpRequestMessage request = new HttpRequestMessage
                        {
                            Method = HttpMethod.Post,
                            RequestUri = u,
                            Content = c
                        };

                        HttpResponseMessage result = await client.SendAsync(request);
                        if (result.IsSuccessStatusCode)
                        {
                            response = result.StatusCode.ToString();
                        }
                    }
                   
                }
                catch
                {
                    return View();
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}