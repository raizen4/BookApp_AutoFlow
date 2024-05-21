using BookApp_AutoFlow.Models;

namespace BookApp_AutoFlow.ViewModels;

public class BookDetailsPageViewModel(ViewModelContext context) : BaseViewModel(context)
{
    public Book Book
    {
        get => Get<Book>();
        set => Set(value);
    }
}