using System.ComponentModel.DataAnnotations;
using SQLite;

namespace BookApp_AutoFlow.Models;

public class Book
{
    [PrimaryKey, AutoIncrement] 
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; }
    public string Author { get; set; }
    public string Description { get; set; }
    public string Genre { get; set; }
    public int PublicationYear  { get; set; }
}