using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AzureDevOpsConnector
{
    public static class AzureDevOpsConnector
    {
        [FunctionName("AzureDevOpsConnector")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            var commentText = data?.detailedMessage["text"].ToString();

            return commentText != null
                ? (ActionResult)new OkObjectResult($"Hello, {commentText}")
                : new BadRequestObjectResult("Please pass a name on the query string or in the request body");
        }               
    }
}
