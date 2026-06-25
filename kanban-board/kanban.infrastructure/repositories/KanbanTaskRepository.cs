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
                .FirstOrDefaultAsync(t => t.Id == id, cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IEnumerable<KanbanTask>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await dbContext.KanbanTasks
                .AsNoTracking()
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);
        }
    }
}
