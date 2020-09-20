namespace Api.Models
{
    public abstract class BaseEntity
    {
        public long Id { get; set; }
        public abstract string EntityName { get; }
    }
}