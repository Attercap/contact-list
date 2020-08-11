# contact-list
.Net Core 3 / Angular 8 simple contact list with architecture for bulk users &amp; records

Currently set as "development mode" project only

Requires SQL Server 2012 (or higher). Create your database and run the SQL\instantiate-sql.sql script.
Update appsettings.json in ContactList.WebAngular and ContactList.Test with your connection info.

NuGet Packages and node_modules not included in project, make sure to following the following steps:

1. Open the solution in Visual Studio and use the Manage NuGet packages to reinstall all neccessary libraries.
2. From a Node.js command line, go to the ClientApp directory under the web project and run "npm install" to download all node_package script libraries.
2. Once the install is complete run "ng build" to ensure the Angular app builds as expected
3. You should now be set to run an IIS Express development preview of the Web project

This is a basic "proof of concept" project and while it does work, there may be bugs and it's not protected from all possible user errors.

SQL is currently in-line for "ease of read"/code dissemination. In a working project would be Stored Proc.

Next steps:
Code clean-up/patternization of dtos (especially contact list /w proper messaging in case of error)
Change username-based created contact tables to guid-based (with script to automate existing table updates)
