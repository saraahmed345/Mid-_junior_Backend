using Microsoft.AspNetCore.Authentication.JwtBearer;  // Namespace for JWT authentication
using Microsoft.AspNetCore.Builder;                 // Namespace for building ASP.NET Core applications
using Microsoft.AspNetCore.Hosting;                // Namespace for hosting ASP.NET Core applications
using Microsoft.Extensions.Configuration;          // Namespace for configuration management
using Microsoft.Extensions.DependencyInjection;    // Namespace for dependency injection
using Microsoft.Extensions.Hosting;                // Namespace for extension-based hosting
using Microsoft.IdentityModel.Tokens;              // Namespace for working with security tokens
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;




// Namespace for text-related operations


namespace Mid_Junior_backend
{
    public class StartUp                           // Startup class, entry point for configuration
    {
        public StartUp(IConfiguration configuration) // Constructor, injects configuration
        {
            Configuration = configuration;          // Store configuration for later use
        }

        public IConfiguration Configuration { get; } // Property to access configuration

        public void ConfigureServices(IServiceCollection services) // Configure services
        {
            // ... other service configuration (add any additional services here)

            // JWT configuration (replace with your secrets and settings)
            var jwtSecret = Configuration["Jwt:Secret"]; // Read secret from configuration
            var jwtIssuer = Configuration["Jwt:Issuer"]; // Read issuer from configuration

            // Enhanced JWT configuration for security
            var key = Encoding.ASCII.GetBytes(jwtSecret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; // Set default auth scheme
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; // Set default challenge scheme
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false; // Adjust to true for production environments
                x.SaveToken = true;             // Save tokens for subsequent requests
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,        // Validate JWT issuer
                    IssuerSigningKey = new SymmetricSecurityKey(key), // Use secret key for validation
                    
                    ValidateAudience = true,       // Validate JWT audience
                    ValidAudience = jwtIssuer,      // Audience should match issuer
                    ValidateLifetime = true,       // Validate JWT expiration
                    RequireExpirationTime = true,  // Require JWT to have an expiration time
                    ClockSkew = TimeSpan.Zero      // Prevent acceptance of expired tokens
                };
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("BuyerOnly", policy => policy.RequireRole("buyer")); // Define "BuyerOnly" policy
                options.AddPolicy("SellerOnly", policy => policy.RequireRole("seller")); // Define "SellerOnly" policy
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) // Configure app behavior
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage(); // Use developer exception page for debugging
            }

            // ... other configuration (add any middleware or configuration here)

            app.UseRouting(); // Enable routing to match requests to endpoints
            app.UseAuthentication(); // Enable authentication middleware
            app.UseAuthorization(); // Enable authorization middleware

          
        }
    }
}
