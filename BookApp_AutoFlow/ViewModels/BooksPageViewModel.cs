using System.Collections.ObjectModel;
using System.Windows.Input;
using BookApp_AutoFlow.Enums;
using BookApp_AutoFlow.Interfaces;
using BookApp_AutoFlow.Models;

namespace BookApp_AutoFlow.ViewModels;

public class BooksPageViewModel : BaseViewModel
{
    private readonly IShellNavigation _shellNavigation;
    private readonly ISqlLiteDatabase _databaseService;
    private readonly IPageDialogService _pageDialogs;

    public ObservableCollection<Book> Books
    {
        get=> Get<ObservableCollection<Book>>();
        set => Set(value);
    }

    public ObservableCollection<Book> FilteredBooks {  
        get=> Get<ObservableCollection<Book>>();
        set => Set(value); }
    public ICommand RemoveBookCommand { get; set; }

    public Book SelectedBook
    {
        get=> Get<Book>();
        set => Set(value);
    }

    public ICommand EditBookCommand { get; set; }
    
    public ICommand AddBookCommand { get; set; }

    public ICommand ItemSelectedCommand { get; set; }
    
    public ICommand PerformSearchCommand { get; set; }

    public BooksPageViewModel(
        ViewModelContext context, 
        IShellNavigation shellNavigation, 
        ISqlLiteDatabase databaseService,
        IPageDialogService pageDialogs) : base(context)
    {
        _shellNavigation = shellNavigation;
        _databaseService = databaseService;
        _pageDialogs = pageDialogs;
        EditBookCommand = new Command<Book>(async (book) => await EditBookDetails(book));
        RemoveBookCommand = new Command<Book>(async (book) => await RemoveBook(book));
        ItemSelectedCommand = new Command(async (book) => await NavigateToBookDetails());
        PerformSearchCommand = new Command<string>( (searchText) =>  PerformSearchBooks(searchText));
        AddBookCommand = new Command( async () => await AddBook());
    }

    public override async void OnAppearing()
    {
        base.OnAppearing();
        SelectedBook = null; 
        try
        {
            var fetchedBooks = await _databaseService.GetBooks();
            Books = new ObservableCollection<Book>(fetchedBooks);
            FilteredBooks = Books;
        }
        catch (Exception e)
        {
            await _pageDialogs.DisplayAlert("Woops", "Something went wrong when fetching ", "Ok");
        }
    }

    public void PerformSearchBooks(string searchText)
    {
        if (string.IsNullOrEmpty(searchText))
        {
            FilteredBooks = Books;
        }
        else
        {
            var newlyFilteredLit = Books.Where(book =>
                book.Title.ToLower().Contains(searchText));
            FilteredBooks = new ObservableCollection<Book>(newlyFilteredLit);
        }
    }

    public async Task EditBookDetails(Book bookToEdit)
    {
        var navigationParameter = new ShellNavigationQueryParameters
        {
            {NavigationParametersConstants.UpdateBookDetailsPageNavigationParameters.OperationMode, OperationMode.Edit },
            {NavigationParametersConstants.UpdateBookDetailsPageNavigationParameters.Book, bookToEdit }
        };
        await _shellNavigation.GoToAsync("AddOrUpdateBookDetailsPage", navigationParameter);
    }

    public async Task AddBook()
    {
        var navigationParameter = new ShellNavigationQueryParameters
        {
            {NavigationParametersConstants.UpdateBookDetailsPageNavigationParameters.OperationMode, OperationMode.Create }
        };
        
        await _shellNavigation.GoToAsync("AddOrUpdateBookDetailsPage",navigationParameter);    
    }

    public async Task RemoveBook(Book bookToRemove)
    {
        try
        {
          var result =  await _databaseService.DeleteBook(bookToRemove.Id);
          if (result)
          {
              Books.Remove(bookToRemove);
          }
          else
          {
              await _pageDialogs.DisplayAlert("Woops", "Something went wrong removing this book. Please try again", "Ok");
          }
        }
        catch (Exception e)
        {
            await _pageDialogs.DisplayAlert("Woops", "Something went wrong removing this book. Please try again", "Ok");
        }
    }

    private async Task NavigateToBookDetails()
    {
        if (SelectedBook != null)
        {
            var navigationParameter = new ShellNavigationQueryParameters
            {
                {NavigationParametersConstants.BookDetailsPageNavigationParameters.Book, SelectedBook }
            };
            await _shellNavigation.GoToAsync("BookDetails", navigationParameter);
        }
    }
}