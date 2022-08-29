using Local.DTOS.Account;
using Local.Models;
using Local.Models.Data;
using Local.Settings;
using Mapster;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Local.Services
{
    public class UserService : IUserService
    {
        private readonly LocaldbContext context;
        private readonly JwtSetting jwtSetting;

        public UserService(LocaldbContext context, JwtSetting jwtSetting)
        {
            this.context = context;
            this.jwtSetting = jwtSetting;
        }
        public string GenerateToken(User user)
        {
            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub,user.UserEmail),
                new Claim("email",user.UserEmail),
                new Claim("role",user.Role.RoleName),
                //new Claim("additonal","TestSomething"),
                new Claim("todoDay", DateTime.Now.ToString())
            };
            return BuildToken(claims);
        }

        private string BuildToken(Claim[] claims)
        {
            var expires = DateTime.Now.AddDays(Convert.ToDouble(jwtSetting.Expire));
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSetting.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //สร้าง Token
            var token = new JwtSecurityToken(
                issuer: jwtSetting.Issuer,
                audience: jwtSetting.Audience,
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public User GetInfo(string accessToken)
        {
            var token = new JwtSecurityTokenHandler().ReadToken(accessToken) as JwtSecurityToken;

            // key is case-sensitive
            var userEmail = token.Claims.First(claim => claim.Type == "sub").Value;
            var role = token.Claims.First(claim => claim.Type == "role").Value;

            var user = new User
            {
                UserEmail = userEmail,
                Role = new Role
                {
                    RoleName = role
                }
            };

            return user;
        }

        //public async Task<User> Login(string UserEmail, string UserPassword)
        //{
        //    var user = await localdbContext.Users.Include(x => x.Role)
        //        .SingleOrDefaultAsync(p => p.UserEmail == UserEmail);

        //    if (user != null && VerifyPassword(user.UserPassword, UserPassword))
        //    {
        //        return user;
        //    }
        //    return null;
        //}

        public async Task<object> Login(LoginRequest data)
        {
            var result = await context.Users.Include(a => a.Role).FirstOrDefaultAsync(a =>
                a.UserEmail.Equals(data.UserEmail) && a.UserIsuse== "1"
            );
            if (result != null)
            {
                var salt = await context.SecurityUsers.FindAsync(result.UserId);
                if (VerifyPassword(result.UserPassword, data.UserPassword, salt!.Salt))
                    return Constants.Return200Token("เข้าสู่ระบบสำเร็จ", GenerateToken(result));
            }
            return Constants.Return400("ชื่อผู้ใช้หรือรหัสผ่านไม่ถูกต้อง");
        }

        //public async Task Register(User user)
        //{
        //    var resuilt = await context.Users.SingleOrDefaultAsync(x => x.UserEmail == user.UserEmail);
        //    ///////////////////////////////////////////////////////////////////
        //    if (resuilt != null) throw new Exception("Not exist Account");

        //    user.UserPassword = CreateHashPassword(user.UserPassword);
        //    await context.Users.AddAsync(user);
        //    await context.SaveChangesAsync();


        //    //throw new NotImplementedException();
        //}

        public async Task<object> Register(RegisterRequest data)
        {
            var result = await context.Users.FirstOrDefaultAsync(a => a.UserEmail.Equals(data.UserEmail));
            if (result != null) return Constants.Return400("อีเมลผู้ใช้ซ้ำ");

            //#region Check and UploadImage
            //(string errorMessage, string imageName) = await UpLoadImage(data.ProfileImage!);
            //if (!string.IsNullOrEmpty(errorMessage)) return Constants.Return400(errorMessage);
            //#endregion

            (string passwordHash, string salt) = CreateHashPasswordAndSalt(data.UserPassword);

            #region New Account
            User newAccount = data.Adapt<User>();
            newAccount.UserEmail = newAccount.UserEmail.ToLower();
            newAccount.UserPassword = passwordHash;
            //newAccount.ProfileImage = imageName;
            newAccount.UserId = Constants.uuid18();
            newAccount.UserCreate = DateTime.Now;
            newAccount.UserIsuse = "1";
            #endregion

            #region New Salt
            SecurityUser newSalt = new SecurityUser
            {
                UserId = newAccount.UserId,
                TruePassword = data.UserPassword,
                Salt = salt
            };
            #endregion

            await context.Users.AddAsync(newAccount);
            await context.SecurityUsers.AddAsync(newSalt);
            await context.SaveChangesAsync();
            return Constants.Return200("สมัครสมาชิกสำเร็จ");
        }

        //private bool VerifyPassword(string saltAndHashFromDB, string UserPassword)
        //{
        //    var parts = saltAndHashFromDB.Split('.', 2);
        //    if (parts.Length != 2) return false;

        //    var salt = Convert.FromBase64String(parts[0]);
        //    var passwordHash = parts[1];
        //    var hashed = HashPassword(UserPassword, salt);


        //    return hashed == passwordHash;
        //}
        private bool VerifyPassword(string passwordHashFormDB, string UserPassword, string salt)
        {
            if (string.IsNullOrEmpty(salt)) return false;
            string newHashed = HashPassword(UserPassword, Convert.FromBase64String(salt));
            return newHashed == passwordHashFormDB;
        }

        private string CreateHashPassword(string UserPassword)
        {
            byte[] salt = new byte[128 / 8];
            using (var rngCsp = new RNGCryptoServiceProvider())
            {
                rngCsp.GetNonZeroBytes(salt);
            }
            Console.WriteLine($"Salt: {Convert.ToBase64String(salt)}");

            var hashed = HashPassword(UserPassword, salt);


            var hpw = $"{Convert.ToBase64String(salt)}.{hashed}";
            return hpw;
        }

        private (string passwordHash, string salt) CreateHashPasswordAndSalt(string password)
        {
            byte[] salt = new byte[128 / 8];
            using (var rngCsp = new RNGCryptoServiceProvider()) rngCsp.GetNonZeroBytes(salt);

            string hashed = HashPassword(password, salt);
            return (hashed, Convert.ToBase64String(salt));
        }

        private string HashPassword(string UserPassword, byte[] salt)
        {
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: UserPassword,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));
            return hashed;
        }

        public async Task<IEnumerable<User>> FindAll()
        {
            var users = await context.Users.
                Include(x => x.Role).ToListAsync();

            return users;
        }

        public async Task<object> GetByToken(string userToken)
        {
            if (userToken is null) return new { statusCode = 400, message = "no token" };

            var token = new JwtSecurityTokenHandler().ReadToken(userToken) as JwtSecurityToken;

            var id = token!.Claims.First(claim => claim.Type == "userId").Value;
            var result = await context.Users.Include(a => a.Role).FirstOrDefaultAsync(a => a.UserId.Equals(int.Parse(id)));
            if (result is null) return new { statusCode = 400, message = "no user" };
            return new { statusCode = 200, message = "success", data = UserResponse.FromUser(result) };
        }
    }
}
