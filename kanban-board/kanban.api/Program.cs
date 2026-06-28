using kanban.api.middleware;
using kanban.application.services;
using kanban.domain.enums;
using kanban.domain.models;
using kanban.infrastructure.persistence;
using kanban.infrastructure.repositories;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using FluentValidation.AspNetCore;

using kanban.application.validators;

var builder = WebApplication.CreateBuilder(args);

var frontendUrls = builder.Configuration
    .GetSection("FrontendUrls")
    .Get<string[]>() ?? Array.Empty<string>();

// Configure services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

builder.Services.AddValidatorsFromAssemblyContaining<CreateKanbanTaskRequestValidator>();
builder.Services.AddFluentValidationAutoValidation();

// Register DbContext (InMemory)
builder.Services.AddDbContext<KanbanDbContext>(options => options.UseInMemoryDatabase("KanbanDb"));

// Application / Infrastructure registrations
builder.Services.AddScoped<IKanbanTaskRepository, KanbanTaskRepository>();
builder.Services.AddScoped<IKanbanTaskService, KanbanTaskService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("ReactApp", policy =>
    {
        policy
            .WithOrigins(frontendUrls)
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

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

    app.UseSwagger();
    app.UseSwaggerUI();

    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseCors("ReactApp");

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapControllers();

app.Run();
