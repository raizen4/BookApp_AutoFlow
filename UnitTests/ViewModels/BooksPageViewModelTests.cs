using System.Collections.ObjectModel;
using BookApp_AutoFlow.Interfaces;
using BookApp_AutoFlow.Models;
using BookApp_AutoFlow.ViewModels;
using Moq;
using Xunit;
using ILoggerFactory = Microsoft.Extensions.Logging.ILoggerFactory;
using INavigation = MauiMicroMvvm.INavigation;

namespace UnitTests.ViewModels;

public class BooksPageViewModelTests
{
    [Fact]
    public void PerformSearchBooks_WhenCalled_ShouldFilterBooks()
    {
        // Arrange
        var loggerFactoryMock = new Mock<ILoggerFactory>();
        var navigationMock = new Mock<INavigation>();
        var pageDialogsMock = new Mock<IPageDialogs>();
        var mockShellNavigation = new Mock<IShellNavigation>();
        var mockDatabaseService = new Mock<ISqlLiteDatabase>();
        var mockPageDialogs = new Mock<IPageDialogService>();

        var books = new ObservableCollection<Book>
        {
            new() { Title = "Book 1" },
            new() { Title = "Book 2" }
        };
        mockDatabaseService.Setup(db => db.GetBooks()).ReturnsAsync(new List<Book>(books));

        var viewModel = new BooksPageViewModel(
            new ViewModelContext(loggerFactoryMock.Object, navigationMock.Object, pageDialogsMock.Object),
            mockShellNavigation.Object,
            mockDatabaseService.Object,
            mockPageDialogs.Object);

        // Act
        viewModel.PerformSearchBooks("Book 1");

        // Assert
        Assert.Single(viewModel.FilteredBooks);
        Assert.Equal("Book 1", viewModel.FilteredBooks.First().Title);
    }

    [Fact]
    public async Task AddBook_WhenCalled_ShouldNavigateToAddBookPage()
    {
        // Arrange
        var mockShellNavigation = new Mock<IShellNavigation>();
        var mockDatabaseService = new Mock<ISqlLiteDatabase>();
        var mockPageDialogs = new Mock<IPageDialogService>();
        var loggerFactoryMock = new Mock<ILoggerFactory>();
        var navigationMock = new Mock<INavigation>();
        var pageDialogsMock = new Mock<IPageDialogs>();

        var viewModel = new BooksPageViewModel(
            new ViewModelContext(loggerFactoryMock.Object, navigationMock.Object, pageDialogsMock.Object),
            mockShellNavigation.Object,
            mockDatabaseService.Object,
            mockPageDialogs.Object);

        // Act
        await viewModel.AddBook();

        // Assert
        mockShellNavigation.Verify(
            x => x.GoToAsync("AddOrUpdateBookDetailsPage", It.IsAny<ShellNavigationQueryParameters>()), Times.Once);
    }

    [Fact]
    public async Task EditBookDetails_WhenCalled_ShouldNavigateToEditBookPage()
    {
        // Arrange
        var mockShellNavigation = new Mock<IShellNavigation>();
        var mockDatabaseService = new Mock<ISqlLiteDatabase>();
        var mockPageDialogs = new Mock<IPageDialogService>();
        var loggerFactoryMock = new Mock<ILoggerFactory>();
        var navigationMock = new Mock<INavigation>();
        var pageDialogsMock = new Mock<IPageDialogs>();
        var bookToEdit = new Book { Title = "Book 1" };

        var viewModel = new BooksPageViewModel(
            new ViewModelContext(loggerFactoryMock.Object, navigationMock.Object, pageDialogsMock.Object),
            mockShellNavigation.Object,
            mockDatabaseService.Object,
            mockPageDialogs.Object);

        // Act
        await viewModel.EditBookDetails(bookToEdit);

        // Assert
        mockShellNavigation.Verify(
            x => x.GoToAsync("AddOrUpdateBookDetailsPage", It.IsAny<ShellNavigationQueryParameters>()), Times.Once);
    }

    [Fact]
    public async Task RemoveBook_WhenCalled_ShouldRemoveBookFromDatabase()
    {
        // Arrange
        var loggerFactoryMock = new Mock<ILoggerFactory>();
        var navigationMock = new Mock<INavigation>();
        var pageDialogsMock = new Mock<IPageDialogs>();
        var mockShellNavigation = new Mock<IShellNavigation>();
        var mockDatabaseService = new Mock<ISqlLiteDatabase>();
        var mockPageDialogs = new Mock<IPageDialogService>();

        var bookToRemove = new Book { Id = new Guid(), Title = "Book 1" };
        mockDatabaseService.Setup(db => db.DeleteBook(bookToRemove.Id)).ReturnsAsync(true);

        var viewModel = new BooksPageViewModel(
            new ViewModelContext(loggerFactoryMock.Object, navigationMock.Object, pageDialogsMock.Object),
            mockShellNavigation.Object,
            mockDatabaseService.Object,
            mockPageDialogs.Object);

        viewModel.Books = new ObservableCollection<Book> { bookToRemove };

        // Act
        await viewModel.RemoveBook(bookToRemove);

        // Assert
        mockDatabaseService.Verify(x => x.DeleteBook(bookToRemove.Id), Times.Once);
        Assert.Empty(viewModel.Books);
    }
}