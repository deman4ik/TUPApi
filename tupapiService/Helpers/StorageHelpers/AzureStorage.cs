using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using tupapi.Shared.Enums;
using tupapiService.Helpers.ExceptionHelpers;

namespace tupapiService.Helpers.StorageHelpers
{
    public class AzureStorage : IDisposable
    {
        private readonly CloudBlobContainer _postsContainer;

        public AzureStorage()
        {
            try
            {
                // Retrieve storage account from connection string.
                 var storageAccount = CloudStorageAccount.Parse(
                     CloudConfigurationManager.GetSetting("StorageConnectionString"));

                // Create the blob client.
                 var blobClient = storageAccount.CreateCloudBlobClient();

                // Retrieve a reference to a container.
                _postsContainer = blobClient.GetContainerReference(Const.StoragePostsContainer);

                // Create the container if it doesn't already exist.
                _postsContainer.CreateIfNotExists();

                // Enable anonymous read access to BLOBs.
                _postsContainer.SetPermissions(new BlobContainerPermissions
                {
                    PublicAccess = BlobContainerPublicAccessType.Blob
                });
            }
            catch (Exception ex)
            {
                throw new ApiException(ApiResult.Unknown, ErrorType.StorageError, ex.Message, ex.InnerException);
            }
        }


        public string GetPostsSas()
        {
            try
            {
                SharedAccessBlobPolicy sasPolicy = new SharedAccessBlobPolicy()
                {
                    SharedAccessStartTime = DateTime.UtcNow,
                    SharedAccessExpiryTime = DateTime.UtcNow.AddMinutes(5),
                    Permissions = SharedAccessBlobPermissions.Write
                };
                
                return _postsContainer.Uri + _postsContainer.GetSharedAccessSignature(sasPolicy);
            }
            catch (Exception ex )
            {
                
                throw new ApiException(ApiResult.Unknown, ErrorType.StorageError,ex.Message);
            }
            
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}