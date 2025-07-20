// JwtAuthApi.csproj
// This is the project file for the C# ASP.NET Core Web API application.
// It specifies the target framework and package references.
/*
<Project Sdk="Microsoft.NET.Sdk.Web">

  <PropertyGroup>
    <TargetFramework>net6.0</TargetFramework>
    <Nullable>enable</Nullable>
    <ImplicitUsings>enable</ImplicitUsings>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.AspNetCore.Authentication.JwtBearer" Version="6.0.0" />
    <PackageReference Include="Swashbuckle.AspNetCore" Version="6.2.3" />
  </ItemGroup>

</Project>
*/

// appsettings.json
// This file contains the application's configuration, including JWT settings.
// In a production environment, ensure the "Key" is a strong, randomly generated string
// and is managed securely (e.g., via environment variables or a secrets manager).
/*
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "Jwt": {
    "Key": "ThisIsASecretKeyForJwtTokenWhichMustBeLongEnoughAndSecure", // IMPORTANT: Use a strong, unique key in production!
    "Issuer": "MyAuthServer",
    "Audience": "MyApiUsers",
    "DurationInMinutes": 60
  }
}
*/

// Program.cs
// This file is the entry point for the ASP.NET Core application.
// It configures services (including authentication and authorization) and the HTTP request pipeline.
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims; // Required for ClaimTypes

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure JWT Authentication
// This section sets up the JWT Bearer authentication scheme.
builder.Services.AddAuthentication(options =>
{
    // Set the default authentication scheme to JWT Bearer.
    // This tells ASP.NET Core that if no specific scheme is mentioned,
    // it should try to authenticate using JWT Bearer.
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    // Configure the JWT Bearer options.
    options.TokenValidationParameters = new TokenValidationParameters
    {
        // ValidateIssuer: Ensures the issuer of the token is valid (e.g., "MyAuthServer").
        ValidateIssuer = true,
        // ValidateAudience: Ensures the audience of the token is valid (e.g., "MyApiUsers").
        ValidateAudience = true,
        // ValidateLifetime: Ensures the token has not expired and is not used before its "not before" time.
        ValidateLifetime = true,
        // ValidateIssuerSigningKey: Ensures the token's signature is valid using the specified key.
        ValidateIssuerSigningKey = true,

        // Specify the valid issuer from appsettings.json.
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        // Specify the valid audience from appsettings.json.
        ValidAudience = builder.Configuration["Jwt:Audience"],
        // Specify the signing key used to validate the token's signature.
        // The key must match the one used to sign the token.
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

// Add authorization services to the dependency injection container.
// This enables the use of authorization policies and attributes like [Authorize].
builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Enable Swagger UI for API documentation and testing in development environment.
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Redirect HTTP requests to HTTPS.
app.UseHttpsRedirection();

// Add authentication middleware to the pipeline.
// This middleware checks for authentication headers (like Authorization: Bearer <token>)
// and populates the HttpContext.User property with the authenticated principal.
app.UseAuthentication();

// Add authorization middleware to the pipeline.
// This middleware checks if the authenticated user is authorized to access the requested resource
// based on policies and attributes (like [Authorize]).
app.UseAuthorization();

// Map controllers to handle incoming HTTP requests.
app.MapControllers();

// Run the application.
app.Run();

```csharp
// Models/LoginModel.cs
// This file defines the data transfer object (DTO) for login requests.
// It represents the structure of the JSON payload expected when a user attempts to log in.
namespace JwtAuthApi.Models
{
    public class LoginModel
    {
        /// <summary>
        /// Gets or sets the username provided by the user for login.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the password provided by the user for login.
        /// </summary>
        public string Password { get; set; }
    }
}
```csharp
// Controllers/AuthController.cs
// This controller handles user authentication, specifically the login process.
// It validates user credentials and, upon success, generates a JWT token.
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JwtAuthApi.Models; // Ensure this namespace matches your model's namespace

namespace JwtAuthApi.Controllers
{
    [ApiController] // Indicates that this class is an API controller.
    [Route("api/[controller]")] // Defines the base route for this controller (e.g., /api/Auth).
    public class AuthController : ControllerBase // Inherits from ControllerBase for API-specific features.
    {
        private readonly IConfiguration _configuration; // Used to access configuration settings (like JWT key, issuer, etc.).

        /// <summary>
        /// Constructor for AuthController.
        /// </summary>
        /// <param name="configuration">Injected IConfiguration service.</param>
        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Handles user login requests.
        /// </summary>
        /// <param name="model">The login credentials (username and password) from the request body.</param>
        /// <returns>
        /// An <see cref="IActionResult"/> representing the result of the login attempt.
        /// Returns 200 OK with a JWT token on successful login.
        /// Returns 401 Unauthorized on invalid credentials.
        /// </returns>
        [HttpPost("login")] // Defines a POST endpoint at /api/Auth/login.
        public IActionResult Login([FromBody] LoginModel model)
        {
            // In a real-world application, you would perform robust user authentication here.
            // This typically involves:
            // 1. Hashing the provided password.
            // 2. Querying a user database to find the user by username.
            // 3. Comparing the hashed provided password with the stored hashed password.
            // 4. Handling various failure scenarios (user not found, incorrect password, locked account, etc.).

            // For this demonstration, we use a simple hardcoded validation.
            if (IsValidUser(model.Username, model.Password))
            {
                // If credentials are valid, generate a JWT token.
                var token = GenerateJwtToken(model.Username);
                // Return the token in the response.
                return Ok(new { Token = token });
            }
            // If credentials are invalid, return an Unauthorized response.
            return Unauthorized("Invalid username or password.");
        }

        /// <summary>
        /// Simulates user validation against a hardcoded username and password.
        /// This method should be replaced with actual database validation in a production application.
        /// </summary>
        /// <param name="username">The username to validate.</param>
        /// <param name="password">The password to validate.</param>
        /// <returns>True if the hardcoded user matches, false otherwise.</returns>
        private bool IsValidUser(string username, string password)
        {
            // This is a dummy validation for demonstration purposes ONLY.
            // DO NOT use this in a production environment.
            return username == "testuser" && password == "password123";
        }

        /// <summary>
        /// Generates a JSON Web Token (JWT) for the authenticated user.
        /// </summary>
        /// <param name="username">The username for whom the token is being generated.</param>
        /// <returns>A string representing the generated JWT token.</returns>
        private string GenerateJwtToken(string username)
        {
            // Define the claims that will be included in the JWT.
            // Claims are statements about an entity (typically, the user) and additional metadata.
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username), // The subject of the token (username).
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) // Unique identifier for the JWT.
                // You can add more claims here, such as user roles (e.g., new Claim(ClaimTypes.Role, "Admin")).
            };

            // Retrieve JWT configuration settings from appsettings.json.
            var jwtKey = _configuration["Jwt:Key"];
            var jwtIssuer = _configuration["Jwt:Issuer"];
            var jwtAudience = _configuration["Jwt:Audience"];
            // Parse the duration from string to double.
            var jwtDurationInMinutes = Convert.ToDouble(_configuration["Jwt:DurationInMinutes"]);

            // Create a symmetric security key from the JWT key bytes.
            // This key is used for signing and verifying the token.
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));

            // Create signing credentials using the key and the HMAC SHA256 algorithm.
            // This specifies how the token will be signed.
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Create the JWT token.
            var token = new JwtSecurityToken(
                issuer: jwtIssuer,                      // The issuer of the token.
                audience: jwtAudience,                  // The audience for whom the token is intended.
                claims: claims,                         // The claims to be included in the token.
                expires: DateTime.Now.AddMinutes(jwtDurationInMinutes), // The expiration time of the token.
                signingCredentials: creds               // The credentials used to sign the token.
            );

            // Write the token into its string representation.
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
```csharp
// Controllers/SecureController.cs
// This controller demonstrates how to protect API endpoints using the [Authorize] attribute.
// Endpoints marked with [Authorize] will require a valid JWT token for access.
using Microsoft.AspNetCore.Authorization; // Required for the [Authorize] attribute.
using Microsoft.AspNetCore.Mvc;

namespace JwtAuthApi.Controllers
{
    [ApiController] // Indicates that this class is an API controller.
    [Route("api/[controller]")] // Defines the base route for this controller (e.g., /api/Secure).
    public class SecureController : ControllerBase // Inherits from ControllerBase for API-specific features.
    {
        /// <summary>
        /// An endpoint that requires authentication (a valid JWT token).
        /// </summary>
        /// <returns>A message indicating successful access to secured data.</returns>
        [HttpGet("data")] // Defines a GET endpoint at /api/Secure/data.
        [Authorize] // This attribute ensures that only authenticated users with a valid JWT can access this endpoint.
        public IActionResult GetSecuredData()
        {
            // When an endpoint is authorized, the HttpContext.User property is populated
            // with the authenticated user's principal, allowing access to their claims.
            var username = User.Identity?.Name; // Get the username from the token's claims.
            return Ok($"Hello {username}! You have accessed secured data.");
        }

        /// <summary>
        /// An endpoint that is publicly accessible (does not require authentication).
        /// </summary>
        /// <returns>A message indicating public data.</returns>
        [HttpGet("public")] // Defines a GET endpoint at /api/Secure/public.
        public IActionResult GetPublicData()
        {
            // This endpoint does not have the [Authorize] attribute, so it can be accessed by anyone.
            return Ok("This is public data, accessible without authentication.");
        }
    }
}
