using kanban.application.Dtos;

namespace kanban.application.Services
{
    public interface IKanbanTaskService
    {
        Task<KanbanTaskDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<IEnumerable<KanbanTaskDto>> GetAllAsync(CancellationToken cancellationToken);
    }
}
