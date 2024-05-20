using BookApp_AutoFlow.Interfaces;

namespace BookApp_AutoFlow.Services;

public class ShellNavigation : IShellNavigation
{
    public async Task GoToAsync(string route)
    {
        await Shell.Current.GoToAsync(route);
    }

    public async Task GoToAsync(string route, bool animated)
    {
       await Shell.Current.GoToAsync(route, animated);
    }

    public async Task GoToAsync(string route, ShellNavigationQueryParameters parameters)
    {
        await Shell.Current.GoToAsync(route,parameters);
    }
    

    public async Task GoToAsync(string route, bool animated, IDictionary<string, object> parameters)
    {
        await Shell.Current.GoToAsync(route, animated,parameters);
    }
}