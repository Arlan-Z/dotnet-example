using Data;
using Dtos.Stock;
using Interfaces;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Repository
{
    public class StockRepository : IStockRepository
    {  
        private readonly ApplicationDBContext _context;
        public StockRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Stock> CreateAsync(Stock stockModel)
        {
            await _context.Stocks.AddAsync(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }

        public async Task<Stock?> DeleteAsync(int id)
        {
            var stockModel = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);
            if(stockModel == null) return null;
            _context.Stocks.Remove(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }

        public async Task<List<Stock>> GetAllAsync()
        {
            return await _context.Stocks.Include(c => c.Comments).ToListAsync();
        }

        public async Task<Stock?> GetByIdAsync(int id)
        {
            return await _context.Stocks.Include(c => c.Comments).FirstOrDefaultAsync(i => i.Id == id);    
        }

        public Task<bool> StockExists(int id)
        {
            return _context.Stocks.AnyAsync(s => s.Id == id); 
        }

        public async Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto stockDto)
        {   
            var existingStock = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);
            if(existingStock == null) return null;
            
            existingStock.Symbol = stockDto.Symbol;
            existingStock.industry = stockDto.industry;
            existingStock.Purchase = stockDto.Purchase;
            existingStock.LastDiv = stockDto.LastDiv;
            existingStock.MarketCap = stockDto.MarketCap;
            existingStock.CompanyName = stockDto.CompanyName;

            await _context.SaveChangesAsync();
            return existingStock;
        }
    }
}