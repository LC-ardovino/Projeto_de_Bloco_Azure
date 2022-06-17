using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SocialNetwork.Api.Models;
using SocialNetwork.Core.Models;
using SocialNetwork.DataAccess.Repositories;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using Microsoft.WindowsAzure.Storage;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage.Blob;
using System.IO;
using System.Threading.Tasks;
using Azure.Storage.Blobs;

namespace SocialNetwork.Api.Controllers
{
    public class ProfilesController : ApiController
    {
        // GET: api/Profiles
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Profiles/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Profiles
        public async Task<IHttpActionResult> Post()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                return BadRequest();
            }

            var result = await Request.Content.ReadAsMultipartAsync();

            var requestJson = await result.Contents[0].ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<ProfileBindingModel>(requestJson);

            if (result.Contents.Count > 1)
            {
                model.PictureUrl = await CreateBlob(result.Contents[1]);
            }

            var accountId = User.Identity.GetUserId();

            var profile = new Profile()
            {
                AccountId = accountId,
                FirstName = model.FirstName,
                LastName = model.LastName,
                BirthDate = model.BirthDate
            };

            var repository = new ProfileRepository();
            repository.Create(profile);

            return Ok();
        }

        private async Task<string> CreateBlob(HttpContent httpContent)
        {
            string StorageConnectionString = "DefaultEndpointsProtocol=https;AccountName=storageardovino;AccountKey=rIWRyN0vVMDBY5b1mPK+gOCKOhowKUajYFQ0eOsbDQCFEk5T7YglInupgt6ILzdyQPaQ6qEja9tF+AStkGRo1g==;EndpointSuffix=core.windows.net";

            BlobServiceClient blobServiceClient = new BlobServiceClient(StorageConnectionString);
            var blobContainerName = "imagensardovino";

            var blobContainer = blobServiceClient.GetBlobContainerClient(blobContainerName);

            var fileName = httpContent.Headers.ContentDisposition.FileName;

            BlobClient blobClient = blobContainer.GetBlobClient(fileName);

            await blobClient.UploadAsync(fileName);

            return blobClient.Uri.AbsoluteUri
;        }

        private string GetRandomBlobName(string filename)
        {
            string ext = Path.GetExtension(filename);
            return string.Format("{0:10}_{1}{2}", DateTime.Now.Ticks,Guid.NewGuid(), ext);
        }

        // PUT: api/Profiles/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Profiles/5
        public void Delete(int id)
        {
        }
    }
}
