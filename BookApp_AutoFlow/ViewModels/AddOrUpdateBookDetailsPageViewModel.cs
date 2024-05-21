using System.Windows.Input;
using BookApp_AutoFlow.Enums;
using BookApp_AutoFlow.Interfaces;
using BookApp_AutoFlow.Models;
using Debug = System.Diagnostics.Debug;

namespace BookApp_AutoFlow.ViewModels;

public class AddOrUpdateBookDetailsPageViewModel : BaseViewModel
{
    private readonly IPageDialogService _pageDialogs;
    private readonly ISqlLiteDatabase _databaseService;
    private readonly IShellNavigation _shellNavigation;

    public Book Book
    {
        get => Get<Book>();
        set => Set(value);
    }

    public string Title
    {
        get => Get<string>();
        set => Set(value);
    }
    
    public OperationMode OperationMode
    {
        get => Get<OperationMode>();
        set => Set(value);
    }

    public ICommand SubmitCommand { get; set; }

    
    public AddOrUpdateBookDetailsPageViewModel(
        ViewModelContext context,
        IPageDialogService pageDialogs,
        ISqlLiteDatabase databaseService,
        IShellNavigation shellNavigation
        ) : base(context)
    {
        _pageDialogs = pageDialogs;
        _databaseService = databaseService;
        _shellNavigation = shellNavigation;
        SubmitCommand = new Command(async async =>await OnSubmit());
    }

    private async Task OnSubmit()
    {
        if (OperationMode == OperationMode.Create)
        {
            await OnSubmitCreateBook();
        }
        else
        {
            await OnSubmitUpdateBook();
        }
    }

    public override void OnAppearing()
    {
        base.OnAppearing();
        if (OperationMode == OperationMode.Create)
        {
            Book = new Book();
            Title = "Add Book";
        }
        else
        {
            Title = "Update Book";
        }
       
    } 

    private async Task OnSubmitCreateBook()
    {
        try
        {
          var result = await _databaseService.SaveBook(Book);
          if (result)
          {
              await _pageDialogs.DisplayAlert("Success", "Book has been saved", "OK");
              await _shellNavigation.GoToAsync("..");
          }
          else
          {
              await _pageDialogs.DisplayAlert("Woops", "Something went wrong removing this book. Please try again", "Ok");
          }
        }
        catch (Exception e)
        {
            Debug.WriteLine(e.Message);
            await _pageDialogs.DisplayAlert("Woops", "Something went wrong removing this book. Please try again", "Ok");
        }
    }

    private  async Task OnSubmitUpdateBook()
    {
        try
        {
            var result = await _databaseService.UpdateBook(Book);
            if (result)
            {
                await _pageDialogs.DisplayAlert("Success", "Book has been updated", "OK");
                await _shellNavigation.GoToAsync("..");
            }
            else
            {
                await _pageDialogs.DisplayAlert("Woops", "Something went wrong updating this book. Please try again", "OK");
            }
        }
        catch (Exception e)
        {
           await _pageDialogs.DisplayAlert("Woops", "Something went wrong updating this book. Please try again", "OK");
        }
    }
}