using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Web.Helpers;

using DataAccess;

namespace Services;

public class AuthenticationService
{
    private IAuthenticationRepo _repo;
    public AuthenticationService() {
        _repo = new AuthenticationRepo();           // FOR dependency injection, COULD switch out with initialized factory??
    }

    public bool Register(string username, string password) {
        if (!_repo.UsernameExists(username)) {
            string hash = Crypto.HashPassword(password);
            _repo.NewLogIn(username, hash);
            _repo.NewProfile(username);
            return true;
        }
        return false;
    }

    public string? LogIn(string username, string password) {
        if (_repo.UsernameExists(username)) {
            string hash = Crypto.HashPassword(password);
            if (_repo.VerifyCredentials(username, hash)) {
                // Can ALSO use: Crypto.VerifyHashedPassword(hash, password) TO COMPARE un-hashed password to hashed password
                return GenerateWebToken();
                //return _repo.UserId(username);    // Does the ID need to be passed in header to keep track of user??? (if so, make new model & return both JWT and ID) OR USE Jwt Claims
            }
        }
        return null;
    }

    private string GenerateWebToken() {
        SymmetricSecurityKey securityKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("SuperSecretKey"));    
        SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);    
    
        JwtSecurityToken token = new JwtSecurityToken(//null,    
                    expires: DateTime.Now.AddMinutes(120),    
                    signingCredentials: credentials);    

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}