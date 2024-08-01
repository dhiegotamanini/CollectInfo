# School Management System

## Project Overview

This project was created to manage key aspects of a school system, such as teachers, students, and courses. Users can create, update, and delete records for teachers, students, and courses, as well as associate teachers with courses. A student can be enrolled in one course, a teacher can teach multiple courses, and a course can have multiple teachers.

## Technologies Used

- **ASP.NET Core MVC**: For building the web application.
- **Razor Pages**: For creating dynamic web pages.
- **Bootstrap CSS**: For responsive and modern web design.
- **Entity Framework Core**: For database operations and ORM.
- **C#**: As the programming language.
- **SQL Server**: As the database.

This project does not use JavaScript, focusing solely on Razor Pages and C# in the backend.

## Project Structure

The project is organized into the following layers:

- **Controllers**: Handle incoming HTTP requests and return responses.
- **Services**: Contain the business logic of the application.
- **Repositories**: Handle data access and database operations.
- **Models**: Represent the data structures of the application.
- **DTOs (Data Transfer Objects)**: Used to transfer data between layers.

AutoMapper is used for mapping entities to DTOs and vice versa.

## Dependency Injection

The project uses dependency injection to manage dependencies and expose only interfaces for accessing necessary methods. This keeps the project decoupled and easier to maintain and extend in the future.

## Configuration

Details about credentials and other settings are stored in `appsettings.json`. As this is a simple project, secure methods for storing sensitive information, such as database credentials, have not been implemented. For a production environment, it is recommended to use secure storage methods.

## How to Run the Project

1. **Clone the repository**:
    ```sh
    git clone git@github.com:dhiegotamanini/SchoolManagementSystem.git
    ```
2. **Navigate to the project directory**:
    ```sh
    cd yourrepository
    ```
3. **Restore the dependencies**:
    ```sh
    dotnet restore
    ```
4. **Update the database**:
    Ensure your SQL Server is running and update the connection string in `appsettings.json` as needed, then run:
    ```sh
    dotnet ef database update
    ```
5. **Run the project**:
    ```sh
    dotnet run
    ```

## Future Improvements

- **Security**: Implement secure storage for sensitive information such as database credentials.
- **Frontend Enhancements**: Integrate JavaScript for better user interactions.
- **Unit Testing**: Add unit tests to ensure code quality and reliability.
- **User Authentication**: Implement authentication and authorization for better access control.



## 🚀 Tecnologias
<div>
  <img src="https://img.shields.io/badge/HTML-239120?style=for-the-badge&logo=html5&logoColor=white">
  <img src="https://img.shields.io/badge/CSS-239120?&style=for-the-badge&logo=css3&logoColor=white">  
  <img src="https://img.shields.io/badge/bootstrap-%238511FA.svg?style=for-the-badge&logo=bootstrap&logoColor=white">
</div>
<div>
  <img src="https://img.shields.io/badge/c%23-%23239120.svg?style=for-the-badge&logo=csharp&logoColor=white">
  <img src="https://img.shields.io/badge/-Entity_Framework_Core-fff?style=flat&logo=Microsoft&logoColor=0078D7">
  <img src="https://img.shields.io/badge/-ASP.NET%20Core-fff?style=flat&logo=.net&logoColor=blue">
</div>
<div>
  <img src="https://img.shields.io/badge/-SQL-fff?style=flat&logo=Microsoft-SQL-Server&logoColor=blue">
  <img src="https://img.shields.io/badge/-Git-fff?style=flat&logo=git">
</div>

