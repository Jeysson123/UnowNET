using Jose;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models;
using Models.Dtos;
using Models.Entities;
using Services.Interfaces;
using Services.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class JWTService : IJWTService
    {
        private readonly ILogger<JWTService> _logger;

        public JWTService(ILogger<JWTService> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<string> GenerateToken(string username, string secretkey)
        {
            try
            {
                var payload = new
                {
                    sub = username,
                    exp = DateTimeOffset.UtcNow.AddHours(1).ToUnixTimeSeconds()
                };

                var key = Encoding.UTF8.GetBytes(secretkey);

                return JWT.Encode(payload, key, JwsAlgorithm.HS256);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generting token");

                return string.Empty;
            }
        }
    }
}
