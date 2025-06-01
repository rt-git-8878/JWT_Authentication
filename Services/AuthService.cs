using JWT_Authentication.Context;
using JWT_Authentication.Interfaces;
using JWT_Authentication.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWT_Authentication.Services
{
    public class AuthService : IAuthService
    {
        private readonly JwtContext _jwtService;
        private readonly IConfiguration _configuration;
        public AuthService(JwtContext jwtContext,IConfiguration configuration)
        {
            _jwtService = jwtContext;
            _configuration = configuration;
        }
        public Role AddRole(Role role)
        {
            var addedRole = _jwtService.Roles.Add(role);
            _jwtService.SaveChanges();
            return addedRole.Entity;
        }
        public User AddUser(User user)
        {
            var addedUser = _jwtService.Users.Add(user);
            _jwtService.SaveChanges();
            return addedUser.Entity;
        }
        public bool AssignRoleToUser(AddUserRole obj)
        {
            try
            {
                var addRoles = new List<UserRole>();
                var user = _jwtService.Users.SingleOrDefault(s => s.Id == obj.UserId);
                if (user == null)
                    throw new Exception("user is not valid");
                foreach (int role in obj.RoleIds)
                {
                    var userRole = new UserRole();
                    userRole.RoleId = role;
                    userRole.UserId = user.Id;
                    addRoles.Add(userRole);
                }
                _jwtService.UserRoles.AddRange(addRoles);
                _jwtService.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public string Login(RequestModel.LoginRequestEntity loginRequestEntity)
        {
            if (loginRequestEntity.Username != null && loginRequestEntity.Password != null)
            {
                var user=_jwtService.Users.SingleOrDefault(s =>s.Username == loginRequestEntity.Username && s.Password ==loginRequestEntity.Password );
                if(user != null)
                {
                    var claims = new[] { 
                    new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                    new Claim("Id",user.Id.ToString()),
                    new Claim("UserName",user.Name.ToString())
                   };
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var signIn=new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        _configuration["Jwt:Issuer"],
                        _configuration["Jwt:Audience"],
                        claims,
                        expires:DateTime.UtcNow.AddMinutes(10),
                        signingCredentials: signIn);
                     var jwtToken=new JwtSecurityTokenHandler().WriteToken(token);
                    return jwtToken;
                }
                else
                {
                    throw new Exception("User is not valid");
                }
            }
            else
            {
                throw new Exception("Credential are not valid");
            }
            
        }
    }
}
