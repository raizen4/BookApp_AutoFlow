namespace BookApp_AutoFlow.Interfaces;

public interface IShellNavigation 
{
    Task GoToAsync(string route);
    Task GoToAsync(string route, bool animated);
    Task GoToAsync(string route, ShellNavigationQueryParameters parameters);
    Task GoToAsync(string route, bool animated, IDictionary<string,object> parameters);
}