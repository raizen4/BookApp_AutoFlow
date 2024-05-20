using System.Windows.Input;
using BookApp_AutoFlow.Enums;
using BookApp_AutoFlow.Models;

namespace BookApp_AutoFlow.ViewModels;

public class AddOrUpdateBookDetailsPageViewModel : BaseViewModel
{
    public Book? Book
    {
        get => Get<Book>();
        set => Set(value);
    }
    
    public OperationMode OperationMode
    {
        get => Get<OperationMode>();
        set => Set(value);
    }

    public ICommand SubmitCommand { get; set; }
    
    public AddOrUpdateBookDetailsPageViewModel(ViewModelContext context) : base(context)
    {
        SubmitCommand = OperationMode == OperationMode.Create ? new Command<Book>(OnSubmitSaveForm) : new Command<Book>(OnSubmitUpdateForm);
       
    }

    public override void OnResume()
    {
        base.OnResume();
        if (OperationMode == OperationMode.Create)
        {
            Book = new Book();
        }
        Book = new Book();
    }

    private void OnSubmitSaveForm(Book obj)
    {
        
    }

    private void OnSubmitUpdateForm(Book book)
    {
        
    }
}