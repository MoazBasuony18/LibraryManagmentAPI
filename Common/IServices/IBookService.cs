using LibraryManagmentAPI.Common.BaseResponse;
using LibraryManagmentAPI.Common.DTOs;


namespace LibraryManagmentAPI.Common.IServices
{
    public interface IBookService
    {
        Task<IList<BookDTO>> GetAllBooksAsync();
        Task<BookDTO> GetBookAsync(int id);

        Task UpdateBookAsync(UpdateBookDTO book);
        Task DeleteBookAsync(int id);
        Task<BaseCommandResponse> AddBookAsync(CreateBookDTO bookDTO);
    }
}
