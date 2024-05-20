using System.Collections.ObjectModel;
using System.Windows.Input;
using BookApp_AutoFlow.Enums;
using BookApp_AutoFlow.Interfaces;
using BookApp_AutoFlow.Models;
using BookApp_AutoFlow.Views;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Views;

namespace BookApp_AutoFlow.ViewModels;

public class BooksPageViewModel : BaseViewModel
{
    private readonly IShellNavigation _shellNavigation;
    private readonly ISqlLiteDatabase _databaseService;
    private readonly IPageDialogs _pageDialogs;
    public ObservableCollection<Book> Books { get; set; }
    public ICommand NavigateToBookDetailsCommand { get; set; }
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
        IPageDialogs pageDialogs,
        IPopupService popupService) : base(context)
    {
        _shellNavigation = shellNavigation;
        _databaseService = databaseService;
        _pageDialogs = pageDialogs;
        Books = new ObservableCollection<Book>()
        {
            new Book {Title = "Book 1", Author = "Author 1", Description = "Description 1", PublicationYear = 2021},
            new Book {Title = "Book 2", Author = "Author 2", Description = "Description 2", PublicationYear = 2022},
            new Book {Title = "Book 3", Author = "Author 3", Description = "Description 3", PublicationYear = 2023},
        };
        EditBookCommand = new Command<Book>(async (book) => await EditBookDetails(book));
        RemoveBookCommand = new Command<Book>(async (book) => await RemoveBook(book));
        ItemSelectedCommand = new Command<Book>(async (book) => await NavigateToBookDetails(book));
        PerformSearchCommand = new Command<string>( (searchText) =>  PerformSearchBooks(searchText));
        AddBookCommand = new Command( async () => await AddBook());
    }

    public override async void OnResume()
    {
        base.OnResume();
        try
        {
            var fetchedBooks = await _databaseService.GetBooks();
            Books = new ObservableCollection<Book>(fetchedBooks);
        }
        catch (Exception e)
        {
           await _pageDialogs.DisplayAlert("Woops", "Something went wrong", "Ok");
        }
    }

    private void PerformSearchBooks(string searchText)
    {
        var filteredBooks = Books.Where(book =>
            book.Title.ToLower().Contains(searchText));
        Books = new ObservableCollection<Book>(filteredBooks);
    }

    private async Task EditBookDetails(Book bookToEdit)
    {
        var navigationParameter = new ShellNavigationQueryParameters
        {
            {NavigationParametersConstants.UpdateBookDetailsPageNavigationParameters.OperationMode, OperationMode.Edit },
            {NavigationParametersConstants.UpdateBookDetailsPageNavigationParameters.Book, bookToEdit }
        };
        await _shellNavigation.GoToAsync("AddOrUpdateBookDetailsPage", navigationParameter);
    }
    
    private async Task AddBook()
    {
        var navigationParameter = new ShellNavigationQueryParameters
        {
            {NavigationParametersConstants.UpdateBookDetailsPageNavigationParameters.OperationMode, OperationMode.Create }
        };
        
        await _shellNavigation.GoToAsync("AddOrUpdateBookDetailsPage", navigationParameter);    
    }

    private async Task RemoveBook(Book bookToRemove)
    {
        try
        {
            Books.Remove(bookToRemove);  
        }
        catch (Exception e)
        {
            await _pageDialogs.DisplayAlert("Woops", "Something went wrong removing this book. Please try again", "Ok");
        }
    }

    private async Task NavigateToBookDetails(Book bookPressed)
    {
        if (bookPressed != null)
        {
            var navigationParameter = new ShellNavigationQueryParameters
            {
                {NavigationParametersConstants.BookDetailsPageNavigationParameters.Book, bookPressed }
            };
            await _shellNavigation.GoToAsync("BookDetails", navigationParameter);
        }
    }
}