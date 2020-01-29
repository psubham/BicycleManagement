using MapPointApi.model;
using MapPointApi.model.RepositoryLayer;
using MapPointApi.UserException;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MapPointApi.ServiceLayer
{
    public class HubService
    {
        private readonly HubRepo hubRepo;
        public HubService(HubRepo hubRepo)
        {
            this.hubRepo = hubRepo;
        }
        #region add new hub
        public async Task<bool> AddHub(Hub hub)
        {
           
            try
            {
                var result = await this.hubRepo.AddHub(hub);
                if(result==false)
                    throw new MapException("cannot created hub" );
                return true;
            }
            catch (Exception exp)
            {
                throw new MapException("cannot created hub"+exp.Message);
            }
            
        }

        #endregion

        #region get all hub
        public IEnumerable<Hub> GetHubs()
        {
            
            try
            {
                return this.hubRepo.GetHubs();
            }
            catch (Exception exp)
            {
                throw new MapException("Cannot fetch the hub");
            }

        }


        #endregion

        #region is hubid is there
        public async Task<bool> Ishub(int hubId)
        {
            var resultExist = await this.hubRepo.GetHub(hubId);
            if (resultExist == null)
                return false;
            return true;
        }
        #endregion

        #region get hub by id
        public async Task<Hub> GetHub(int id)
        {
            var recordExist =await this.hubRepo.GetHub(id);
            if(recordExist==null)
            {
                throw new MapException("no hub with id"+id);
            }
            return recordExist;
        }
        #endregion

        #region delete hub
        public async Task DeleteHub(int id)
        {
            var recordExist =await  this.hubRepo.GetHub(id);
            try
            {
                var result = await this.hubRepo.DeleteHub(recordExist);
            }
            catch(Exception exp)
            {
                throw new MapException("cannot delete the enitity"+exp);
            }
        }
        #endregion

        #region update hub
        public async Task UpdateHub(Hub value)
        {
            if (await this.hubRepo.GetHub(value.Id) == null)
                throw new MapException("element is not present");
            if (await this.hubRepo.UpdateHub(value) == false)
                throw new MapException("Cannot update the hub due server error");
        }
        #endregion

    }

}
