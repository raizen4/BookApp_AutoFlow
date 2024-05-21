using BookApp_AutoFlow.Models;

namespace BookApp_AutoFlow.Interfaces;

public interface ISqlLiteDatabase
{
    Task<bool> SaveBook(Book bookToSave);
    Task<List<Book>> GetBooks();
    Task<bool> DeleteBook(Guid bookId);
    Task<bool> UpdateBook(Book bookToUpdate);
}