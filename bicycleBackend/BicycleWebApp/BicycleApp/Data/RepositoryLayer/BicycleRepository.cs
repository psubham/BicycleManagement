using BicycleApp.Data.DataAccessLayer;
using BicycleApp.Data.model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BicycleApp.Data.RepositoryLayer
{
    public class BicycleRepository
    {
        BicycleDbContext dbContext;
        public BicycleRepository(BicycleDbContext bicycleDbContext)
        {
            this.dbContext = bicycleDbContext;
        }

        #region Get all bicycle 
        public IEnumerable<Bicycles> GetAllType()
        {
            return this.dbContext.Bicycles.ToList();
        }
        #endregion

        #region Get  bicycle  by id
        public async Task<Bicycles> GetBicycleId(int id)
        {
            return await this.dbContext.Bicycles.FindAsync(id);
          
        }
        #endregion

        #region Get bicycle type by hubId
        public IEnumerable<Bicycles> GetBicycleHubId(int hubId)
        {
          return (from bicycle in dbContext.Bicycles
             where bicycle.HubId == hubId
             select bicycle).ToList(); 
        }
        #endregion

        #region Get bicycle type by typeId
        public IEnumerable<Bicycles> GetBicycleTypeId(int typeId)
        {
            return (from bicycle in dbContext.Bicycles
                    where bicycle.TypeId==typeId
                    select bicycle).ToList();
        }
        #endregion

        #region Add new bicycle 
        public async Task<bool> AddBicycle(Bicycles value)
        {
            bool status;
            try
            {
                await dbContext.Bicycles.AddAsync(value);
                await dbContext.SaveChangesAsync();
                status= true;

            }
            catch (Exception)
            {
                status = false;
            }
            return status;
        }
        #endregion

        #region Delete bicycle 
        public async Task<bool> Delete(Bicycles bicycle)
        {
            dbContext.Bicycles.Remove(bicycle);
            await dbContext.SaveChangesAsync();
            return true;
        }
        #endregion

        #region change Isrent by id
        public async Task<bool> Isrent(int id,bool val)
        {
            bool status;
            try
            {
                Bicycles bicycle = dbContext.Bicycles.Where(p => p.BicycleId == id).FirstOrDefault();
                if (bicycle != null)
                {
                    bicycle.IsRent = val;
                    await dbContext.SaveChangesAsync();
                }
                status = true;
            }
            catch (Exception)
            {
                status = false;
            }
            return status;
        }
        #endregion

        #region reserve a bike 
        public async Task<Bicycles> Reserve(BookingDetail value)
        {
            try
            {
                var result =dbContext.Bicycles.Where(b =>
                                              b.TypeId == value.TypeId &&
                                              b.HubId == value.HubId &&
                                              b.IsRent == false
                                              ).FirstOrDefault();
                if (result != null)
                {
                    result.IsRent = true;
                    dbContext.SaveChanges();
                    return result;
                }
                return null;
                
            }
            catch(Exception exp)
            {
                throw new Exception("server error:cannot find bicycle"+exp.Message);
            }

        }
        #endregion

        #region update bicycles

        public async Task<Bicycles> Update(Bicycles bicycle)
        {
            Bicycles result = dbContext.Bicycles.SingleOrDefault(p => p.BicycleId == bicycle.BicycleId);
            if (result != null)
            {
                result.HubId = bicycle.HubId;
                await dbContext.SaveChangesAsync();
            }
            else
            {
                return null;
            }
            return bicycle;
        }
        #endregion

        #region Get bicycle type  detail by bicycle id
        public async Task<BicycleType> GetBicycleType(int BicycleId)
        {
            BicycleType result;
            try
            {

                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync("http://localhost:57207/api/BicycleType/GetBicycleType/" + BicycleId))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        result = (BicycleType)JsonConvert.DeserializeObject(apiResponse, typeof(BicycleType));
                    }
                }
                
            }catch(Exception exp)
            {
                return null;
            }
            return result;

        }

        #endregion


    }
}

