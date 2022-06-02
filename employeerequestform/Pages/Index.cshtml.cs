using employeerequestform.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace employeerequestform.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
         private AppSettings AppSettings { get; set; }  
  
         
        public IndexModel(ILogger<IndexModel> logger, IOptions<AppSettings> settings)
        {
            AppSettings = settings.Value;
            _logger = logger;
        }

        public void OnGet()
        {

        }
        public static void SerializeJsonIntoStream(object value, Stream stream)
        {
            using (var sw = new StreamWriter(stream, new UTF8Encoding(false), 1024, true))
            using (var jtw = new JsonTextWriter(sw) { Formatting = Formatting.None })
            {
                var js = new JsonSerializer();
                js.Serialize(jtw, value);
                jtw.Flush();
            }
        }
        public void OnPost()
        {

            string Test = "Test";

        }
        private static HttpContent CreateHttpContent(object content)
        {
            HttpContent httpContent = null;

            if (content != null)
            {
                var ms = new MemoryStream();
                SerializeJsonIntoStream(content, ms);
                ms.Seek(0, SeekOrigin.Begin);
                httpContent = new StreamContent(ms);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            }

            return httpContent;
        }
        public async Task<IActionResult> OnPostAsync(employeeformdata objemployeeform)
        {
            var Url = AppSettings.AzureFunctionURL;

          

            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage(HttpMethod.Post, Url))
            using (var httpContent = CreateHttpContent(objemployeeform))
            {
                request.Content = httpContent;

                using (var response = await client
                    .SendAsync(request, HttpCompletionOption.ResponseHeadersRead)
                    .ConfigureAwait(false))
                {

                    var resualtList = response.Content.ReadFromJsonAsync<List<employeeformdata>>();

                 

                    return Page();
                }
            }

        }

    }
}