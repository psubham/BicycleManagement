using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MapPointApi.model.RepositoryLayer
{
    public class HubRepo
    {
        private readonly HubDbContext _hubDbContext;
        public HubRepo(HubDbContext hubDbContext)
        {
            this._hubDbContext = hubDbContext;
        }

        #region add new hub
        public async Task<bool> AddHub(Hub hub)
        {
            if (AnyNearestHub(hub))
                return false;
            try
            {
               
               await  this._hubDbContext.hubs.AddAsync(hub);
               await this._hubDbContext.SaveChangesAsync();
            }
            catch (Exception exp)
            {
                return false;
            }
            return true;
        }

        #endregion

        #region get all hub
        public IEnumerable<Hub> GetHubs()
        {
            try
            {
                return this._hubDbContext.hubs.ToList();
            }
            catch(Exception exp)
            {
                return null;
            }
            
        }


        #endregion

        #region tangent distance between two gps
        public static double Bearing(Hub pt1, Hub pt2)
        {
            double x = Math.Cos(DegreesToRadians(pt1.Latitude)) * Math.Sin(DegreesToRadians(pt2.Latitude)) - Math.Sin(DegreesToRadians(pt1.Latitude)) * Math.Cos(DegreesToRadians(pt2.Latitude)) * Math.Cos(DegreesToRadians(pt2.Longitude - pt1.Longitude));
            double y = Math.Sin(DegreesToRadians(pt2.Longitude - pt1.Longitude)) * Math.Cos(DegreesToRadians(pt2.Latitude));

            // Math.Atan2 can return negative value, 0 <= output value < 2*PI expected 
            return (Math.Atan2(y, x) + Math.PI * 2) % (Math.PI * 2);
        }

        public static double DegreesToRadians(double angle)
        {
            return angle * Math.PI / 180.0d;
        }

        #endregion

        #region  check if there is any nearest hub near by
        public bool AnyNearestHub(Hub value)
        {
            bool status = false;
            var hubs = this._hubDbContext.hubs.ToList();
            foreach (var temp in hubs)
            {
                if (Bearing(temp, value) < 1.0)
                { status = true;
                    break; }
            }
            //if (status == true)
            //    return true;
            return status;
        }
        #endregion

        #region GetHub
        public async Task<Hub> GetHub(int hubId)
        {
            return await this._hubDbContext.hubs.FindAsync(hubId);
        }
        #endregion

        #region delete hub
        public async Task<bool> DeleteHub(Hub hub)
        {
            try
            {

                this._hubDbContext.hubs.Remove(hub);
                await this._hubDbContext.SaveChangesAsync();
                return true;
            }
            catch(Exception exp)
            {
                throw exp;
            }
        }
        #endregion

        #region update hub
        public async Task<bool> UpdateHub(Hub value)
        {
            try
            {
                var recordExist = this._hubDbContext.hubs.Update(value);

                //recordExist = value;
                //recordExist.Locality = "sp";
                await this._hubDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception exp )
            {
                return false;
            }
            

        }

        #endregion

    }
}
