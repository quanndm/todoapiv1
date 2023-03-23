namespace TodoApi.Models
{
    public class Todo
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Content { get; set; } = string.Empty;

        public bool IsDone { get; set; } = false;
    }
}
