namespace kanban.application.dtos
{
    public class CreateKanbanTaskRequest
    {
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;
    }
}
