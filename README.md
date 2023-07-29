
# C# Console Weather App 


The C# Weather App is a console application that allows users to retrieve weather information for a specific city using the OpenWeatherMap API. Users can sign in or sign up to access the weather data. 


## Features: 
1. Establishes a DB connection that is safe against SQL-Injection attacks. MySql.Data package is used for DB connection. 
2. Protects user information, passwords are hashed before storing in the DB with black-red pepper and with a high work-force level. Hashing and Validation is satisfied with CryptAndHash NuGet Package.
3. Basic log-in, sign-up.
4. All of the keys&sensetive attributes are defined in private so that they are secure.
5. Uses dynamic deserialization using the Newtonsoft.Json NuGet Package.
6. Established a mock_server using Postman for testing & developing.

### The C# Weather App uses the following external libraries: 

#### Newtonsoft.Json: 
* For JSON deserialization.
#### MySql.Data:  
* For connecting to MySQL database.
#### WarNov.CryptAndHash:
* For password hashing.