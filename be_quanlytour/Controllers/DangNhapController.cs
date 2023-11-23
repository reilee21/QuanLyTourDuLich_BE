using be_quanlytour.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace be_quanlytour.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DangNhapController : ControllerBase
    {
        private readonly QltourDuLichContext _context;

        public DangNhapController(QltourDuLichContext context)
        {
            _context = context;
        }
        [HttpPost("RefreshToken")]
        public IActionResult RefreshToken(string oldtoken)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes("zzzzhdz02x1ta1urertv01vwxchjb4h1");

                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false, // Bạn có thể kiểm tra Issuer nếu cần
                    ValidateAudience = false, // Bạn có thể kiểm tra Audience nếu cần
                    ValidateLifetime = false, // Chúng ta sẽ tự kiểm tra thời gian hết hạn
                };

                // Giải mã mã thông báo cũ
                SecurityToken validatedToken;
                var principal = tokenHandler.ValidateToken(oldtoken, tokenValidationParameters, out validatedToken);

                // Kiểm tra thời gian hết hạn của mã thông báo cũ (nếu cần)
                if (validatedToken.ValidTo < DateTime.UtcNow)
                {
                    throw new SecurityTokenException("Mã thông báo đã hết hạn");
                }

                var username = principal.Identity.Name;
                var userRole = principal.Claims.First(claim => claim.Type == ClaimTypes.Role).Value;
                var userMail = principal.Claims.First(claim => claim.Type == ClaimTypes.Email).Value;
                var userIdClaim = principal.Claims.FirstOrDefault(claim => claim.Type == "id");
                string userId = "";
                if (userIdClaim != null)
                {
                    userId = userIdClaim.Value;
                }
                var newToken = GenerateJWTToken(username, userRole,userMail, userId);

                return Ok(new { Token = newToken });
            }
            catch (Exception ex)
            {
                // Xử lý lỗi
                Console.WriteLine("Lỗi khi làm mới mã thông báo: " + ex.Message);
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult DangNhap(string username, string password)
        {
            var nhanVien = _context.TaiKhoanNvs.SingleOrDefault(t => t.Username == username && t.Password ==password);
            var khachHang = _context.TaiKhoans.SingleOrDefault(t => t.Username == username && t.Password == password);
            
            if (nhanVien != null)
            {
                var infonv = _context.NhanViens.SingleOrDefault(t => t.MaNv == nhanVien.MaNv);
                var token = GenerateJWTToken(username,infonv.ChucVu,infonv.Email,infonv.MaNv);
                return Ok(new { Token = token});
            }
            else if (khachHang != null)
            {
                var infoKH = _context.KhachHangs.SingleOrDefault(t => t.MaKh == khachHang.MaKh);
                var token = GenerateJWTToken(username, "client", infoKH.Email, infoKH.MaKh) ;
                return Ok(new { Token = token });
            }

            return Unauthorized();
        }
        private string GenerateJWTToken(string username,string role,string email,string id)
        {
            var jwttoken = new JwtSecurityTokenHandler();
            var skbyte = Encoding.UTF8.GetBytes("zzzzhdz02x1ta1urertv01vwxchjb4h1");
            var tokendes = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.Role,role),
                    new Claim(ClaimTypes.Email, email), // Add email as a claim
                     new Claim("id", id.ToString()),
                    new Claim("TokenId",Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(skbyte),SecurityAlgorithms.HmacSha512Signature)
                
            };
            var token = jwttoken.CreateToken(tokendes);
            return jwttoken.WriteToken(token);
        }

    }
}
