# URLShortner Service

This is a simple URL Shortener Service built with ASP.NET Core using a Clean Architecture approach. It allows users to shorten a given URL into a compact  8 letter format
and retrieve the original URL by its shortened ID.

The project is structured with separate layers (Domain, Application, Infrastructure, and API), ensuring clean separation of concerns and scalability.

#Table of Contents


	1. Project Overview
	2. Architecture
	3. Logic for Short ID Generation
	4. Setup Instructions
	5. API Endpoints, Sample Input and Output
	6. Testing
	7. Scalability Options

##1. Project Overview

This application provides the following core functionalities:

	1.1 Shorten a URL:
	Takes a long URL and generates a unique short ID.

	1.2 Resolve a Short URL:
	Resolves the short ID to its corresponding original URL.

##2. Architecture

The project follows the Clean Architecture pattern, ensuring that each layer has clear responsibilities and no tight coupling.

	2.1 Domain Layer: Defines core entities (UrlMapping).
	2.2 Application Layer: Contains business logic, interfaces (IUrlRepository, IUrlShortenerService), and service implementations.
	2.3 Infrastructure Layer: Implements repositories (InMemoryUrlRepository) to provide storage.
	2.4 WebAPI Layer: Exposes endpoints to clients via controllers.
	2.5 Test Projects: Unit tests for Application and Infrastructure layers.

The dependecies flow towards the Domain Layer.
	Domain <--- Application <--- Infrastructure <--- API

##3. Logic for Short ID Generation


	3.1 The logic generates new GUID which ensures randamness and are unlikely to collide. 
	3.2 The GUID is converted into Base64 string and the non url friendly characters are removed
	3.3 Then the first 8 characters are picked and this is stored against the long url.

##4. Setup Instructions


       4.1 Assumptions: .Net 7.0 SDK installed, Visual Sudio IDE installed.
       4.2 Clone the Repository from the git link. (https://github.com/sandeep-ishar/URLShortener)
       4.3 Restore Dependencies either by right clicking on the solution or in the terminal, type ""dotnet restore"".
       4.4 Set the UrlShortenerApi As the start up project and run. If using terminal, type ""dotnet run""
       4.5 The service should be available at  http://localhost:5103/swagger/index.html if using http
       4.6. The service should be available at http://localhost:38806/swagger/index.html if using IISExpress.
       4.7 Optional: ShortDomain can be setup in appsettings.json to match the your-domain format(http://<your_domain>/<short_id>).
	           - Default has been set to "ShortDomain": "http://www.testdomain.com/".		

##5.Sample Input and Output  - API Endpoints 


	1. Shorten URL
		Endpoint: POST /api/shorten
		Description: Accepts a URL and generates a short ID.
		Request Body:   (Use Swagger UI to test)
						{
							"originalUrl": "https://www.google.com"
						}
		Response Body: 
						{
						  "shortId": "uxbI2s7m",
						  "shortUrl": "http://localhost:38806//uxbI2s7m"
						}
       
	
        2. Resolve Short URL
		Endpoint: GET /api/{shortId}
		Description: Resolves the short ID to its original URL.
		Example Request Body : /api/bw7xqbIL (Use Swagger UI to test)
		Example Response Body:
		              {
						"originalUrl": "https://www.google.com"
         		      }

##6. Testing


	The project includes unit tests for both Application and Infrastructure layers:
	1. Run all Tests from terminal - dotnet test
	2. If using UI, Right click and click on run test on the respective test projects.

##7. Scalability Options
     Below are the few options to scale this application:

		Persistent Storage: Replace InMemoryUrlRepository with a database-based repository:
							Relational: SQL Server, PostgreSQL.
							NoSQL: MongoDB, CosmosDB, or DynamoDB.
		
		Distributed Cache: Add caching (e.g., Redis) for frequent lookups.

		Load Balancing: Deploy the application in a load-balanced environment with multiple instances.

		Infrastructure scaling : Scale out more instances of apps , DBs or use auto scaling in case of Cloud based app Service plans.
  
