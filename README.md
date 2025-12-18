# Overview
Console based CRUD application to track habits.
Developed using C# and SQLite.

# How to operate
Follow the onscreen prompts! A simple interface is provided which gives you few, but clear, options.

If you want to run the tests, simply move into "LoggerEngine.Tests" and run 'dotnet test'.

# Given Requirements:
- [x] This is an application where you’ll log occurrences of a habit.
- [x] This habit can't be tracked by time (ex. hours of sleep), only by quantity (ex. number of water glasses a day)
- [x] Users need to be able to input the date of the occurrence of the habit
- [x] The application should store and retrieve data from a real database
- [x] When the application starts, it should create a sqlite database, if one isn’t present.
- [x] It should also create a table in the database, where the habit will be logged.
- [x] The users should be able to insert, delete, update and view their logged habit.
- [x] You should handle all possible errors so that the application never crashes.
- [x] You can only interact with the database using ADO.NET. You can’t use mappers such as Entity Framework or Dapper.
- [x] Follow the DRY Principle, and avoid code repetition.
- [x] Your project needs to contain a Read Me file where you'll explain how your app works and tell a little bit about your thought progress. What was hard? What was easy? What have you learned? Here's a nice example:

# Features

* SQLite database connection

	- The program uses a SQLite db connection to store and read information. 
	- If no database exists, or the correct table does not exist they will be created on program start.

* A console based UI where users can navigate by key presses
 
* CRUD DB functions

	- From the main menu users can Create, Read, Update or Delete entries for whichever date they want, entered in YYYY-MM-DD format. They can also enter "t" for the current date.
	- Dates inputted are checked to make sure they are in the correct format, and a real date that can exist.

# Challenges

- [x] Provide Unit Tests for helper methods
- [x] Use Parameterized queries for secure database interactions
- [x] Seed Data into database automatically if it doesn't exist 
	
# Difficulties, etc.,

There wasn't really anything that proved a big challenge in this project; the requirements were straightforward, and all the technologies involved were well documented. I tried to focus a bit more on creating interfaces even though they weren't
a tremendous lift for this application; it's a practice I want to continue to develop, and it did ease testing.
