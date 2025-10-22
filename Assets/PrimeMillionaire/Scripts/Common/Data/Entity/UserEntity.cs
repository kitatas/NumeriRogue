namespace PrimeMillionaire.Common.Data.Entity
{
    public sealed class UserEntity
    {
        private UserVO _user;

        public void Set(UserVO user)
        {
            _user = user;
        }
    }
}