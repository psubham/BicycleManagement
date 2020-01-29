using BicycleApp.Data.model;
using BicycleApp.Data.RepositoryLayer;
using BicycleApp.UserException;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BicycleApp.ServiceLayer
{
    public class BicycleTypeService
    {
        private BicycleTypeRepository _bicycleTypeRepository;
        public BicycleTypeService(BicycleTypeRepository bicycleTypeRepository)
        {
            this._bicycleTypeRepository = bicycleTypeRepository;
        }
        #region Add new type
        public async Task<bool> Add(BicycleType bicycleType)
        {
            var result=_bicycleTypeRepository.GetType(bicycleType.Name);
            if (result != null)
                throw new DuplicateElement("Element with same name is already there");
            try
            {
               await  _bicycleTypeRepository.AddType(bicycleType);
                return true;
            }
            catch (Exception exp)
            {
                throw new BicycleTypeException("Cannot add new type" + exp.Message);
            }
        }
        #endregion

        #region Get All bicycle type
        public IEnumerable<BicycleType>GetAllType()
        {
            try
            {
                return _bicycleTypeRepository.GetAllType();
            }
            catch(Exception exp)
            {
                throw new BicycleTypeException("Cannot Get all type" + exp.Message);
            }
        }
        #endregion

        #region get type by id
        public async Task<BicycleType> Get(int typeId)
        {
            try
            {
                return await this._bicycleTypeRepository.GetType(typeId);
            }
            catch(Exception exp)
            {
                throw new BicycleTypeException("Cannot get type by id" + exp.Message);
            }

        }
        #endregion

        #region get type by name
        public BicycleType Get(string name)
        {
            try
            {
                return this._bicycleTypeRepository.GetType(name);
            }
            catch (Exception exp)
            {
                throw new BicycleTypeException("Cannot get type by name" + exp.Message);
            }

        }
        #endregion

        #region delete type
        public async Task<bool> DeleteType(int typeId)
        {
            var res = await this._bicycleTypeRepository.GetType(typeId);
            if(res==null)
            {
                throw new Exception();
            }
            try
            {
                return await this._bicycleTypeRepository.DeleteType(res);
            }
            catch(Exception exp)
            {
                throw new BicycleTypeException("Cannot delete the type" + exp.Message);
            }
        }

        #endregion

        #region get all type by hub id
        public IEnumerable<BicycleType> GetAllType(int hubId)
        {
            try
            {
                return _bicycleTypeRepository.GetAllType(hubId);
            }
            catch (Exception exp)
            {
                throw new BicycleTypeException("Cannot Get all type" + exp.Message);
            }
        }
        #endregion

        #region upload photo
        public async Task<string> UploadPhoto(IFormFile files)
        {
            try
            {
                var res = await this._bicycleTypeRepository.UploadtoAzureAsync(files);
                if (res == "Unsuccessful")
                    throw new BicycleTypeException("Unsuccessfull in upload photo");
                return res;
            }
            catch (Exception exp)
            {
                throw new BicycleTypeException("Exception in upload photo" + exp.Message);
            }
        }

        #endregion


        #region bicycle tyoe exist or not
        public async Task<bool> IsBicycleType(int TypeId)
        {
            return await this._bicycleTypeRepository.IsBicycleType(TypeId);

        }
        #endregion
    }
}
