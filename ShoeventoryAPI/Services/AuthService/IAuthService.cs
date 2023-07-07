using ShoeventoryAPI.Models;
using ShoeventoryAPI.DTOs;

namespace ShoeventoryAPI.Services.AuthService
{
    public interface IAuthService
    {
        Task<Merchant> Register(MerchantDto merchantDto, string hashedPass);
        Task<Boolean> UserExists(string email);
        Task<string> GetUserPassHash(string email);
        Task<string> GetUserName(string email);
        Task<Merchant> GetMe(string email);
        
    }
}
