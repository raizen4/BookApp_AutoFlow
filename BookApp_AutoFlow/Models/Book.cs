using SQLite;

namespace BookApp_AutoFlow.Models;

public class Book
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string Description { get; set; }
    public int PublicationYear  { get; set; }
}