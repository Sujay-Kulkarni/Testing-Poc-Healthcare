using Testing_Poc_Healthcare.Models;

namespace Testing_Poc_Healthcare.Interface
{
    public interface IUserService
    {
        UserInfoVM GetUserDetails(UserLogin userLogin, JwtInfo jwtInfo);
        bool AddUserDetails(UserInfo userInfo);
        string GenerateToken(UserInfoVM userDetails, JwtInfo jwtInfo);
    }
}
