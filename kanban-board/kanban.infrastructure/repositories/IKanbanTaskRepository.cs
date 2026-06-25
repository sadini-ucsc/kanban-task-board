using kanban.domain.models;

namespace kanban.infrastructure.repositories
{
    public interface IKanbanTaskRepository
    {
        Task<KanbanTask?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<IEnumerable<KanbanTask>> GetAllAsync(CancellationToken cancellationToken);
        Task<KanbanTask> CreateAsync(KanbanTask task, CancellationToken cancellationToken);
    }

}
