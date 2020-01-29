using BicycleApp.Data.DataAccessLayer;
using BicycleApp.Data.model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BicycleApp.Data.RepositoryLayer
{
    public class BicycleTypeRepository
    {
        BicycleDbContext dbContext;
        IHostingEnvironment _environment;
        azureDb azure;
        public BicycleTypeRepository(BicycleDbContext bicycleDbContext,
            IHostingEnvironment environment,
            azureDb azure)
        {
            this.dbContext = bicycleDbContext;
            _environment = environment;
            this.azure = azure;
        }

        #region Get all bicycle type
        public IEnumerable<BicycleType> GetAllType()
        {
            return this.dbContext.BicycleType.ToList();
        }
        #endregion

        #region Get  bicycle type  by id
        public async Task<BicycleType> GetType(int id)
        {
            return await this.dbContext.BicycleType.FindAsync(id);
        }
        #endregion

        #region Get bicycle type by name
        public BicycleType GetType(string name)
        {
          return (from bicycletype in dbContext.BicycleType
             where bicycletype.Name == name
             select bicycletype).FirstOrDefault(); 
        }
        #endregion

        #region Add new bicycle type
        public async Task<bool> AddType(BicycleType value)
        {
            await dbContext.BicycleType.AddAsync(value);
            await dbContext.SaveChangesAsync();
            return true;
        }
        #endregion

        #region Delete bicycle type
        public async Task<bool> DeleteType(BicycleType bicycleType)
        {
            dbContext.BicycleType.Remove(bicycleType);
            await dbContext.SaveChangesAsync();
            return true;
        }
        #endregion

        #region get type based on hubIb
        public IEnumerable<BicycleType> GetAllType(int hubId)
        {
            return dbContext.BicycleType
                         .FromSql($"select * from BicycleType where BicycleType.TypeId In (select Bicycles.TypeId from Bicycles where HubId={hubId} and IsRent={false}) ")
                         .ToList();
        }
        #endregion

        #region upload image
        public string UploadImage(IFormFile files)
        {
            if (files.Length > 0)
            {
                try
                {
                    if (!Directory.Exists(_environment.WebRootPath + "\\uploads\\"))
                    {
                        Directory.CreateDirectory(_environment.WebRootPath + "\\uploads\\");
                    }
                    using (FileStream filestream = System.IO.File.Create(_environment.WebRootPath + "\\uploads\\" + DateTime.Now.ToString("yymmssff") + files.FileName))
                    {
                        files.CopyTo(filestream);
                        filestream.Flush();
                        var res = "\\uploads\\" + DateTime.Now.ToString("yymmssff") + files.FileName;
                        return JsonConvert.SerializeObject(res).ToString();

                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                return "Unsuccessful";
            }

        }

        private string GenerateFileName(string fileName)
        {
            string strFileName = string.Empty;
            string[] strName = fileName.Split('.');
            strFileName = DateTime.Now.ToUniversalTime().ToString("yyyy-MM-dd") + "/" + DateTime.Now.ToUniversalTime().ToString("yyyyMMdd\\THHmmssfff") + "." + strName[strName.Length - 1];
            return strFileName;
        }
        public async Task<string> UploadtoAzureAsync(IFormFile files)
        {
            try
            {
                CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(azure.accesKey);
                CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
                string strContainerName = "images";
                CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference(strContainerName);
                string fileName = this.GenerateFileName(files.FileName);

                if (await cloudBlobContainer.CreateIfNotExistsAsync())
                {
                    await cloudBlobContainer.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });
                }

                if (files.FileName != null && files != null)
                {
                    CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(fileName);
                    
                    using (var fileStream =files.OpenReadStream())
                    {
                        await cloudBlockBlob.UploadFromStreamAsync(fileStream);
                    }
                    var sharedPolicy = cloudBlockBlob.GetSharedAccessSignature(new SharedAccessBlobPolicy()
                    {
                        Permissions = SharedAccessBlobPermissions.Read,
                        SharedAccessExpiryTime = DateTime.UtcNow.AddDays(10)
                    });
                    var url = JsonConvert.SerializeObject(string.Format("{0}{1}", cloudBlockBlob.Uri, sharedPolicy));

                    return url;
                }
                return "";
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion

        #region bicycle type exist or not
        public async Task<bool> IsBicycleType(int TypeId)
        {
            var recordExist= await this.dbContext.BicycleType.FindAsync(TypeId);
            if (recordExist == null)
                return false;
            return true;

        }
        #endregion
    }
}
