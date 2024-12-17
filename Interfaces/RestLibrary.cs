using RestLibrary.Model;

namespace RestLibrary.Interfaces;
    public interface IRestLibrary
    {
        Task<IEnumerable<Book>> GetBooksAsync();
        Task<IEnumerable<Order>> GetOrdersAsync();
        Task AddBookAsync(Book book);
        }

