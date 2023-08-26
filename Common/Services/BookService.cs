using AutoMapper;
using LibraryManagmentAPI.Common.BaseResponse;
using LibraryManagmentAPI.Common.DTOs;
using LibraryManagmentAPI.Common.IServices;
using LibraryManagmentAPI.Domain.Entities;
using LibraryManagmentAPI.Infrastructure.UnitOfWork;
using System.Linq;

namespace LibraryManagmentAPI.Common.Services
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public BookService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<BaseCommandResponse> AddBookAsync(CreateBookDTO book)
        {
            var response = new BaseCommandResponse();

            var existedBooks = await unitOfWork.Books.GetAsync(s => s.Title == book.Title && s.Author == book.Author);
            if (existedBooks != null)
            {
                response.Success = false;
                response.Message = "This Book is already exist!";
                return response;
            }
            var bookDTO = mapper.Map<Book>(book);
            await unitOfWork.Books.AddAsync(bookDTO);
            await unitOfWork.Save();

            response.Success = true;
            response.Message = "Book Added Successfuly";
            response.Data = bookDTO;
            return response;
        }

        public async Task DeleteBookAsync(int id)
        {
            await unitOfWork.Books.RemoveAsync(id);
            await unitOfWork.Save();
        }



        public async Task<IList<BookDTO>> GetAllBooksAsync()
        {
            var books = await unitOfWork.Books.GetAllAsync();
            //var groupedBooks = books.GroupBy(x => x.Title).SelectMany(group => group).ToList();
            var result = mapper.Map<IList<BookDTO>>(books);
            return result;
        }

        public async Task<BookDTO> GetBookAsync(int id)
        {
            var book = await unitOfWork.Books.GetAsync(x => x.Id == id);
            var result = mapper.Map<BookDTO>(book);
            return result;
        }

        public async Task UpdateBookAsync(UpdateBookDTO bookDTO)
        {
            var book = mapper.Map<Book>(bookDTO);
            await unitOfWork.Books.UpdateAsync(book);
            await unitOfWork.Save();
        }
    }
}
