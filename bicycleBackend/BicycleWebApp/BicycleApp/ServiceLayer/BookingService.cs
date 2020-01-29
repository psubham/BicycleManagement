using BicycleApp.Data.model;
using BicycleApp.Data.RepositoryLayer;
using BicycleApp.UserException;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BicycleApp.ServiceLayer
{
    
    public class BookingService
    {
        private BookingRepository _bookingRepository;
        public BookingService(BookingRepository bookingReposiroty)
        {
            this._bookingRepository = bookingReposiroty;
        }

        #region add a booking detail
        public async Task<Booking> Book(BookingDetail value)
        {
            try
            {
                return await this._bookingRepository.Book(value);
            }catch(Exception exp)
            {
                throw exp;
            }

        }
        #endregion


        #region get boooking detail by id
        public async Task<Booking> GetDetail(int id)
        {
            return await this._bookingRepository.GetDetail(id);
        }
        #endregion


        #region get all booking detail
        public IEnumerable<Booking> GetBookings()
        {
            return this._bookingRepository.GetBooking();
        }
        #endregion

        #region get booking detail based on username
        public IEnumerable< Booking> GetDetail(string userName)
        {
            var recordExist = this._bookingRepository.GetDetail(userName);
            if (recordExist == null)
            {
                throw new DetailsNotFound("no booking");
            }
            return recordExist;
        }
        #endregion

        #region get active booking by user
        public Booking isActive(string userName)
        {
            var recordExist = this._bookingRepository.isActive(userName);
            return recordExist;
            //if (recordExist == null)
            //    return false;
            //return true;
        }
        #endregion



    }
}
