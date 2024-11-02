using Models;

namespace Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser appUser);
    }
}