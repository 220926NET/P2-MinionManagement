using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Web.Helpers;

using DataAccess;

namespace Services;
public class AuthenticationService
{
    private IAuthenticationRepo _repo;
    public AuthenticationService(IAuthenticationRepo repo) {
        _repo = repo;
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
            string hash = _repo.GetHash(username)!;
            if (hash != null) {
                if (Crypto.VerifyHashedPassword(hash, password)) {
                    int userId = _repo.UserId(username);    // For the ID to be passed in token claim to keep track of user
                    return GenerateWebToken(userId.ToString());
                }
            }
        }
        return null;
    }

    private string GenerateWebToken(string id) {
        SymmetricSecurityKey securityKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("SuperSecretSecurityKey"));    
        SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);  

        List<Claim> tokenClaims = new List<Claim> {
            new Claim(ClaimTypes.Sid, id)
        };
    
        JwtSecurityToken token = new JwtSecurityToken(//null, 
                    claims: tokenClaims,   
                    expires: DateTime.Now.AddMinutes(120),    
                    signingCredentials: credentials);    

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}