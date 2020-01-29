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

    public class BookingRepository
    {
        private BicycleDbContext _db;
        public BookingRepository(BicycleDbContext obj)
        {
            _db = obj;
        }

        #region get all  booking detail

        public IEnumerable< Booking> GetBooking()
        {
            return this._db.Bookings.ToList();
        }
        #endregion

        #region book bicycle
        public async Task<Booking> Book(BookingDetail value)
        {

            Bicycles result;
            bool flag;
            try
            {

                // check bicycel is there
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.PostAsJsonAsync("http://localhost:57207/api/Bicycle/reserve/", value))
                    {
                        if(response.IsSuccessStatusCode==true)
                        {

                            string apiResponse = await response.Content.ReadAsStringAsync();
                            result = (Bicycles)JsonConvert.DeserializeObject(apiResponse, typeof(Bicycles));
                        }
                        else
                        {
                            throw new Exception("bicycle is not there");
                        }
                    }
                }

            }
            catch (Exception exp)
            {
                throw exp;
            }
                                 
            Booking bookingObj = new Booking() {
                BicycleId = result.BicycleId,
                DateTime = DateTime.Now,
                UserName = value.UserName,
                BicycleNumber = result.BicycleNumber,
                DeliveryAddress = value.DeliveryAddress,
                Latitude = value.Latitude,
                Longitude = value.Longitude,
                Active=true
             };
            
            try
            {
                _db.Bookings.Add(bookingObj);
                await _db.SaveChangesAsync();

            }
            catch (Exception exp)
            {
                throw exp;
            }
            var bookingResult = _db.Bookings.Where(b =>
                                 b.BicycleId == result.BicycleId && b.Active==true
                                   ).FirstOrDefault();

            try
            {
                /// for updating delivery databse
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.PostAsJsonAsync("http://localhost:64966/api/DeliveryBike/AddDelivery", bookingResult.BookingId)) 
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        flag = (bool)JsonConvert.DeserializeObject(apiResponse, typeof(bool));
                    }
                }

            }
            catch (Exception exp)
            {
                throw exp;
            }
            if (flag == true)
                return bookingResult;
            else
                throw new Exception("Cannot set delivery");
        }
        #endregion



        #region get bicycle based on id
        public async Task<Booking> GetDetail(int id)
        {
            var res=await _db.Bookings.FindAsync(id);
            //var res = _db.Bookings.Where(b =>
            //                       b.BookingId == id
            //                       ).FirstOrDefault();
            return res;
        }
        #endregion

        #region        get bicycle based on username
        public IEnumerable<Booking>  GetDetail(string userName)
        {
            return this._db.Bookings.Where(booking => booking.UserName.Equals(userName)).ToList();
        }
        #endregion

        #region get active booking by user
        public Booking isActive(string userName)
        {
            return this._db.Bookings
                .Where(booking => booking.UserName.Equals(userName) 
                                    && booking.Active==true)
                .FirstOrDefault();
        }
        #endregion
    }
}
