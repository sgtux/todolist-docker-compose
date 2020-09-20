using System.Text.Json.Serialization;

namespace Api.Models
{
    public class TodoItem : BaseEntity
    {
        public string Description { get; set; }

        public bool Done { get; set; }

        public int UserId { get; set; }

        [JsonIgnore]
        public override string EntityName => "TodoItem";
    }
}