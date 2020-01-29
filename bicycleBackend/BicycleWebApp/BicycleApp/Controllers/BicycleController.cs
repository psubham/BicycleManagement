using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BicycleApp.Data.DataAccessLayer;
using BicycleApp.Data.model;
using BicycleApp.ServiceLayer;
using BicycleApp.UserException;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BicycleApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ExceptionHandler]
    public class BicycleController : ControllerBase
    {
        BicycleService _bicycleService;
        public BicycleController(BicycleService bicycleService)
        {
            this._bicycleService = bicycleService;
        }
        #region Get all Bicycle details
        [HttpGet("GetAllBicycle")]
        public IEnumerable<Bicycles> Get()
        {
            return this._bicycleService.GetAll();
        }
        #endregion

        #region Add Bicycle details
        [HttpPost("AddBicycle")]
        [Authorize(Roles="admin")]
        public async Task<bool> Post([FromBody] Bicycles value)
        {
            return await this._bicycleService.AddBicycle(value);
        }
        #endregion

        #region delete bicycle
        [Authorize(Roles = "admin")]
        [HttpPost("DeleteBicycle")]
        public async Task<bool> Delete(Bicycles value)
        {
            return await  _bicycleService.Delete(value.BicycleId);
        }
        #endregion

        #region Get  Bicycle detail by id
        [HttpPost("GetBicycleId")]
        public async Task<Bicycles> GetBicycleId([FromBody]int id)
        {
            return await this._bicycleService.GetBicycleId(id);
        }
        #endregion

        #region reserve a bike
        [HttpPost("reserve")]
        public async Task<Bicycles>  reserve(BookingDetail value)
        {
            return await this._bicycleService.reserve(value);
        }
        #endregion

        #region update Bicycle details
        [Authorize(Roles = "admin")]
        [HttpPut("UpdateBicycle")]
        public async Task<Bicycles> UpdateBicycle(Bicycles value)
        {
            return await this._bicycleService.Update(value);
        }
        #endregion

        #region get bicycel by id
        [HttpGet("GetType/{BicycleId}")]
        public async Task<BicycleType> GetType(int BicycleId)
        {
            return await this._bicycleService.GetType(BicycleId);
        }
        #endregion
    }
}
