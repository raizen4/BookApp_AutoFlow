using BookApp_AutoFlow.Interfaces;
using BookApp_AutoFlow.Services;
using BookApp_AutoFlow.ViewModels;
using BookApp_AutoFlow.Views;
using CommunityToolkit.Maui;
using InputKit.Handlers;
using Microsoft.Extensions.Logging;
using UraniumUI;

namespace BookApp_AutoFlow;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiMicroMvvm<AppShell>()
            .UseMauiCommunityToolkit()
            .UseUraniumUI()
            .UseUraniumUIMaterial()
            .ConfigureMauiHandlers(handlers =>
            {
                handlers.AddInputKitHandlers();
            })
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddMaterialIconFonts();
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif
        builder.Services.MapView<BooksPage, BooksPageViewModel>();
        builder.Services.MapView<AppShell, AppShellViewModel>();
        builder.Services.MapView<BookDetailPage, BookDetailsPageViewModel>();
        builder.Services.MapView<AddOrUpdateBookDetailsPage, AddOrUpdateBookDetailsPageViewModel>();
        
        builder.Services.AddSingleton<Shell>(x => new AppShell());
        builder.Services.AddSingleton<IPageDialogService, PageDialogService>();
        builder.Services.AddSingleton<IShellNavigation, ShellNavigation>();
        builder.Services.AddSingleton<ISqlLiteDatabase, SqlLiteDatabase>();
        
        RegisterRoutes();
        return builder.Build();
    }

    private static void RegisterRoutes()
    {
        Routing.RegisterRoute("BookDetails", typeof(BookDetailPage));
        Routing.RegisterRoute("AddOrUpdateBookDetailsPage", typeof(AddOrUpdateBookDetailsPage));
    }
}