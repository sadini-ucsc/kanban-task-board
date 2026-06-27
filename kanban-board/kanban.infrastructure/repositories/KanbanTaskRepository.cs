using kanban.domain.models;
using kanban.infrastructure.persistence;
using Microsoft.EntityFrameworkCore;

namespace kanban.infrastructure.repositories
{
    public class KanbanTaskRepository : IKanbanTaskRepository
    {
        private readonly KanbanDbContext dbContext;

        public KanbanTaskRepository(KanbanDbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<KanbanTask?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await dbContext.KanbanTasks
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == id && !t.IsDeleted, 
                cancellationToken);
        }

        public async Task<IEnumerable<KanbanTask>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await dbContext.KanbanTasks
                .AsNoTracking()
                .Where(t => !t.IsDeleted)
                .ToListAsync(cancellationToken);
        }

        public async Task<KanbanTask> CreateAsync(KanbanTask task, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(task);

            await dbContext.KanbanTasks.AddAsync(task, cancellationToken);

            await dbContext.SaveChangesAsync(cancellationToken);

            return task;
        }

        public async Task<KanbanTask> UpdateAsync(KanbanTask task, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(task);

            dbContext.KanbanTasks.Update(task);

            await dbContext.SaveChangesAsync(cancellationToken);

            return task;
        }

        public async Task DeleteAsync(KanbanTask task, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(task);

            dbContext.KanbanTasks.Remove(task);

            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
