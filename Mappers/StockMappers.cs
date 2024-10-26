using Dtos.Stock;
using Models;

namespace Mappers
{
    public static class StockMappers
    {
        public static StockDto toStockDto(this Stock stockModel)
        {
            return new StockDto
            {
                Id = stockModel.Id,
                Symbol = stockModel.Symbol,
                CompanyName = stockModel.CompanyName,
                Purchase = stockModel.Purchase,
                LastDiv = stockModel.LastDiv,
                industry = stockModel.industry,
                MarketCap = stockModel.MarketCap,
            };  
        }

        public static Stock ToStockFromCreateDTO(this CreateStockRequestDto stockDto)
        {
          return new Stock
          {
            Symbol = stockDto.Symbol,
            CompanyName = stockDto.CompanyName,
            Purchase = stockDto.Purchase,
            LastDiv = stockDto.LastDiv,
            industry = stockDto.industry,
            MarketCap = stockDto.MarketCap,
          };      
        }
    }
}