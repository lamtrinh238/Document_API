# Document_API

## Description
- The goal of this exercise is to write a small web api (Restful, json) to handle all documents
- Using a simple web API framework as default, don't use any pattern like DI, multilayer
- Using 3rd JWT for authentication and authorization
- Using EF code first to work with MS SQL

## What you need to run this code
- Visual studio
- .NET Framework 4.7
- MS SQL
- Postman for test

## How to run this code
- Review connect_string in "Web.config" and make sure it correct
- In visual studio, open Package Manager Console and run this script "update-database -Verbose" 
	==> check in SSMS if the DB named DocumentDB already created, it's ok
- In the folder of the project, find the file "Document_API.postman_collection.json" then import to postman and test with script