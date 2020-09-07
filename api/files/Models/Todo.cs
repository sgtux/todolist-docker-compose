using System.Text.Json.Serialization;

namespace Site.Models
{
    public class TodoItem : BaseEntity
    {
        public string Description { get; set; }

        public bool Done { get; set; }

        [JsonIgnore]
        public override string EntityName => "TodoItem";
    }
}