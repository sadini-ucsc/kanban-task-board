using FluentAssertions;
using kanban.application.services;
using kanban.domain.enums;
using kanban.domain.models;
using kanban.infrastructure.repositories;
using Moq;
using Xunit;

namespace kanban.application.tests.services
{
    public class KanbanTaskServiceGetByIdTests
    {
        private readonly Mock<IKanbanTaskRepository> repositoryMock;
        private readonly KanbanTaskService service;

        public KanbanTaskServiceGetByIdTests()
        {
            repositoryMock = new Mock<IKanbanTaskRepository>();
            service = new KanbanTaskService(repositoryMock.Object);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnTask_WhenExists()
        {
            // Arrange
            var taskId = Guid.NewGuid();

            var task = new KanbanTask
            {
                Id = taskId,
                Title = "Test Task",
                Description = "Test Desc",
                Status = KanbanTaskStatus.Todo,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            repositoryMock
                .Setup(r => r.GetByIdAsync(taskId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(task);

            // Act
            var result = await service.GetByIdAsync(taskId, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result!.Id.Should().Be(taskId);
            result.Title.Should().Be("Test Task");
            result.Description.Should().Be("Test Desc");
            result.Status.Should().Be(KanbanTaskStatus.Todo);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldThrow_WhenNotFound()
        {
            // Arrange
            var taskId = Guid.NewGuid();

            repositoryMock
                .Setup(r => r.GetByIdAsync(taskId, It.IsAny<CancellationToken>()))
                .ReturnsAsync((KanbanTask?)null);

            // Act
            Func<Task> act = async () =>
                await service.GetByIdAsync(taskId, CancellationToken.None);

            // Assert
            await act.Should()
                .ThrowAsync<KeyNotFoundException>()
                .WithMessage($"Task with id {taskId} not found");
        }
    }
}