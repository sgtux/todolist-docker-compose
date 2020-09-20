using System.Collections.Generic;
using Api.Config;
using Api.Models;

namespace Api.Services
{
    public class TodoService : BaseService<TodoItem>
    {
        public TodoService(AppConfig config) : base(config) { }

        public IEnumerable<TodoItem> GetAll(int userId) => Query($"SELECT \"Id\", \"Description\", \"Done\" FROM \"{TableName}\" WHERE \"UserId\"=@UserId ORDER BY \"Id\"", new { UserId = userId });

        public void Add(TodoItem todoItem)
        {
            var query = $"INSERT INTO \"{TableName}\" (\"Description\", \"Done\", \"UserId\") VALUES (@Description, @Done, @UserId)";
            Execute(query, todoItem);
        }

        public void Update(TodoItem todoItem)
        {
            var query = $"UPDATE \"{TableName}\" SET \"Description\" = @Description, \"Done\" = @Done, \"UserId\" = @UserId WHERE \"Id\" = @Id";
            Execute(query, todoItem);
        }

        public void Remove(int id, int userId)
        {
            var query = $"DELETE FROM \"{TableName}\" WHERE \"Id\" = @Id AND \"UserId\" = @UserId";
            Execute(query, new { Id = id, UserId = userId });
        }
    }
}