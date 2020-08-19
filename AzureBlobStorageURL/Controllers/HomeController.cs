using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AzureBlobStorageURL.Models;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Auth;
using System;
    
namespace AzureBlobStorageURL.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BlobManager _blobManager;

        public HomeController(ILogger<HomeController> logger, BlobManager blobManager)
        {
            _logger = logger;
            _blobManager = blobManager;
        }

        public IActionResult Index()
        {
            StorageCredentials storageCredentials = new StorageCredentials(System.Environment.GetEnvironmentVariable("AzBlobAccount"), System.Environment.GetEnvironmentVariable("AzBlobSharedAccessSignature"));
            CloudStorageAccount storageAccount = new CloudStorageAccount(storageCredentials, useHttps: true);
            string containerName = "test";

            //Create the blob client object.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            //Get a reference to a container to use for the sample code, and create it if it does not exist.
            CloudBlobContainer container = blobClient.GetContainerReference(containerName);

            ViewBag.URL = _blobManager.GetFileURI(container, "SAMPLE.pdf");

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
    }
}