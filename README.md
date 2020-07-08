# **Car Market App**

Car Market is a platform for posting and browsing car listings.
It is a monolithic ASP.NET Core 3.1 MVC web application. SQL Server is used for DB with EF Core for ORM and Automapper for mapping. The front end is mainly Bootstrap. For the software architecture I have used the Repository-Service Pattern. While working on the project, I have striven to adhere to the good practices for writing clean, clear, quality code.

# Technologies:

-  .NET Core 3.1
-  ASP.NET Core 3.1
-  ASP.NET Core MVC, WebAPI
-  Entity Framework Core 3
-  HTML, CSS, Bootstrap
-  jQuery, JavaScript
-  Automapper, moment.js, FontAwesome
-  CloudinaryDotNet

# Brief description of functionality

In the project there are examples of, **view components**, **partial views**, **api controllers** with the needed jquery scripts to call them, **custom middleware**, **custom attribute validations**, integration with **a cloud for uploading image files** (cloudinary.com), custom exceptions, extensive **search functionality**, pagination, fontawesome icons, moment.js to format the creation times in local time for each user.

 - Non-registered users can browse car listings and use the search functionality.
 - Users can register, log in, log out. Once registered, they can create, edit, delete	listings, bookmark other users' listings, have pages that display their own listings and their bookmarked listings.
 - The admin can browse all the users and edit and delete their corresponding listings.
