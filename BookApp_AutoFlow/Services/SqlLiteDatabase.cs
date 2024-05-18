using BookApp_AutoFlow.Interfaces;
using BookApp_AutoFlow.Models;
using SQLite;

namespace BookApp_AutoFlow.Services;

public class SqlLiteDatabase: ISqlLiteDatabase
{
    private readonly SQLiteAsyncConnection _database;
    
    public SqlLiteDatabase()
    {
        _database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        _database.CreateTableAsync<Book>();
    }
    
    public async Task<bool> SaveBook(Book bookToSave)
    {
        try
        {
            var res = await _database.InsertAsync(bookToSave);
            return res > 0;
        }
        catch (Exception e)
        {
             throw new Exception("Error while saving data to database", e);
        }
    }

    public async Task<List<Book>> GetBooks()
    {
        try
        {
            var result = await _database.Table<Book>().ToListAsync();
            return result;
        }
        catch (Exception e)
        {
            throw new Exception("Error while fetching data from database", e);
        }
    }

    public async Task<bool> DeleteBook(int bookId)
    {
        try
        {
            var res = await _database.Table<Book>().DeleteAsync(x => x.Id.Equals(bookId));
            return res > 0;
        }
        catch (Exception e)
        {
            throw new Exception("Error while deleting data from database", e);
        }
    }

    public async Task<bool> UpdateBook(Book bookToUpdate)
    {
       try
       {
           var res = await _database.InsertAsync(bookToUpdate);
           return res > 0;
       }
       catch (Exception e)
       {
           throw new Exception("Error while updating data in database", e);
       }
    }
}