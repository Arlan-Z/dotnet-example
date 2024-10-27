using Dtos.Comment;
using Interfaces;
using Mappers;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepo;
        private readonly IStockRepository _stockRepo;
        public CommentController(ICommentRepository commentRepository, IStockRepository stockRepository)
        {
            _commentRepo = commentRepository;
            _stockRepo = stockRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if(!ModelState.IsValid) return BadRequest(ModelState); // inherited from ControllerBase
            var comments = await _commentRepo.GetAllAsync();
            var commentDto = comments.Select(s => s.toCommentDto());
            return Ok(commentDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            var comment = await _commentRepo.GetByIdAsync(id);
            if(comment == null) return NotFound();
            return Ok(comment.toCommentDto());
        }

        [HttpPost("{stockId:int}")]
        public async Task<IActionResult> Create([FromRoute] int stockId, CreateCommentRequestDto commentDto)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            if(!await _stockRepo.StockExists(stockId)) return BadRequest("Stock Does Not Exist");

            var commentModel = commentDto.toCommentFromCreateDto(stockId);
            await _commentRepo.CreateAsync(commentModel);
            return CreatedAtAction(nameof(GetById), new {id = commentModel.Id}, commentModel.toCommentDto()); 
            // nameof(GetById) url to get created Comment
            // new {id = commentModel} url setting
            // commentModel.toCommentDto() return created comment
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCommentRequestDto updateDto)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            var comment = await _commentRepo.UpdateAsync(id, updateDto.toCommentFromUpdateDto());
            if(comment == null) return NotFound("Comment Not Found");
            return Ok(comment.toCommentDto());

        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            var commentModel = await _commentRepo.DeleteAsync(id);
            if(commentModel == null) return NotFound("Comment Does Not Exist ;)");
            return Ok(commentModel);
        }
    }
}