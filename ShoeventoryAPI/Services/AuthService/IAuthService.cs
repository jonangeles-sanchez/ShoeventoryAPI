using ShoeventoryAPI.Models;
using ShoeventoryAPI.DTOs;

namespace ShoeventoryAPI.Services.AuthService
{
    public interface IAuthService
    {
        Task<Merchant> Register(MerchantDto merchantDto);
        Task<Boolean> UserExists(string email);
    }
}
