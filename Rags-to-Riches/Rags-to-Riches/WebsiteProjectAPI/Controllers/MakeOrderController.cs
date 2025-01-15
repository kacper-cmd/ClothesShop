using Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebsiteProjectAPI.Models.BussinessLogic;

namespace WebsiteProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MakeOrderController : ControllerBase
    {
        private readonly RagsToRichesDbContext _context;
        public MakeOrderController(RagsToRichesDbContext context)
        {
            _context = context;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> MakeOrderFromCurrentCart(string id)
        {
            try
            {

                var cartB = new CartB(_context);
                Order order;
                try
                {
                    order = await cartB.MakeOrderFromCurrentCart(id);
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }


                return order;
            }
            catch (Exception e)
            {

                throw;
            }
        }
    }
}
