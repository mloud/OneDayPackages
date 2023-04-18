namespace OneDay.User
{
    public interface IUser
    {
        string Id { get; }
    }
    public class User : IUser
    {
        public string Id { get; }

        public User(string id)
        {
            Id = id;
        }
    }
}