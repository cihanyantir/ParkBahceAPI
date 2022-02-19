using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ParkBahceAPI.Abstract;
using ParkBahceAPI.Data;
using ParkBahceAPI.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ParkBahceAPI.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly AppSettings _appsettings;

        public UserRepository(ApplicationDbContext db, IOptions<AppSettings> appsettings)
        {
            _db = db;
            _appsettings = appsettings.Value;
        }

        public User Authenticate(string username, string password) //USER WITH TOKEN
        {
            var user = _db.Users.SingleOrDefault(x => x.Username == username && x.Password == password);
            if (user == null)
                return null;
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appsettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role) //Claim ROLES.
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor); //created token with SecurityTokenDescriptor object properties by JwtSecurityTokenHandler object.
            user.Token = tokenHandler.WriteToken(token);
            return user;
        }

        public bool IsUniqueUser(string username)
        {
            bool user = _db.Users.Any(x => x.Username == username);
            if (user)
            { return false; }
            return true;
        }

        public User Register(string username, string password)
        {
            User userobject = new User()
            {
                Username = username,
                Password = password,
                Role="Admin",

            };
            _db.Users.Add(userobject);
            _db.SaveChanges();
            userobject.Password = "";
            return userobject;
        }
    }
}
