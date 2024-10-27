using Data;
using Dtos.Stock;
using Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

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
        public async Task<IActionResult> GetAll()
        {
            var stocks = await _context.Stocks.ToListAsync();
            var stocDto = stocks.Select(s => s.toStockDto());   

            return Ok(stocks); // returns code 200 and data
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var stock = await _context.Stocks.FindAsync(id);
            if(stock == null)
            {
                return NotFound(); // returns 404
            }
            return Ok(stock.toStockDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockRequestDto stockDto)
        {
            var stockModel = stockDto.ToStockFromCreateDTO();
            await _context.Stocks.AddAsync(stockModel);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new {id = stockModel.Id}, stockModel.toStockDto()); // return 201 created
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateDto)
        {
            var stockModel = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);
            if(stockModel == null)
            {
                return NotFound();
            }

            stockModel.Symbol = updateDto.Symbol;
            stockModel.industry = updateDto.industry;
            stockModel.Purchase = updateDto.Purchase;
            stockModel.LastDiv = updateDto.LastDiv;
            stockModel.MarketCap = updateDto.MarketCap;
            stockModel.CompanyName = updateDto.CompanyName;

            await _context.SaveChangesAsync();
            return Ok(stockModel.toStockDto());
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var stockModel = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);
            if(stockModel == null)
            {
                return NotFound();
            } 
            _context.Stocks.Remove(stockModel); // Remove is not async function, because it just mark object as toBeDeleted in a context
            await _context.SaveChangesAsync();
            return NoContent(); // 204 status code
        }
    }
}