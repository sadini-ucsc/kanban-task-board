using kanban.application.dtos;
using kanban.domain.enums;
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

        public async Task<KanbanTaskDto> CreateAsync(CreateKanbanTaskRequest request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            if (string.IsNullOrWhiteSpace(request.Title))
            {
                throw new ArgumentException("Title is required.", nameof(request));
            }

            var entity = new KanbanTask
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                Description = request.Description,
                Status = KanbanTaskStatus.Todo,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            var createdTask = await repository.CreateAsync(entity, cancellationToken);

            return MapToDto(createdTask);
        }

        public async Task<KanbanTaskDto?> UpdateAsync(Guid id, UpdateKanbanTaskRequest request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            var entity = await repository.GetByIdAsync(id, cancellationToken);

            if (entity is null)
                return null;

            if (string.IsNullOrWhiteSpace(request.Title))
                throw new ArgumentException("Title is required.", nameof(request));

            if (entity.Status == KanbanTaskStatus.Done && request.Status != KanbanTaskStatus.Done)
            {
                throw new InvalidOperationException("Completed tasks cannot be moved back.");
            }

            entity.Title = request.Title;
            entity.Description = request.Description;
            entity.Status = request.Status;
            entity.UpdatedAt = DateTime.UtcNow;

            await repository.UpdateAsync(entity, cancellationToken);

            return MapToDto(entity);
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
