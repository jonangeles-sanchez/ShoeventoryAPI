using ShoeventoryAPI.Models;
using ShoeventoryAPI.DTOs;

namespace ShoeventoryAPI.Services.AuthService
{
    public class IAuthService
    {
        Task<Merchant> Register(MerchantDto merchantDto);
        Task<Boolean> UserExists(string email);
    }
}
