using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Azure;
using Azure.Core;
using Azure.Messaging.EventGrid;
using Azure.Storage.Blobs;
using employeeonboarding.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;


namespace employeeonboarding
{
    public class employeeonboardrequest
    {
        private readonly ILogger<employeeonboardrequest> _logger;

        public employeeonboardrequest(ILogger<employeeonboardrequest> log)
        {
            _logger = log;
        }

        [FunctionName("employeeonboardrequest")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
         ILogger _logger)
        {


          //Reading data from http request
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            employeeformdata employeedata = JsonConvert.DeserializeObject<employeeformdata>(requestBody);

            //store employee documents to blob storage
            Stream objStream = new MemoryStream(employeedata.docs.socialimagefile);
            await uploadblob(objStream, employeedata.alias + "-social" + ".png");

            Stream objStreamDL = new MemoryStream(employeedata.docs.socialimagefile);
            await uploadblob(objStreamDL, employeedata.alias + "-dl" + ".png");

            //Generate new password
            var password = Guid.NewGuid().ToString("d").Substring(1, 8);
            employeedata.password = password;
            employeedata.id = Guid.NewGuid().ToString();

            //Send event to Event Grid
            employeedata.docs = new documents();
            var jsonEvent = JsonConvert.SerializeObject(employeedata);
            EventGridPublisherClient client = new EventGridPublisherClient(new Uri("https://employeeonboarding.southcentralus-1.eventgrid.azure.net/api/events"), new AzureKeyCredential("BYxUM+OxRBHlshbV8PJDsniOuq/dpUH/v7RU7Q+xiMU="));
            EventGridEvent egEvent = new EventGridEvent("County-City-HR", "EmployeeHired", "1.0", jsonEvent);
            await client.SendEventAsync(egEvent);
            return new OkObjectResult(employeedata);
        }

        public static async Task uploadblob(Stream objdata, string blobname)
        {
            string connectionString = Environment.GetEnvironmentVariable("AzureWebJobsStorage");


            BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);
            try
            {
                // Create the container
                var containerClient = blobServiceClient.GetBlobContainerClient("documents");
                await containerClient.CreateIfNotExistsAsync(Azure.Storage.Blobs.Models.PublicAccessType.None);
                await containerClient.UploadBlobAsync(blobname, objdata);

            }
            catch (RequestFailedException e)
            {
                Console.WriteLine("HTTP error code {0}: {1}",
                                    e.Status, e.ErrorCode);
                Console.WriteLine(e.Message);
            }
        }
    }
}

