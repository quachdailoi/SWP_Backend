﻿using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SECapstoneEvaluation.APIs.JwtFeatures;
using SECapstoneEvaluation.APIs.Services.Constracts;

namespace SECapstoneEvaluation.APIs.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly IUserService service;
        private readonly JwtHandler jwtHandler;

        public UsersController(IConfiguration configuration, IUserService service)
        {
            this.configuration = configuration;
            this.service = service;
            jwtHandler = new JwtHandler(configuration);
        }

        
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]string idToken, int campusId)
        {
            FirebaseAuth firebaseAuth = FirebaseAuth.DefaultInstance;

            FirebaseToken verifiedIdToken = await firebaseAuth.VerifyIdTokenAsync(idToken);

            if (verifiedIdToken == null) return BadRequest("Login failed.");

            IReadOnlyDictionary<string, dynamic> claims = verifiedIdToken.Claims;

            string email = claims["email"];

            var user = await service.GetUserByEmail(email);

            if (user == null) return BadRequest("Login failed - Not found email account in our system.");

            if (user.CampusId != campusId)
            {
                return BadRequest("Login failed - User account is not access to this campus.");
            }

            string token = jwtHandler.GenerateToken(user);

            return Ok(token);
        }

        [Authorize]
        [HttpGet("test-auth")]
        public void Test()
        {
            return;
        }
    }    
}