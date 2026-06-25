using kanban.domain.enums;

namespace kanban.application.dtos
{
    public class UpdateKanbanTaskRequest
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public KanbanTaskStatus Status { get; set; }
    }
}
