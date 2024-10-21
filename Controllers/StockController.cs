using Data;
using Microsoft.AspNetCore.Mvc;

namespace Controllers
{
    [Route("api/stock")] // Route path
    [ApiController] // Api controller attribute
    public class StockController : ControllerBase
    {  
        private readonly ApplicationDBContext _context;
        public StockController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var stocks = _context.Stocks.ToList();
            return Ok(stocks); // returns code 200 and data
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var stock = _context.Stocks.Find(id);
            if(stock == null)
            {
                return NotFound(); // returns 404
            }
            return Ok(stock);
        }
    }
}