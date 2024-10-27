using Data;
using Dtos.Stock;
using Helpers;
using Interfaces;
using Mappers;
using Microsoft.AspNetCore.Mvc;

namespace Controllers
{
    [Route("api/stock")] // Route path
    [ApiController] // Api controller attribute
    public class StockController : ControllerBase
    {  
        private readonly ApplicationDBContext _context;
        private readonly IStockRepository _stockRepo;
        public StockController(ApplicationDBContext context, IStockRepository stockRepo)
        {
            _stockRepo = stockRepo;
            _context = context; // our db context, session between database and our app
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            var stocks = await _stockRepo.GetAllAsync(query);
            var stockDto = stocks.Select(s => s.toStockDto());   

            return Ok(stocks); // returns code 200 and data
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            var stock = await _stockRepo.GetByIdAsync(id);
            if(stock == null)
            {
                return NotFound(); // returns 404
            }
            return Ok(stock.toStockDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockRequestDto stockDto)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            var stockModel = stockDto.ToStockFromCreateDTO();
            await _stockRepo.CreateAsync(stockModel);
            return CreatedAtAction(nameof(GetById), new {id = stockModel.Id}, stockModel.toStockDto()); // return 201 created
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateDto)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            var stockModel = await _stockRepo.UpdateAsync(id, updateDto);
            if(stockModel == null)
            {
                return NotFound();
            }

            return Ok(stockModel.toStockDto());
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            var stockModel = await _stockRepo.DeleteAsync(id);
            if(stockModel == null)
            {
                return NotFound();
            } 
            return NoContent(); // 204 status code
        }
    }
}