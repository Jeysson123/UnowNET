using Jose;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Apis.Middlewares
{
    public class JWTMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _secretKey;

        public JWTMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;

            _secretKey = configuration.GetSection("JWT:JWTSecretKey").Value;

            if (string.IsNullOrEmpty(_secretKey))
            {
                throw new Exception("JWT Secret Key not found");
            }
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                if (context.Request.Path.Value.StartsWith("/api/task", StringComparison.OrdinalIgnoreCase))
                {
                    var token = context.Request.Headers["Authorization"].ToString()?.Replace("Bearer ", "");

                    if (string.IsNullOrEmpty(token))
                    {
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;

                        await context.Response.WriteAsync("Authorization token is missing.");

                        return;
                    }

                    ValidateToken(token);
                }

                await _next(context);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;

                await context.Response.WriteAsync($"Authorization failed: {ex.Message}");
            }
        }

        private void ValidateToken(string token)
        {
            var key = Encoding.UTF8.GetBytes(_secretKey);

            var payload = JWT.Decode<Dictionary<string, object>>(token, key);

            if (payload == null || !payload.ContainsKey("sub"))
            {
                throw new Exception("Invalid token payload.");
            }

            var exp = Convert.ToInt64(payload["exp"]);

            if (DateTimeOffset.UtcNow.ToUnixTimeSeconds() > exp)
            {
                throw new Exception("Token has expired.");
            }
        }
    }
}
