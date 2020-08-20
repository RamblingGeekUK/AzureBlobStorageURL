using System;
using Microsoft.Azure.Storage.Blob;

namespace AzureBlobStorageURL
{
    public class BlobManager
    { 
        public string GetFileURI(CloudBlobContainer container, string fullFileName)
        {
            string sasContainerToken;

            SharedAccessBlobPolicy adHocPolicy = new SharedAccessBlobPolicy()
            {
                // Set start time to five minutes before now to avoid clock skew.
                SharedAccessStartTime = DateTime.UtcNow.AddMinutes(-5),
                SharedAccessExpiryTime = DateTime.UtcNow.AddMinutes(1),
                Permissions = SharedAccessBlobPermissions.Read
            };

            sasContainerToken = container.GetSharedAccessSignature(adHocPolicy, null);
            CloudBlob blob = container.GetBlobReference(fullFileName);
            string newUri = new Uri(blob.Uri.AbsoluteUri + sasContainerToken).ToString();
            
            return newUri;
        }
    }
}