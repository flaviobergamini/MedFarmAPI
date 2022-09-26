using MedFarmAPI.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MedFarmAPI.Services
{
    public class TokenService
    {
        private DecryptedTokenService decryptedTokenService = new DecryptedTokenService();
        public string GenerateClientToken(Client client)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(Configuration.JwtKey);

            var tokenDescriptor = new SecurityTokenDescriptor 
            {
                Subject = new ClaimsIdentity(new Claim[] { 
                    new Claim(ClaimTypes.Name, client.Name),
                    new Claim(ClaimTypes.Email, client.Email),
                    new Claim(ClaimTypes.Role, "Client"),
                    new Claim("id", client.Id.ToString()),
                }),

                Expires = DateTime.UtcNow.AddHours(8),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public string GenerateDoctorToken(Doctor doctor)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(Configuration.JwtKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.Name, doctor.Name),
                    new Claim(ClaimTypes.Email, doctor.Email),
                    new Claim(ClaimTypes.Role, "Doctor"),
                    new Claim("id", doctor.Id.ToString()),
                }),

                Expires = DateTime.UtcNow.AddHours(8),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public string GenerateDrugstoreToken(Drugstore drugstore)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(Configuration.JwtKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.Name, drugstore.Name),
                    new Claim(ClaimTypes.Email, drugstore.Email),
                    new Claim(ClaimTypes.Role, "Drugstore"),
                    new Claim("id", drugstore.Id.ToString()),
                }),

                Expires = DateTime.UtcNow.AddHours(8),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public DecryptedTokenService ReadToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(token);
            decryptedTokenService.Id = int.Parse(jwtSecurityToken.Claims.First(claim => claim.Type == "id").Value);
            decryptedTokenService.Name = jwtSecurityToken.Claims.First(claim => claim.Type == "name").Value;
            decryptedTokenService.Email = jwtSecurityToken.Claims.First(claim => claim.Type == "email").Value;
            decryptedTokenService.Role = jwtSecurityToken.Claims.First(claim => claim.Type == "role").Value;
            return decryptedTokenService;
        }
    }
}
