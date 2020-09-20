using System.Collections.Generic;
using Api.Config;
using Api.Models;

namespace Api.Services
{
    public class UserService : BaseService<User>
    {
        private const string SELECT_COLUMNS = "\"Id\", \"Name\", \"Email\", \"Password\"";

        public UserService(AppConfig config) : base(config) { }

        public long Add(User user)
        {
            var query = $"INSERT INTO \"{TableName}\" (Name, Email, Password) VALUES (@Name, @Email, @Password)";
            Execute(query, user);
            return CurrentId(user);
        }

        public long Update(User user)
        {
            var query = $"UPDATE \"{TableName}\" SET \"Name\" = @Name, \"Email\" = @Email, \"Password\" = @Password, \"Document\" = @Document WHERE Id = @Id";
            Execute(query, user);
            return CurrentId(user);
        }

        public User FindByEmail(string email) => FirstOrDefault($"SELECT {SELECT_COLUMNS} FROM \"{TableName}\" WHERE \"Email\" = '{email}'");

        public User GetById(long id) => FirstOrDefault($"SELECT {SELECT_COLUMNS} FROM \"{TableName}\" WHERE Id = @Id", new { Id = id });

        public IEnumerable<User> GetAll() => Query($"SELECT {SELECT_COLUMNS} FROM \"{TableName}\"");
    }
}