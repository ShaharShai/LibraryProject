# Library Project

https://github.com/ShaharShai/LibraryProject/assets/138113516/1b313986-621b-49c2-9c7e-7e470249bb06

Full Video:

https://vimeo.com/840830805

This project is a library management system developed using C# .NET and follows the principles of Object-Oriented Programming (OOP). It focuses on maintaining a separation between the logic and the user interface for enhanced modularity and maintainability.

Project Structure
The project repository contains the following components:

# 1. Library Item - Abstract Class
This class serves as the base class for different types of library items. It contains common properties and methods shared by all library items.

Books - Class that derives from Library Item
This class represents books in the library and extends the functionality of the base Library Item class. It introduces additional properties specific to books, such as the author.

Journal - Class that derives from Library Item
This class represents journals in the library and also derives from the Library Item class. It includes properties that are relevant to journals.

Properties of the Library Item Class:

ID
Publisher/Company Name
Publish Date
Genre
Price
Borrow Date
Discount

# 2. Manager Class
The Manager class handles administrative tasks related to library items. It provides the following roles:

Add Library Item: Allows adding new library items to the collection.
Edit Library Item: Enables modifying the properties of existing library items.
Add a copy of an item: Facilitates adding additional copies of existing items.
Delete Library Item: Permits removing library items from the collection.

# 3. User Class
The User class handles operations related to borrowing and returning library items. It provides the following roles:

Borrow Library Item: Allows users to borrow items from the library.
Return Library Item: Enables users to return borrowed items.

# 4. Item Collection Class
This class manages a collection of library items, using a list or dictionary structure. It provides functionality to:

Add item: Add library items to the collection.
Search by conditions: Search for items based on specified conditions.

# 5.General
This section encompasses general features and functionalities of the library project, including:

Filtering by condition: Allows users to filter the list of items based on conditions such as name, author, genre, or publisher.
Notification for overdue items: Notifies users if an item is not returned on time.
Daily Report - Amounts - Borrows: Generates a daily report with information on borrowing activity and associated amounts.
Logic in Class Library: Emphasizes the implementation of business logic within the library classes.
Dealing with errors: Ensures robust error handling and exception management.
Creating a GUI: Envisions the development of a graphical user interface for enhanced user interaction.
Checks: Indicates the presence of various validation and integrity checks within the project.
