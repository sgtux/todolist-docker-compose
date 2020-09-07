using System.Collections.Generic;
using Site.Config;
using Site.Models;
using Site.Repository;

namespace Site.Services
{
    public class TodoService : BaseService<TodoItem>
    {
        public TodoService(AppConfig config) : base(config) { }

        public IEnumerable<TodoItem> GetAll() => Query($"SELECT \"Id\", \"Description\", \"Done\" FROM \"{TableName}\" ORDER BY \"Id\"");

        public void Add(TodoItem todoItem)
        {
            var query = $"INSERT INTO \"{TableName}\" (\"Description\", \"Done\") VALUES (@Description, @Done)";
            Execute(query, todoItem);
        }

        public void Update(TodoItem todoItem)
        {
            var query = $"UPDATE \"{TableName}\" SET \"Description\" = @Description, \"Done\" = @Done WHERE \"Id\" = @Id";
            Execute(query, todoItem);
        }

        public void Remove(int id)
        {
            var query = $"DELETE FROM \"{TableName}\" WHERE \"Id\" = @Id";
            Execute(query, new { Id = id });
        }
    }
}