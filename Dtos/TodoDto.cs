namespace TodoApi.Dtos
{
    public class TodoDto
    {
        public string? Id { get; set; }

        public string? Title {get; set;} = string.Empty;

        public string? Content { get; set; }

        public string? Category {get; set;} = string.Empty;

        public string? Date_created {get; set; } = string.Empty;
    }
}
