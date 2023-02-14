# WebMVC

# Description
The application provides information about the announcements of the market of secondary goods on various sites. 
The web application allows users registered in the system to obtain information about the desired announcements. 

# Demonstration & DB script
All the demonstration files are placed in demo folder.
DB backups files(include sql script) are placed in PostgreSQL folder.

# More about application
The developed web application contains a server part responsible for storing data about goods on the site and processing them. Server part includes PostgreSql provider.
To provide the server part of the project, two trigger functions were implemented that are responsible for validating the input data and the business logic of the application.
The first trigger function is responsible for safely deleting the user from the database. It checks the user for a cart of favorites and a cart of recently viewed items,
if necessary, the function removes entries from the tables of links between carts and products, and also removes the links between the user and carts, followed by 
deleting the user himself. The trigger is called when a user is deleted.
The second trigger function performs an integrity check, it is designed to prevent the entry of an incorrect password consisting of less than 8 characters. If an incorrect
password is entered, a warning is displayed and the record is not added to the table.
Also implemented protection against SQL injection.
The client part is a site with a wide range of options for viewing and filtering ads.
