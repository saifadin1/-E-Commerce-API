using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace E_Commerce_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> userManager;
        private IConfiguration configurations;

        public AccountController(UserManager<AppUser> userManager , IConfiguration configurations)
        {
            this.userManager = userManager;
            this.configurations = configurations;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            if(ModelState.IsValid)
            {
                var user = new AppUser
                {
                    UserName = registerDTO.UserName,
                    Email = registerDTO.Email
                };
                IdentityResult res = await userManager.CreateAsync(user, registerDTO.Password); 
                if(res.Succeeded)
                {
                    return Ok();
                }
                return BadRequest(res.Errors.FirstOrDefault());
            }
            return BadRequest();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByNameAsync(loginDTO.UserName);
                if (user is AppUser)
                {
                    bool IsPasswordValid = await userManager.CheckPasswordAsync(user, loginDTO.Password);
                    if (IsPasswordValid)
                    {
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.NameIdentifier, user.Id),
                            new Claim(ClaimTypes.Name, user.UserName),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                        };
                        var roles = await userManager.GetRolesAsync(user);
                        foreach (var role in roles)
                        {
                            claims.Add(new Claim(ClaimTypes.Role, role));
                        }
                        SigningCredentials creds = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configurations["JWT:secret"])), SecurityAlgorithms.HmacSha256); 
                        JwtSecurityToken token = new JwtSecurityToken(
                            issuer: configurations["JWT:validIssuer"],
                            audience: configurations["JWT:validAudience"],
                            claims: claims,
                            expires: DateTime.Now.AddMinutes(30),
                            signingCredentials: creds
                            );
                        return Ok(new {msg = "Vaalid User" , token = new JwtSecurityTokenHandler().WriteToken(token) , expiration = token.ValidTo});
                    }
                }
            }
            return Unauthorized();
        }
    }
}
