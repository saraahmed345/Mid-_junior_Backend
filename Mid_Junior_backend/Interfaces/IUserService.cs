namespace Mid_Junior_backend.Interfaces
{
    public interface IUserService
    {
        Task<User> CreateUser(User user);
        Task<User> GetUserById(int id);
        Task<List<User>> GetUsers(UserFilter filter);
        Task UpdateUser(int id, User user);
        Task DeleteUser(int id);
    }
}
