using BicycleApp.Data.model;
using DeliverPage.Exceptions;
using DeliverPage.Model;
using DeliverPage.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliverPage.Services
{
    public class DeliveryService
    {
        DeliveryRepo _deliveryRepo;
        public DeliveryService(DeliveryRepo deliveryRepo)
        {
            this._deliveryRepo = deliveryRepo;
        }

        #region Add delivery
        public async Task<Object> AddDelivery(int BookingId)
        {
            var result = await this._deliveryRepo.AddDelivery(BookingId);
            if (result ==false)
            {
                throw new DetailsNotFound("Cannot get details");
            }
            return result;
        }
        #endregion

        #region get delivery details
        public IEnumerable<Delivery> GetDeliveries()
        {
            return this._deliveryRepo.GetDeliveries();

        }
        #endregion

        #region delivery detail by deliveryid
        public async Task<Delivery> GetDelievry(int deliveryId)
        {
            var recordExist = await this._deliveryRepo.GetDetail(deliveryId);
            if (recordExist == null)
            {
                throw new DetailsNotFound("detail not present");
            }
            return recordExist;
        }
        #endregion

        #region change status
        public async Task<bool> ChangeStatus(string Status,int DeliverId)
        {
            Delivery recordExist = await this._deliveryRepo.GetDetail(DeliverId);
            if (recordExist == null)
            {
                throw new DetailsNotFound("detail not present " + DeliverId);
            }
            if(recordExist.ConfirmationTime.Equals(default(DateTime))&& Status.Equals("confirm"))
            {
                recordExist.ConfirmationTime = DateTime.Now;
                
            }else if (recordExist.DeliverTime.Equals(default(DateTime)) && Status.Equals("deliver"))
            {
                if (recordExist.ConfirmationTime.Equals(default(DateTime)))
                {
                    throw new UpdateException("cannot set delivery time which is yet not confirm");
                }
                recordExist.DeliverTime = DateTime.Now;
            }
            else if(Status.Equals("cancel"))
            {
                if (recordExist.DeliverTime.Equals(default(DateTime)) || recordExist.ConfirmationTime.Equals(default(DateTime)))
                    recordExist.Cancelationtime = DateTime.Now;
                else
                {
                    throw new UpdateException("cannot cancel delivered cycle");
                }
            }
            recordExist.Status = Status;
            var res = await this._deliveryRepo.update(recordExist);
            if(res==false)
            {
                return false;
            }
            return true;
        }
        #endregion

        #region bike will deliver or not Confirmation
        public string IsConfirm(int BookingId)
        {
            string result;
            Delivery recordExist =  this._deliveryRepo.getDeleveryBookingId(BookingId);
            if (recordExist == null)
            {
                throw new DetailsNotFound("detail not present " + BookingId);
            }
            if (!recordExist.Cancelationtime.Equals(default(DateTime)))
                result= "Cancel";
            else if (!recordExist.ConfirmationTime.Equals(default(DateTime)))
                result= "Rajesh";
            else
                result= "Init";
            return JsonConvert.SerializeObject(result);
        }
        #endregion

      
    }
}
