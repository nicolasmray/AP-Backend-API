using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using ExpenseAdminSystem.Model.Repositories;

namespace CourseAdminSystem.API.Middleware
{
    public class BasicAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public BasicAuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context) {
            //Console.WriteLine("---- Basic Authentication Middleware Triggered ----");

            var endpoint = context.GetEndpoint();
            if (endpoint?.Metadata.GetMetadata<IAllowAnonymous>() != null)
            {
                Console.WriteLine("Anonymous access allowed for this endpoint.");
                await _next(context);
                return;
            }

            string authHeaderValue = context.Request.Headers["Authorization"];
            Console.WriteLine("Authorization Header Received: " + authHeaderValue);

            if (string.IsNullOrWhiteSpace(authHeaderValue))
            {
                Console.WriteLine("Authorization header is missing.");
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Authorization Header value not provided");
                return;
            }

            try
            {
                if (!authHeaderValue.StartsWith("Basic "))
                {
                    Console.WriteLine("Authorization header does not start with 'Basic '");
                    context.Response.StatusCode = 401;
                    await context.Response.WriteAsync("Authorization header must start with 'Basic '");
                    return;
                }

                var encodedCredentials = authHeaderValue.Substring("Basic ".Length).Trim();
                //Console.WriteLine("Encoded Credentials: " + encodedCredentials);

                var decodedBytes = Convert.FromBase64String(encodedCredentials);
                var decodedString = Encoding.UTF8.GetString(decodedBytes);
                //Console.WriteLine("Decoded Credentials: " + decodedString);

                var parts = decodedString.Split(':');
                if (parts.Length != 2)
                {
                    Console.WriteLine("Credentials format invalid (missing ':').");
                    context.Response.StatusCode = 401;
                    await context.Response.WriteAsync("Invalid credentials format");
                    return;
                }

                var username = parts[0];
                var password = parts[1];
                //Console.WriteLine($"Parsed Username: {username}, Password: {password}");

                var userRepository = context.RequestServices.GetRequiredService<UserRepository>();
                var user = userRepository.GetUsers().FirstOrDefault(u => u.UserName == username);

                if (user != null)
                {
                    //Console.WriteLine("User found in DB: " + user.UserName);
                    if (user.Password == password)
                    {
                        Console.WriteLine("Authentication successful.");
                        await _next(context);
                        return;
                    }
                    else
                    {
                        Console.WriteLine("Password mismatch.");
                    }
                }
                else
                {
                    Console.WriteLine("User not found in DB.");
                }

                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Incorrect credentials provided");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception during authentication: " + ex.Message);
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Invalid Authorization Header");
            }

            //Console.WriteLine("---- End of Middleware ----");
        }

    }

    public static class BasicAuthenticationMiddlewareExtensions
    {
        public static IApplicationBuilder UseBasicAuthenticationMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<BasicAuthenticationMiddleware>();
        }
    }
}
