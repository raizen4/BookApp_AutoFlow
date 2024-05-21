# BookApp_AutoFlow


https://github.com/raizen4/BookApp_AutoFlow/assets/15075728/a6f93b93-354f-487c-81c7-4cb98c147ee1


BookApp_AutoFlow is a comprehensive book management application built with C#. It provides functionalities to manage books, including adding, editing, and deleting books. The application is built with a focus on clean architecture, testability, and maintainability.

## Features

- **Book Management**: Add, edit, and delete books.
- **Search**: Perform search operations to filter books.
- **SQLite Database**: Uses SQLite for data persistence.
- **Unit Testing**: Contains unit tests for a viewmodel to showcase how I would mock and test my code. Please bear in mind, the suite of tests could definitely be improved to cover all viewmodels and services but for this showcase I considered enough to include just a few due to time constraints.

## Technologies

- C#
- SQLite
- Moq for mocking in unit tests
- Uranium UI for validation the validation form and material UI feel of the UI controls
- xUnit for unit testing
- MauiMicroMvvm for as the library of choice for MVVM - Could have also chosen Prism, but for the complexity of this project, I thought a more lightweight MVVM library would suffice

## Project Structure

The project is structured into different folders, each serving a specific purpose:

- **Models**: Contains the `Book` model.
- **ViewModels**: Contains the `BooksPageViewModel` which handles the logic for book management.
- **Services**: Contains the `SqlLiteDatabase` and `PageDialogService` services.
- **Interfaces**: Contains the `ISqlLiteDatabase` and `IPageDialogService` interfaces.
- **UnitTests**: Contains the unit tests for the `BooksPageViewModel` and `SqlLiteDatabase` classes.

## Setup

To run this project, you need to have .NET installed on your machine. Clone the repository and open the solution file in JetBrains Rider or any other .NET compatible IDE.

## Testing

The project includes comprehensive unit tests for the ViewModel and Database services. The tests are located in the `UnitTests` folder. To run the tests, use the test runner in your IDE.
