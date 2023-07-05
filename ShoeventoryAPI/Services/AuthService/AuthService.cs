using ShoeventoryAPI.Models;
using ShoeventoryAPI.DTOs;

namespace ShoeventoryAPI.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly DataContext _db;

        public AuthService(DataContext db)
        {
            _db = db;
        }

        public async Task<Merchant> Register(MerchantDto merchantDto)
        {
            var merchant = new Merchant
            {
                MerchantName = merchantDto.MerchantName,
                Password = merchantDto.Password,
                Email = merchantDto.Email
            };
            _db.Merchants.Add(merchant);
            await _db.SaveChangesAsync();
            return merchant;
        }

        public async Task<Boolean> UserExists(string email)
        {
            if (await _db.Merchants.AnyAsync(x => x.Email == email))
            {
                return true;
            }
            return false;
        }
    }
}
