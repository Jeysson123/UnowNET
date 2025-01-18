using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Models.Dtos;
using Models.Entities;
using Services.Services;
using Services.Utils;

namespace Apis.Controllers
{
    [Route("api/auth")]
    [ApiController]
    [ApiVersion("1.0")]
    public class AuthController : ControllerBase
    {
        private readonly JWTService _jwtservice;
        private readonly string secreyKey;
        private readonly string username;
        private readonly string password;

        public AuthController(JWTService JWTService, IConfiguration configuration)
        {
            _jwtservice = JWTService;
            secreyKey = configuration.GetSection("JWT:JWTSecretKey").Value;
            username = configuration.GetSection("JWT:JWTUsername").Value; ;
            password = configuration.GetSection("JWT:JWTPassword").Value; ;
        }

        [HttpPost]
        public async Task<ActionResult<WebResponseDto<string>>> GetToken([FromBody] AuthDto auth)
        {
            if(username.Equals(auth.username) && password.Equals(auth.password))
            {
                return Ok(new WebResponseDto<string>
                {
                    Code = HttpUtils.CODE_OK,
                    Message = HttpUtils.HTTPS_STATUS[HttpUtils.CODE_OK].Replace("{0}", "Token Generated."),
                    Body = await _jwtservice.GenerateToken(username, secreyKey)
                }); 
            }

            return Ok(new WebResponseDto<string>
            {
                Code = HttpUtils.CODE_NO_CONTENT,
                Message = HttpUtils.HTTPS_STATUS[HttpUtils.CODE_NO_CONTENT].Replace("{0}", "Invalid Credentials."),
                Body = string.Empty
            }); 
        }

    }
}
