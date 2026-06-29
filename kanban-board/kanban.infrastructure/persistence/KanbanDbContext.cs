using kanban.domain.models;
using Microsoft.EntityFrameworkCore;

namespace kanban.infrastructure.persistence
{
    public class KanbanDbContext : DbContext
    {
        public KanbanDbContext(DbContextOptions<KanbanDbContext> options)
            : base(options)
        {
        }

        public DbSet<KanbanTask> KanbanTasks { get; set; } = null!;
    }
}
