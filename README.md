# JWT Authentication ASP.NET Core Web API

## Description

A simple project using ASP.NET Core Web API, JWT authentication & postgreSQL.


* **Docker**: Contains postgreSQL service with user, password and database.
* **AuthenticationController**: Contains login and register for users.
* **UserController**: Contains fetch user by Id with authorization.

### Docker

The `docker-compose-postgre.yaml` file contains the service to spawn a postgreSQL service using docker.

Run with docker-compose
```
$ docker-compose -f docker-compose-postgre.yaml up
```

### Deployment

Deploy existing migrations with NuGet package EntityFrameworkCore Tools
```
$ Update-Database
```

### AuthenticationController

The `AuthenticationController` Contains login and register for users.

* POST `/authentication/login`

    * Returns the user information with jwt token.
    * Post Http Request Link: `http://localhost/authentication/login`
    * Request Body Example:

        ```json
        {
            "email": "simple@test.com",
            "password": "test123"
        }
        ```

    * Response Example:

        ```json
        {
            "id": 0,
            "firstName": "Simple",
            "lastName": "Test",
            "birthDate": null,
            "email": "simple@test.com",
            "role": "Everyone",
            "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJlbWFpbCI6ImJsYWlzLmlzYWVsQGdtYWlsLmNvbSIsIm5hbWVpZCI6IjEiLCJnaXZlbl9uYW1lIjoiSXNhZWwiL......."
        }
        ```

* POST `/authentication/register`

    * Regsiter the user with provided information in postgreSQL database.
    * Returns the user information.
    * Post Http Request Link: `http://localhost/authentication/register`
    * Request Body Example:

        ```json
        {
            "email": "simple@test.com",
            "password": "test123",
            "firstName": "Simple",
            "lastName": "Test"
        }
        ```

    * Response Example:

        ```json
        {
            "id": 0,
            "firstName": "Simple",
            "lastName": "Test",
            "birthDate": null,
            "email": "simple@test.com",
            "role": "Everyone"
        }
        ```
### UserController

The `UserController` Contains fetch user by Id with authorization.

* GET `/user/getuser/{id}`

    * Returns the user information.
    * Get Http Request Link: `http://localhost/user/getuser/1`
    * Request Header Example:
        `Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJlbWFpbCI6ImJsYWlzLmlzYWVsQGdtYWlsLmNvbSIsIm5hbWVpZCI6IjEiLCJnaXZlbl9uYW1lIjoiSXNhZWwiL.......`
    * Response Example:

        ```json
        {
            "id": 1,
            "firstName": "Simple",
            "lastName": "Test",
            "birthDate": null,
            "email": "simple@test.com",
            "role": "Everyone"
        }
        ```