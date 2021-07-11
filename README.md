# access-database-application-windows
This project creates a Database application using C# that interacts with Microsoft Access tables. This application will allow direct interaction with database using an application interface.

Features of db app:
- Connect to database (creates a connection with the access database)
- Run query
- Insert record
- Update record
- Delete record
- File > Exit Application

# Todo tasks
- A method for getting and setting Settings.settings Default values, instead of current long access commands.
- Insert new input form, check the input conditions, if all the values are valid -> Allow save. Display error messages
- Export Excel file with name:current date + table, show export messages.
- Add search by column name feature

# Application design (behaviors)
## Completed features
- Load the selected database and table at startup
- Get data from a Table as DataTable type  -> Use GridView to display the data on C# app
- Export data to Excel file
- Flexibly choose the Access database and table
- Lock device feature -> After a device is locked, it must be unlock (by priviledged user) to modify.

## Todos features
- Import data feature from files (Access, Excel...), if the data format is persistent and it complies with the database schema.
- Allow update the location of an item anytime.
- Add project history

### DataGrid feature
- Disable editing first column (ID). The data of this column is automatically updated when user inserts a new row.
- If the value of previous changed cells is zero -> No need for specify the change reason, the update is valid.
- Perform data validation and show input errors (if any) when user inputs data to the Table, so user must correct before the next SAVE button click. Note that the program might already performs this when we define the data structure for the Column Schema  
- Trying to update ID column value -> doesn't take effect. The program will discard this, and auto fill field in the next DataGrid Update

# Preferences, links and resources
- C# Application - Insert Delete Update Select in MS Access Database: https://www.youtube.com/watch?v=uONQaT-nwls&ab_channel=FoxLearnFoxLearn: Add search feature.

# Bugs:
1. When [Search] -> [Update] -> It inserts a new row, instead of what we're implying to be. That's because the current 'DataGridview1' is changed, thus, the data we work on when click [Insert], [Update] buttons are wrong.
2. Detect source of bug: If a cell value/column name has space characters or Vietnamese characters -> causes the Update to throw an exception
3. When changing the Table with different schema -> Error when calling SelectionChange event (fill_input_textboxes()). Probably because the new schema hasn't updated.