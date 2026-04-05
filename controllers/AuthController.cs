using Microsoft.AspNetCore.Mvc;
using fitness_tracker.DTOs;
using fitness_tracker.Data;
using fitness_tracker.models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace fitness_tracker.controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;

        public AuthController(AppDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto dto)
        {
            var athlete = _context.Athletes.FirstOrDefault(a => a.Email == dto.Email);

            if (athlete != null)
            {
                var athletePasswordValid = BCrypt.Net.BCrypt.Verify(dto.Password, athlete.Password);

                if (!athletePasswordValid)
                    return Unauthorized(new { message = "Invalid credentials" });

                var athleteToken = GenerateToken(athlete);
                SetJwtCookie(athleteToken);

                return Ok(new { message = "Login successful" });
            }

            var coach = _context.Coaches.FirstOrDefault(c => c.Email == dto.Email);

            if (coach != null)
            {
                var coachPasswordValid = BCrypt.Net.BCrypt.Verify(dto.Password, coach.Password);

                if (!coachPasswordValid)
                    return Unauthorized(new { message = "Invalid credentials" });

                var coachToken = GenerateToken(coach);
                SetJwtCookie(coachToken);

                return Ok(new { message = "Login successful" });
            }

            return Unauthorized(new { message = "Invalid credentials" });
        }

        private string GenerateToken(dynamic user)
        {
            var jwtSettings = _config.GetSection("Jwt");
            var keyString = jwtSettings["Key"] ?? throw new Exception("JWT Key is missing.");
            var key = Encoding.ASCII.GetBytes(keyString);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.FullName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim("UserId", user.Id.ToString())
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(2),
                Issuer = jwtSettings["Issuer"],
                Audience = jwtSettings["Audience"],
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        [HttpPost("signup/athlete")]
        public IActionResult SignupAthlete([FromBody] AthleteSignupDto dto)
        {
            var athleteEmailExists = _context.Athletes.Any(a => a.Email == dto.Email);
            var coachEmailExists = _context.Coaches.Any(c => c.Email == dto.Email);

            if (athleteEmailExists || coachEmailExists)
                return BadRequest(new { message = "Email already exists." });

            var athlete = new Athlete
            {
                FullName = dto.FullName,
                Email = dto.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Role = "Athlete"
            };

            _context.Athletes.Add(athlete);
            _context.SaveChanges();

            var token = GenerateToken(athlete);
            SetJwtCookie(token);

            return Ok(new { message = "Athlete account created successfully." });
        }
        [HttpPost("signup/coach")]
        public IActionResult SignupCoach([FromBody] CoachSignupDto dto)
        {
            var athleteEmailExists = _context.Athletes.Any(a => a.Email == dto.Email);
            var coachEmailExists = _context.Coaches.Any(c => c.Email == dto.Email);

            if (athleteEmailExists || coachEmailExists)
                return BadRequest(new { message = "Email already exists." });

            var coach = new Coach
            {
                FullName = dto.FullName,
                Email = dto.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Role = "Coach"
            };

            _context.Coaches.Add(coach);
            _context.SaveChanges();

            var token = GenerateToken(coach);
            SetJwtCookie(token);

            return Ok(new { message = "Coach account created successfully." });
        }

        private void SetJwtCookie(string token)
        {
            Response.Cookies.Append("token", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = false, // change to true in production with HTTPS
                SameSite = SameSiteMode.Strict,
                Expires = DateTimeOffset.UtcNow.AddHours(2)
            });
        }

        private void ClearJwtCookie()
        {
            Response.Cookies.Delete("token");
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            ClearJwtCookie();
            return Ok(new { message = "Logged out successfully" });
        }
    }
}