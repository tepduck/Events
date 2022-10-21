using Entities.DataTransferObjects;
using System.Threading.Tasks;

namespace Events
{
    public interface IAuthenticationManager
    {
        Task<bool> ValidateUser(UserForAuthenticationDto userForAuth);
        Task<string> CreateToken();
    }
}
