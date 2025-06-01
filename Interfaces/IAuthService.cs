using JWT_Authentication.Models;
using JWT_Authentication.RequestModel;
using Microsoft.AspNetCore.Identity.Data;
using System.Data;

namespace JWT_Authentication.Interfaces
{
    public interface IAuthService
    {
        User AddUser(User user);
        string Login(LoginRequestEntity loginRequestEntity);
        Role AddRole(Role role);
        bool AssignRoleToUser(AddUserRole obj);
    }
}
