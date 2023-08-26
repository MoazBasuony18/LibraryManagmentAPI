using LibraryManagmentAPI.Common.BaseResponse;
using LibraryManagmentAPI.Common.DTOs;
using LibraryManagmentAPI.Common.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagmentAPI.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService bookService;

        public BookController(IBookService bookService)
        {
            this.bookService = bookService;
        }

        [HttpGet("getAllBooks")]
        public async Task<IActionResult> GetAllBooks()
        {
            var books = await bookService.GetAllBooksAsync();
            return Ok(books);
        }

        [HttpPost("AddBook")]
        public async Task<ActionResult<BaseCommandResponse>> AddBook([FromBody] CreateBookDTO bookDTO)
        {
            await bookService.AddBookAsync(bookDTO);
            return Ok();
        }

        [HttpGet("getBookById/{id}")]
        public async Task<IActionResult> GetBookById(int id)
        {
            var book = await bookService.GetBookAsync(id);
            return Ok(book);
        }
        [HttpPut("updateBook/{id}")]
        public async Task<IActionResult> UpdateBook([FromBody] UpdateBookDTO bookDTO,int id)
        {
            if (id != bookDTO.Id)
            {
                return BadRequest();
            }
            await bookService.UpdateBookAsync(bookDTO);
            return NoContent();
        }

        [HttpDelete("deleteBook/{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            await bookService.DeleteBookAsync(id);
            return NoContent();
        }

    }
}
