using kanban.application.dtos;

namespace kanban.application.services
{
    public interface IKanbanTaskService
    {
        Task<KanbanTaskDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<IEnumerable<KanbanTaskDto>> GetAllAsync(CancellationToken cancellationToken);
        Task<KanbanTaskDto> CreateAsync(CreateKanbanTaskRequest request, CancellationToken cancellationToken);
        Task<KanbanTaskDto?> UpdateAsync(Guid id, UpdateKanbanTaskRequest request, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
