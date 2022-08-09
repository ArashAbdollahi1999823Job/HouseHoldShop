namespace _0_Framework.Application
{
    public interface IAuthHelper
    {
        void SignOut();
        void SignIn(AuthViewModel command);

        bool IsAuthenticated();
    }
}
