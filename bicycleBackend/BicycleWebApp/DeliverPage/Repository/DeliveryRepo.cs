using BicycleApp.Data.model;
using DeliverPage.Data;
using DeliverPage.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace DeliverPage.Repository
{
    public class DeliveryRepo
    {
        DeliveryDbContext db;
        public DeliveryRepo(DeliveryDbContext _db)
        {
            this.db = _db;
        }

        //HttpClient _http;
        dynamic result = "";


        #region Add delivery detail
        public async Task<bool> AddDelivery(int BookingId)
        {
            //List<Customer> reservationList = new List<Customer>();
            using (var httpClient = new HttpClient())
            {
                try
                {
                    using (var response = await httpClient.PostAsJsonAsync("http://localhost:57207/api/Booking/GetDetail", BookingId))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        //reservationList = apiResponse;
                        //reservationList = JsonConvert.DeserializeObject<List<Customer>>(apiResponse);
                        result = (Booking)JsonConvert.DeserializeObject(apiResponse, typeof(Booking));
                        if (result == null)
                        {
                            throw new Exception("Exception in fetching");
                        }
                        Delivery deliveryobj = new Delivery()
                        {
                            BookingId = result.BookingId,
                            BicycleId = result.BicycleId,
                            UserName = result.UserName,
                            DateTime = result.DateTime,
                            BicycleNumber = result.BicycleNumber,
                            DeliveryAddress = result.DeliveryAddress,
                            Deliverylat = result.Latitude,
                            Deliverylng = result.Longitude,
                            OrderTime = DateTime.Now,
                            DeliverTime = default(DateTime),
                            Cancelationtime = default(DateTime),
                            ConfirmationTime = default(DateTime),
                            Status = "Intial",
                            DeliveryUserName=""
                            
                        };
                        
                        await db.delivery.AddAsync(deliveryobj);
                        await db.SaveChangesAsync();
                    }
                    return true;
                }
                catch (Exception exp)
                {
                    throw exp;
                }
            }
        }

        #endregion

        #region Get Delivery details
        public IEnumerable<Delivery> GetDeliveries()
        {
            return (from delivery in db.delivery
                    where delivery.Status == "Intial"
                    select delivery).ToList() ;
        }
        #endregion

        #region get delivery details by delivery id
        public async Task<Delivery> GetDetail(int DeliveryId )
        {
            return await this.db.delivery.FindAsync(DeliveryId);
        }

        #endregion

        #region update delivery detail
        public async Task<bool> update(Delivery value)
        {
            var recordExist=this.db.delivery.SingleOrDefault(delivery=>delivery.DeliveryId==value.DeliveryId);
            if (recordExist == null)
            {
                return false;
            }
            recordExist = value;
            await this.db.SaveChangesAsync();
            return true;

        }
        #endregion

        #region get delivery detail based on booking Id
        public Delivery getDeleveryBookingId(int BookingId)
        {
            return (from delivery in db.delivery
                    where delivery.BookingId == BookingId
                    select delivery).FirstOrDefault();
        }
        #endregion


    }
}
