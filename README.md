# Collect Info from external api

## Project Overview

This project was created to get some informations from api github. The information that need to get is check all the files(you can specificate it in appsettins.json - CriteriaExtensionSearchFileType) and count how many letter exist in the files. 
By default, I created two endpoints for it.

total-letters-by-files : Return a list of how many letters exist by file
total-letters-all-files: Returns a list of sum how many letters exist in all the files 

## Technologies Used

- **ASP.NET Core Web Api**: For create endpoints to getting necessary information from external api (Git hub api).
- **C#**: As the programming language.

This project focusing only in backend - C#.

## Project Structure

The project is organized into the following layers:

- **CollectInfo.Api**: Handle incoming HTTP requests and return responses.
- **CollectInfo.Business**: Contain the business logic of the application, some validation and format return to CollectInfo.Api .
- **Repositories**: Handle connection to external api to get data.
- **Models**: Represent the data structures of the application.

## Dependency Injection

The project uses dependency injection to manage dependencies and expose only interfaces for accessing necessary methods. This keeps the project decoupled and easier to maintain and extend in the future.

## Configuration

Details about credentials and other settings are stored in `appsettings.json`. As this is a simple project, secure methods for storing sensitive information, such as user secrets to keep key hide, have not been implemented. 
For a production environment, it is recommended to use secure storage methods.

## How to Run the Project

1. **Clone the repository**:
    ```sh
    git clone git@github.com:dhiegotamanini/CollectInfo.git
    ```
2. **Navigate to the project directory**:
    ```sh
    cd yourrepository
    ```
3. **Restore the dependencies**:
    ```sh
    dotnet restore
    ```
4. **Run the project**:
    ```sh
    dotnet run
    ```

## Future Improvements

- **Security**: Implement secure storage for sensitive information such as key secrets credentials.
- **Unit Testing**: Add unit tests to ensure code quality and reliability.


## 🚀 Tecnologies

<div>
  <img src="https://img.shields.io/badge/-C_Sharp-fff?style=flat&logo=csharp&logoColor=0078D7">
  <img src="https://img.shields.io/badge/-ASP.NET%20Core-fff?style=flat&logo=.net&logoColor=blue">
</div>
<div>  
  <img src="https://img.shields.io/badge/-Git-fff?style=flat&logo=git">  
</div>

