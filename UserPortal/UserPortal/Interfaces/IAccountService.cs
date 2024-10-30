using UserPortal.Models.Dto.Account;

namespace UserPortal.Interfaces
{
    public interface IAccountService
    {
        void RegisterUser(RegisterUserDto dto);
        string GenerateJwt(LoginDto dto);
        void UpdateUser(UpdateUserDto dto);
    }
}
