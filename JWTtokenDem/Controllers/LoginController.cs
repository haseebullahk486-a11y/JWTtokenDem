using JWTtokenDem.Data;
using JWTtokenDem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWTtokenDem.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly IConfiguration configuration;
        private readonly MyDbContext context;
        public LoginController(MyDbContext _context ,UserManager<User> _userManager, SignInManager<User> _signInManager,IConfiguration _configuration)
        {
            this.context = _context;
            this.userManager = _userManager;
            this.signInManager = _signInManager;
            this.configuration = _configuration;
        }
    
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] User user)
        {

                var adduser = await userManager.CreateAsync(user, user.Password);
                if (adduser.Succeeded)
                {
                    var CreateRoleForUser = await userManager.AddToRoleAsync(user, "User");
                    if(CreateRoleForUser.Succeeded)
                    {
                      return RedirectToAction("Login");
                }
                    else
                    {
                        return View();
                    }
                }
                else
                {
                    return View();
                } 
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] User user)
        {
            var data = await context.User.SingleOrDefaultAsync(x => x.Email == user.Email);

            var LoginUser = await signInManager.PasswordSignInAsync(data.UserName, user.Password, false, false);
            if (LoginUser.Succeeded)
            {
                var token = GenerateJwtToken(data);
                return Ok(new { token });
            }
            else
            {
                return View();
            }
        }
        private string GenerateJwtToken(User user)
        {
            IConfigurationSection jwtSettings = configuration.GetSection("Jwt");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);


            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim("id", user.Id.ToString())
            },
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["ExpireMinutes"])),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login", "Login"); 
        }
    }
}
