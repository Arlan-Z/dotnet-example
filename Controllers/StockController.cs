using Data;
using Dtos.Stock;
using Mappers;
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
            _context = context; // our db context, session between database and our app
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var stocks = _context.Stocks.ToList()
            .Select(s => s.toStockDto());   

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
            return Ok(stock.toStockDto());
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateStockRequestDto stockDto)
        {
            var stockModel = stockDto.ToStockFromCreateDTO();
            _context.Stocks.Add(stockModel);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new {id = stockModel.Id}, stockModel.toStockDto()); // return 201 created
        }
    }
}