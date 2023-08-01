
# C# Console Weather App 


The C# Weather App has been upgraded from a console application to a Web API, providing weather information for a specific city using the OpenWeatherMap API. Users can now access the weather data through HTTP requests. Additionally, the application has been dockerized, allowing it to be easily deployed and accessed outside of Visual Code.

## Features: 
1. Establishes a DB connection that is safe against SQL-Injection attacks. MySql.Data package is used for DB connection. 
2. Protects user information, passwords are hashed before storing in the DB with black-red pepper and with a high work-force level. Hashing and Validation is satisfied with CryptAndHash NuGet Package.
3. Basic log-in, sign-up.
4. All of the keys&sensetive attributes are defined in private so that they are secure.
5. Uses dynamic deserialization using the Newtonsoft.Json NuGet Package.
6. During development and testing, a Postman mock server was used to simulate API responses and ensure smooth integration with the Web API.
7. The application has been dockerized, ensuring easy deployment and execution outside of Visual Code. The Docker container encapsulates the entire application and its dependencies, providing consistency and portability across different environments.
8.  The application has been transformed into a Web API using ASP.NET Core, enabling users to access weather information through HTTP requests. Endpoints have been defined to handle different functionalities.


### The C# Weather App uses the following external libraries and technologies: 

#### Newtonsoft.Json: 
* For JSON deserialization.
#### MySql.Data:  
* For connecting to MySQL database.
#### WarNov.CryptAndHash:
* For password hashing.