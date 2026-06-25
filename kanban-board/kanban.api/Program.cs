using kanban.application.services;
using kanban.domain.enums;
using kanban.domain.models;
using kanban.infrastructure.persistence;
using kanban.infrastructure.repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configure services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

// Register DbContext (InMemory)
builder.Services.AddDbContext<KanbanDbContext>(options =>
    options.UseInMemoryDatabase("KanbanDb"));

// Application / Infrastructure registrations
builder.Services.AddScoped<IKanbanTaskRepository, KanbanTaskRepository>();
builder.Services.AddScoped<IKanbanTaskService, KanbanTaskService>();

var app = builder.Build();

// Seed data
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<KanbanDbContext>();
    db.Database.EnsureCreated();
    if (!db.KanbanTasks.Any())
    {
        db.KanbanTasks.AddRange(
            new KanbanTask
            {
                Id = Guid.Parse("550e8400-e29b-41d4-a716-446655440001"),
                Title = "Design schema",
                Description = "Design initial database schema",
                Status = KanbanTaskStatus.Todo,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new KanbanTask
            {
                Id = Guid.Parse("550e8400-e29b-41d4-a716-446655440002"),
                Title = "Implement application layer",
                Description = "Create task services",
                Status = KanbanTaskStatus.InProgress,
                CreatedAt = DateTime.UtcNow.AddHours(-4),
                UpdatedAt = DateTime.UtcNow.AddHours(-1)
            },
            new KanbanTask
            {
                Id = Guid.Parse("550e8400-e29b-41d4-a716-446655440003"),
                Title = "Implement API",
                Description = "Create endpoints for tasks",
                Status = KanbanTaskStatus.Done,
                CreatedAt = DateTime.UtcNow.AddDays(-1),
                UpdatedAt = DateTime.UtcNow.AddHours(-2)
            }
        );
        db.SaveChanges();
    }
}

// Configure middleware
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
