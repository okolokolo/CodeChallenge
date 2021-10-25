Steps to run the api

Open up the project in visual studio
Set the startup project to CodeChallenge project
Run the following commands in the Package Manager Console using the DataContext as Default project in the Package Manager Console
  a. Add-Migration InitialCreate 
  b. Update-Database
In both appSettings files, in the DataContext console and api applications, set the default connections to the file path of the generated sqlite database in the CodeChallenge folder.
Run the DataContext console application to seed the data.
Set the Startup project back to the api and run the api to test the routes. Note valid Ids are Between 1 & 4. 
Run the tests in the test project
