using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BicycleApp.Data.model;
using BicycleApp.ServiceLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace BicycleApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BicycleTypeController : ControllerBase
    {
        private BicycleTypeService _bicycleTypeService;
       
        public BicycleTypeController(BicycleTypeService bicycleTypeService)
        {
            this._bicycleTypeService = bicycleTypeService;
            
        }

        #region get all type
        [HttpGet]
        public IEnumerable<BicycleType> Get()
        {
            return this._bicycleTypeService.GetAllType();
        }
        #endregion

        #region get all bicycle type
        //get: api/bicycle/5
        [HttpGet("{id}", Name = "Get")]
        public IEnumerable<BicycleType>  Get(int id)
        {
            return this._bicycleTypeService.GetAllType(id);
        }
        #endregion

        #region get bicycletype id
        [HttpGet("GetBicycleType/{id}")]
        public async Task<BicycleType> GetBicycleType(int id)
        {
            return await this._bicycleTypeService.Get(id);
        }
        #endregion

        #region add bicycel type
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<bool> Post([FromBody] BicycleType value )
        {

             return await this._bicycleTypeService.Add(value);
        }
        #endregion

        #region upload photo
        [HttpPost("upload")]
        public async Task<string> Post([FromForm] IFormFile files)
        {
            return (await this._bicycleTypeService.UploadPhoto(files));
        }
        #endregion

        #region check bicycle type 
        [HttpPost("IsBicycleType")]
        public async Task<bool> IsBicycleType([FromBody] int TypeId)
        {
            return await this._bicycleTypeService.IsBicycleType(TypeId);
        }
        #endregion
    }
}
