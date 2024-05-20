using System.ComponentModel.DataAnnotations;
using BookApp_AutoFlow.Models;

namespace BookApp_AutoFlow.ViewModels;

public class BookDetailsPageViewModel : BaseViewModel
{
    public Book Book
    {
        get => Get<Book>();
        set => Set(value);
    }
    public BookDetailsPageViewModel(ViewModelContext context) : base(context)
    {
    }
    
}