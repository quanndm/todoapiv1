namespace TodoApi.Models
{
    public class Todo
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Title {get; set;} = string.Empty;

        public string Content { get; set; } = string.Empty;

        public string Category {get; set;} = string.Empty;

        public string Date_created {get; set; } = DateTime.Now.ToString();
        
        public string Last_update {get; set;} = DateTime.Now.ToString();

        public bool IsDone { get; set; } = false;
    }
}
