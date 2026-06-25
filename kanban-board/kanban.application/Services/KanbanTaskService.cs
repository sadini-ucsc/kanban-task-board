using kanban.application.dtos;
using kanban.domain.models;
using kanban.infrastructure.repositories;

namespace kanban.application.services
{
    public class KanbanTaskService : IKanbanTaskService
    {
        private readonly IKanbanTaskRepository repository;

        public KanbanTaskService(IKanbanTaskRepository repository)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<KanbanTaskDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var entity = await repository.GetByIdAsync(id, cancellationToken);

            if (entity is null)
                return null;

            return MapToDto(entity);
        }

        public async Task<IEnumerable<KanbanTaskDto>> GetAllAsync(CancellationToken cancellationToken)
        {
            var entities = await repository.GetAllAsync(cancellationToken);

            return entities.Select(MapToDto).ToList();
        }

        private static KanbanTaskDto MapToDto(KanbanTask item)
        {
            return new KanbanTaskDto
            {
                Id = item.Id,
                Title = item.Title,
                Description = item.Description,
                Status = item.Status,
                CreatedAt = item.CreatedAt,
                UpdatedAt = item.UpdatedAt
            };
        }
    }
}
