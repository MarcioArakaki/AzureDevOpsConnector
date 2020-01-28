using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace AzureDevOpsConnector
{
    public static class AzureDevOpsConnectorInbound
    {
        [FunctionName("AzureDevOpsConnectorInbound")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Admin, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            try
            {
                var azureDevOpsToken = Environment.GetEnvironmentVariable("AzureDevOpsToken");
                var azureDevopToken = Environment.GetEnvironmentVariable("AzureDevOpsUser");
                var azureDevopsUri = Environment.GetEnvironmentVariable("AzureDevOpsUri");

                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(
                        new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    
                    
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                        Convert.ToBase64String(
                            System.Text.ASCIIEncoding.ASCII.GetBytes(
                                string.Format("{0}:{1}", "TestApiToken", azureDevOpsToken))));

                    var json = JsonConvert.SerializeObject(new { text = "testApi1" });
                    
                    var response = client.PostAsync(
                                azureDevopsUri,new StringContent(json, Encoding.UTF8, "application/json"));

                    if (response.Result.IsSuccessStatusCode)
                        return (ActionResult)new OkObjectResult("Deu Bom");

                    return (ActionResult)new OkObjectResult("Deu alguma coisa");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                 return (ActionResult)new OkObjectResult("Deu ruim");
            }

        }
    }
}
