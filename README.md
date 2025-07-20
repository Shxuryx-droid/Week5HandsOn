# Week5HandsOn
Kafka Chat Application with C#

This repository contains a simple chat application built using C# Windows Forms, demonstrating integration with Apache Kafka as a streaming platform. It allows users to send and receive chat messages in real-time.
Table of Contents

    Introduction

    Kafka Concepts

    Prerequisites

    Installation and Setup

        Kafka and Zookeeper

        Creating Kafka Topic

    How to Run

        Kafka Console Producer

        Kafka Console Consumer

        C# Windows Forms Client

    Project Structure

    References

Introduction

This project showcases how to build a basic chat application using C# that leverages Apache Kafka for message streaming. The application consists of a Windows Forms client that can produce (send) messages to a Kafka topic and consume (receive) messages from the same topic, effectively acting as a real-time chat client.
Kafka Concepts

Before diving into the application, it's essential to understand some core Kafka concepts:

    Kafka: A distributed streaming platform capable of handling trillions of events a day. It's used for building real-time data pipelines and streaming applications.

    Kafka Architecture: Kafka operates as a cluster of one or more servers (brokers).

    Topics: A category or feed name to which records are published. Topics are always multi-subscriber; that is, a topic can have zero, one, or many consumers that subscribe to the data written to it.

    Partitions: Topics are divided into a set of partitions. Each partition is an ordered, immutable sequence of records that is continually appended to a structured commit log. Records in a partition are assigned a sequential ID number called the "offset" that uniquely identifies each record within the partition.

    Brokers: A Kafka server (node) is called a broker. Kafka brokers are stateless, so they use Zookeeper for maintaining their cluster state.

    Kafka plug in .NET: Libraries like Confluent.Kafka provide .NET clients to interact with Kafka brokers.

Prerequisites

Before you can run this application, you need to have the following installed:

    Apache Kafka and Apache Zookeeper: Download from the official Apache Kafka website.

    .NET SDK: .NET 5.0 or later (or the version compatible with your Visual Studio).

    Visual Studio: (Recommended) For building and running the C# Windows Forms application.

Installation and Setup
Kafka and Zookeeper

    Download Kafka: Download the latest stable release of Apache Kafka from https://kafka.apache.org/downloads.

    Extract: Extract the downloaded archive to a convenient location (e.g., H:\kafka).

    Start Zookeeper: Open a command prompt, navigate to your Kafka installation directory (e.g., H:\kafka\kafka_2.12-2.7.0), and run:

    .\bin\windows\zookeeper-server-start.bat .\config\zookeeper.properties

    You should see output indicating Zookeeper is running.

    Start Kafka Server: Open a new command prompt, navigate to your Kafka installation directory, and run:

    .\bin\windows\kafka-server-start.bat .\config\server.properties

    You should see output indicating the Kafka server is running.

Creating Kafka Topic

Once Zookeeper and Kafka are running, create the topic for the chat messages:

    Open a new command prompt, navigate to your Kafka installation directory (e.g., H:\kafka\kafka_2.12-2.7.0\bin\windows).

    Execute the following command to create a topic named chat-message:

    kafka-topics.bat --create --zookeeper localhost:2181 --replication-factor 1 --partitions 1 --topic chat-message

    You should see a confirmation message like "Created topic chat-message."

How to Run
Kafka Console Producer

You can use a console producer to send messages to the chat-message topic:

    Open a new command prompt, navigate to H:\kafka\kafka_2.12-2.7.0\bin\windows.

    Run the producer command:

    kafka-console-producer.bat --broker-list localhost:9092 --topic chat-message

    Now, you can type messages and press Enter to send them. These messages will be consumed by the C# application and the console consumer.

Kafka Console Consumer

You can use a console consumer to view messages from the chat-message topic:

    Open a new command prompt, navigate to H:\kafka\kafka_2.12-2.7.0\bin\windows.

    Run the consumer command:

    kafka-console-consumer.bat --bootstrap-server localhost:9092 --topic chat-message --from-beginning

    You will see messages appearing as they are sent by the C# application or the console producer.

C# Windows Forms Client

    Clone the repository or download the KafkaChatApp folder.

    Open in Visual Studio: Open the KafkaChatApp.sln solution file in Visual Studio.

    Restore NuGet Packages: Visual Studio should automatically restore the Confluent.Kafka NuGet package. If not, right-click on the solution in Solution Explorer and select "Restore NuGet Packages."

    Build the Project: Build the KafkaChatApp project (Build > Build Solution).

    Run the Application: Press F5 or click the "Start" button in Visual Studio to run the Windows Forms application.

    The application will open, allowing you to type messages and send them. Received messages will appear in the display area.

Project Structure

KafkaChatApp/
├── .gitignore
├── README.md
├── KafkaChatApp.sln
└── WinFormsClient/
    ├── WinFormsClient.csproj
    ├── Form1.cs
    ├── Form1.Designer.cs
    ├── Program.cs
    └── App.config (or appsettings.json for .NET Core)

References

    Apache Kafka .NET Application

    Step by Step Installation and Configuration Guide of Apache Kafka on Windows Operating System

    Formatted and updated via AI.



--------------------------------------------------------------------------------------------------------------------------------------------------------------
    JWT Authentication in ASP.NET Core Web API
-------------------------------------------------------------------------------------------------------------------------------------------------------------
This repository contains a hands-on exercise demonstrating how to implement JWT (JSON Web Token) based authentication in an ASP.NET Core Web API microservice.
Table of Contents

    Scenario

    Features

    Prerequisites

    Installation and Setup

        Project Creation

        NuGet Packages

        Configuration (appsettings.json)

        Startup Configuration (Program.cs)

        Models

        Authentication Controller (AuthController.cs)

        Secure Endpoint (SecureController.cs)

    How to Run

        Starting the API

        Testing the Login Endpoint

        Testing the Secured Endpoint

    Project Structure

Scenario

You are building a microservice that requires secure login. You need to implement JWT-based authentication to protect certain API endpoints.
Features

    JWT Token Generation: Generates a JWT token upon successful user login.

    Secure Endpoints: Protects API endpoints using the [Authorize] attribute, requiring a valid JWT token for access.

    Configurable JWT Settings: JWT key, issuer, audience, and token duration are configured via appsettings.json.

Prerequisites

Before you begin, ensure you have the following installed:

    .NET SDK: .NET 6.0 or later (or the version compatible with your Visual Studio/VS Code).

    Visual Studio 2022 or Visual Studio Code with C# extension.

    A tool for API testing: Postman, Insomnia, or curl.

Installation and Setup

Follow these steps to set up and run the project:
Project Creation

    Create a new ASP.NET Core Web API project:

    dotnet new webapi -n JwtAuthApi
    cd JwtAuthApi

NuGet Packages

    Install the necessary NuGet package:

    dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer

Configuration (appsettings.json)

    Update appsettings.json: Add the JWT configuration section.

    {
      "Logging": {
        "LogLevel": {
          "Default": "Information",
          "Microsoft.AspNetCore": "Warning"
        }
      },
      "AllowedHosts": "*",
      "Jwt": {
        "Key": "ThisIsASecretKeyForJwtTokenWhichMustBeLongEnoughAndSecure", // Ensure this key is strong and kept secret!
        "Issuer": "MyAuthServer",
        "Audience": "MyApiUsers",
        "DurationInMinutes": 60
      }
    }

    Note: The Key value should be a strong, randomly generated string in a production environment.

Startup Configuration (Program.cs)

    Modify Program.cs: Configure JWT Bearer authentication and authorization services.

    // ... (existing using statements and builder setup)

    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.IdentityModel.Tokens;
    using System.Text;
    using System.Security.Claims; // For ClaimTypes

    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    // Configure JWT Authentication
    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

    builder.Services.AddAuthorization(); // Add authorization services

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthentication(); // Use authentication middleware
    app.UseAuthorization();  // Use authorization middleware

    app.MapControllers();

    app.Run();

Models

    Create Models/LoginModel.cs: Define the model for login requests.

    // File: Models/LoginModel.cs
    namespace JwtAuthApi.Models
    {
        public class LoginModel
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }
    }

Authentication Controller (AuthController.cs)

    Create Controllers/AuthController.cs: Implement the login endpoint and JWT generation logic.

    // File: Controllers/AuthController.cs
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.IdentityModel.Tokens;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using JwtAuthApi.Models; // Ensure this namespace matches your model's namespace

    namespace JwtAuthApi.Controllers
    {
        [ApiController]
        [Route("api/[controller]")]
        public class AuthController : ControllerBase
        {
            private readonly IConfiguration _configuration;

            public AuthController(IConfiguration configuration)
            {
                _configuration = configuration;
            }

            [HttpPost("login")]
            public IActionResult Login([FromBody] LoginModel model)
            {
                // In a real application, you would validate credentials against a database
                // For demonstration, we'll use a hardcoded user.
                if (IsValidUser(model.Username, model.Password))
                {
                    var token = GenerateJwtToken(model.Username);
                    return Ok(new { Token = token });
                }
                return Unauthorized("Invalid username or password.");
            }

            /// <summary>
            /// Simulates user validation. In a real app, this would query a database.
            /// </summary>
            /// <param name="username">The username to validate.</param>
            /// <param name="password">The password to validate.</param>
            /// <returns>True if the user is valid, false otherwise.</returns>
            private bool IsValidUser(string username, string password)
            {
                // Dummy validation for demonstration purposes
                return username == "testuser" && password == "password123";
            }

            /// <summary>
            /// Generates a JWT token for the given username.
            /// </summary>
            /// <param name="username">The username for whom to generate the token.</param>
            /// <returns>The generated JWT token string.</returns>
            private string GenerateJwtToken(string username)
            {
                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, username),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) // JWT ID
                    // Add other claims as needed (e.g., roles)
                };

                var jwtKey = _configuration["Jwt:Key"];
                var jwtIssuer = _configuration["Jwt:Issuer"];
                var jwtAudience = _configuration["Jwt:Audience"];
                var jwtDurationInMinutes = Convert.ToDouble(_configuration["Jwt:DurationInMinutes"]);

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: jwtIssuer,
                    audience: jwtAudience,
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(jwtDurationInMinutes),
                    signingCredentials: creds
                );

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
        }
    }

Secure Endpoint (SecureController.cs)

    Create Controllers/SecureController.cs: Add an example of an endpoint protected by JWT authentication.

    // File: Controllers/SecureController.cs
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    namespace JwtAuthApi.Controllers
    {
        [ApiController]
        [Route("api/[controller]")]
        public class SecureController : ControllerBase
        {
            [HttpGet("data")]
            [Authorize] // This attribute protects the endpoint
            public IActionResult GetSecuredData()
            {
                // Access the authenticated user's claims
                var username = User.Identity?.Name;
                return Ok($"Hello {username}! You have accessed secured data.");
            }

            [HttpGet("public")]
            public IActionResult GetPublicData()
            {
                return Ok("This is public data, accessible without authentication.");
            }
        }
    }

How to Run
Starting the API

    Build and run the application:

    dotnet run

    The API will start, typically listening on https://localhost:70XX (the exact port will be shown in the console).

Testing the Login Endpoint

    Send a POST request to the login endpoint:

        URL: https://localhost:70XX/api/Auth/login (replace 70XX with your actual port)

        Method: POST

        Headers:

            Content-Type: application/json

        Body (JSON):

        {
          "username": "testuser",
          "password": "password123"
        }

        Expected Response: A 200 OK with a JSON body containing the JWT token:

        {
          "token": "eyJhbGciOiJIUzI1Ni..." // Your actual JWT token
        }

        Test with invalid credentials: Try different username/password to get a 401 Unauthorized response.

Testing the Secured Endpoint

    Test the public endpoint:

        URL: https://localhost:70XX/api/Secure/public

        Method: GET

        Expected Response: 200 OK with the message "This is public data, accessible without authentication."

    Test the secured endpoint (without token):

        URL: https://localhost:70XX/api/Secure/data

        Method: GET

        Expected Response: 401 Unauthorized

    Test the secured endpoint (with valid token):

        URL: https://localhost:70XX/api/Secure/data

        Method: GET

        Headers:

            Authorization: Bearer YOUR_JWT_TOKEN (replace YOUR_JWT_TOKEN with the token obtained from the login endpoint)

        Expected Response: 200 OK with a message like "Hello testuser! You have accessed secured data."

Project Structure

JwtAuthApi/
├── appsettings.json
├── appsettings.Development.json
├── Program.cs
├── JwtAuthApi.csproj
├── Controllers/
│   ├── AuthController.cs
│   └── SecureController.cs
└── Models/
    └── LoginModel.cs

    Formatted and updated via AI.
----------------------------------------------------------------------------------------------------------------------------------------------------------------