using Models;

namespace Application.Common.Interfaces.Security
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}
