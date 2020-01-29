using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BicycleApp.Data.DataAccessLayer;
using BicycleApp.Data.model;
using BicycleApp.ServiceLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BicycleApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly BookingService bookingService;
        public BookingController(BookingService bookingService)
        {
            this.bookingService = bookingService;
        }

        #region get all booking
        // GET: api/booking
        [HttpGet]
        public IEnumerable<Booking> Get()
        {
            return this.bookingService.GetBookings();
        }
        #endregion

        #region get delevery detail by id
        [HttpPost("GetDetail")]
        public async Task<Booking> GetDetail([FromBody] int BookingId)
        {
            return await this.bookingService.GetDetail(BookingId);
        }
        #endregion

        #region get delevery detail by username
        [HttpPost("GetDetailOfUser")]
        public  IEnumerable<Booking> GetDetailOfUser([FromBody] string UserName)
        {
            return  this.bookingService.GetDetail(UserName);
        }
        #endregion

        #region check for active ride
        [HttpPost("ActiveRide")]
        public Booking ActiveRide([FromBody]string UserName)
        {
           return this.bookingService.isActive(UserName);
        }
        #endregion

        #region book a bike
        [HttpPost]
        public  async Task<Booking> Post([FromBody] BookingDetail value)
        {
            return await this.bookingService.Book(value);
        }
        #endregion

    }
}
