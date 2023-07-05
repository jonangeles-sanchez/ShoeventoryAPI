using ShoeventoryAPI.Models;
using ShoeventoryAPI.DTOs;

namespace ShoeventoryAPI.Services.AuthService
{
    public interface IAuthService
    {
        Task<Merchant> Register(MerchantDto merchantDto, string hashedPass);
        Task<Boolean> UserExists(string email);
    }
}
