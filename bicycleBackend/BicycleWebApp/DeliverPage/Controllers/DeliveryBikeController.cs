using System.Collections.Generic;
using System.Threading.Tasks;
using DeliverPage.Exceptions;
using DeliverPage.Model;
using DeliverPage.Services;
using Microsoft.AspNetCore.Mvc;

namespace DeliverPage.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ExceptionHandler]
    public class DeliveryBikeController : ControllerBase
    {
        
        private readonly DeliveryService _deliveryService;
        public DeliveryBikeController(DeliveryService deliveryService)
        {
            this._deliveryService = deliveryService;
        }

        #region get deleveries
        [HttpGet("GetDeliveries")]
        public IEnumerable<Delivery> GetDeliveries()
        {
            return _deliveryService.GetDeliveries();
        }
        #endregion

        #region add delevery
        [HttpPost("AddDelivery")]
        public async Task<IActionResult> AddDelivery([FromBody] int BookingId)
        {
            object detailsExist = await _deliveryService.AddDelivery(BookingId);
            return new OkObjectResult(detailsExist);
        }
        #endregion

        #region get delevery by id
        [HttpPost("GetDelivery")]
        public async Task<Delivery> GetDelivery([FromBody]int DeliverId)
        {
            return await this._deliveryService.GetDelievry(DeliverId);
        }
        #endregion

        #region change status
        [HttpPut("ChangeStatus/{id}")]
        public async Task<bool> ChangeStatus( [FromBody]string Status,int id)
        {
            return await this._deliveryService.ChangeStatus(Status, id);
        }
        #endregion

        #region delivery is confirm
        [HttpGet("IsConfirm/{BookingId}")]
        public ActionResult<string> IsConfirm(int BookingId)
        {
            var res = this._deliveryService.IsConfirm(BookingId);
            return Ok(res);
        }
        #endregion

        #region delete by id
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        #endregion

    }
}
