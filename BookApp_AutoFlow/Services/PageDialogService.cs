using BookApp_AutoFlow.Interfaces;

namespace BookApp_AutoFlow.Services;

public class PageDialogService : IPageDialogService
{
    public async Task DisplayAlert(string title, string message, string cancel)
    {
       await Shell.Current.DisplayAlert(title, message, cancel);
    }

    public async Task DisplayAlert(string title, string message, string accept, string cancel)
    {
        await Shell.Current.DisplayAlert(title, message, accept, cancel);
    }

    public async Task DisplayAlert(string title, string message, string accept, string cancel, FlowDirection flowDirection)
    {
        await Shell.Current.DisplayAlert(title, message, accept, cancel, flowDirection);
    }

    public async Task DisplayAlert(string title, string message, string cancel, FlowDirection flowDirection)
    {
        await Shell.Current.DisplayAlert(title, message, cancel, flowDirection);
    }
}