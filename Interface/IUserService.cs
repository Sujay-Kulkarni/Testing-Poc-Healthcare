using Testing_Poc_Healthcare.Models;

namespace Testing_Poc_Healthcare.Interface
{
    public interface IUserService
    {
        UserInfoVM GetUserDetails(UserLogin userLogin);
        bool AddUserDetails(UserInfo userInfo);
        bool AddRole(Role role);
    }
}
