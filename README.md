# access-database-application-windows
This project creates a Database application using C# that interacts with Microsoft Access tables. This application will allow direct interaction with database using an application interface.

Features of db app:
- Connect to database (creates a connection with the access database)
- Run query
- Insert record
- Update record
- Delete record
- File > Exit Application

# Todos
- Get data from a Table as DataTable type  -> Use GridView to display the data on C# app
- C# Application - Insert Delete Update Select in MS Access Database: https://www.youtube.com/watch?v=uONQaT-nwls&ab_channel=FoxLearnFoxLearn: Add search feature,
- Add log file (refer to my other project)
- Export data to Excel file
- Import data feature, if the data format is persistent and it complies with the database schema.

# DataGrid feature
- Disable editing first column (ID). The data of this column is automatically updated when user inserts a new row.
- If the value of previous changed cells is zero -> No need for specify the change reason, the update is valid.
- Perform data validation and show input errors (if any) when user inputs data to the Table, so user must correct before the next SAVE button click. Note that the program might already performs this when we define the data structure for the Column Schema  
