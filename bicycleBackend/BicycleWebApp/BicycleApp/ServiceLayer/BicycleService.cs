using BicycleApp.Data.model;
using BicycleApp.Data.RepositoryLayer;
using BicycleApp.UserException;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BicycleApp.ServiceLayer
{
    public class BicycleService
    {
        BicycleRepository _bicycleRepository;
        public BicycleService(BicycleRepository bicycleRepository)
        {
            _bicycleRepository = bicycleRepository;
        }
        #region Get all bicycle 
        public IEnumerable<Bicycles> GetAll()
        {
            return this._bicycleRepository.GetAllType();
        }
        #endregion

        #region Get  bicycle  by id
        public async Task<Bicycles> GetBicycleId(int id)
        {
            var result= await this._bicycleRepository.GetBicycleId(id);
            if(result==null)
            {
                throw new BicyclesException("Cannot get the get bicycle by id");
            }
            return result;

        }
        #endregion

        #region Get bicycles  by hubId
        public IEnumerable<Bicycles> GetBicycleHubId(int hubId)
        {
            var result = this._bicycleRepository.GetBicycleHubId(hubId);
            if (result == null)
            {
                throw new BicyclesException("Cannot get the get bicycle by  hub id");
            }
            return result;
        }
        #endregion

        #region Get bicycles  by typeId
        public IEnumerable<Bicycles> GetBicycleTypeId(int typeId)
        {
            var result = this._bicycleRepository.GetBicycleTypeId(typeId);
            if (result == null)
            {
                throw new BicyclesException("Cannot get the get bicycle by type id");
            }
            return result;
        }
        #endregion

        #region Add new bicycle 
        public async Task<bool> AddBicycle(Bicycles value)
        {
            value.BicycleNumber = this.GenerateNumber(value.TypeId);
            if( await this._bicycleRepository.AddBicycle(value)==false)
            {
                throw new BicyclesException("Cannot added new bicycle due to server error");
            }
            return true;
         }
        #endregion

        #region Delete bicycle by id
        public async Task<bool> Delete(int id)
        {
            var result = await this._bicycleRepository.GetBicycleId(id);
            if(result==null)
            {
                throw new BicyclesException($"Cannot find bicycle with id {id}");
            }
            if(await this._bicycleRepository.Delete(result)==false)
            {
                throw new BicyclesException("Cannot delete bicycle due to server error");
            }
            return true;
        }
        #endregion

        #region change Isrent property  by id
        public async Task<bool> Isrent(int id, bool val)
        {
            var result = await this._bicycleRepository.GetBicycleId(id);
            if (result == null)
            {
                throw new BicyclesException($"Cannot find bicycle with id {id}");
            }
            if (await this._bicycleRepository.Isrent(id,val) == false)
            {
                throw new BicyclesException("Cannot delete bicycle due to server error");
            }
            return true;
        }
        #endregion

        #region generate bicycle number
        public string GenerateNumber(int typeid)
        {
            string number = "";
            number = typeid.ToString();
            return number;
        }
        #endregion

        #region reserve a bike 
        public async Task<Bicycles> reserve(BookingDetail value)
        {
           try
            {
                var res = await this._bicycleRepository.Reserve(value);
                if (res ==null)
                    throw new BicyclesException("Element is not there");
                return res;
             }
            catch(Exception exp)
            {
                throw new BicyclesException(exp.Message);
            }
        }
        #endregion

        #region update bicycle
        public async Task<Bicycles> Update(Bicycles bicycle)
        {
            var res = await this._bicycleRepository.Update(bicycle);
            if (res == null)
                throw new BicyclesException("cannot update the bicycel");
            return res;
        }
        #endregion

        #region Get bicycle type  detail by bicycle id
        public async Task<BicycleType> GetType(int BicycleId)
        {
            Bicycles recordExist = await this._bicycleRepository.GetBicycleId(BicycleId);
            return await this._bicycleRepository.GetBicycleType(recordExist.TypeId);
        }

        #endregion

    }
}

