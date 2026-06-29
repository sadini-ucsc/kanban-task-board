using FluentAssertions;
using kanban.application.services;
using kanban.domain.enums;
using kanban.domain.models;
using kanban.infrastructure.repositories;
using Moq;
using Xunit;

namespace kanban.application.tests.services
{
    public class KanbanTaskServiceDeleteTests
    {
        private readonly Mock<IKanbanTaskRepository> repositoryMock;
        private readonly KanbanTaskService service;

        public KanbanTaskServiceDeleteTests()
        {
            repositoryMock = new Mock<IKanbanTaskRepository>();
            service = new KanbanTaskService(repositoryMock.Object);
        }

        [Fact]
        public async Task DeleteAsync_ShouldDeleteTask_AndReturnTrue()
        {
            // Arrange
            var taskId = Guid.NewGuid();

            var existingTask = new KanbanTask
            {
                Id = taskId,
                Title = "Task",
                Description = "Desc",
                Status = KanbanTaskStatus.Todo,
                IsDeleted = false,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            repositoryMock
                .Setup(r => r.GetByIdAsync(taskId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(existingTask);

            repositoryMock
                .Setup(r => r.DeleteAsync(existingTask, It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await service.DeleteAsync(taskId, CancellationToken.None);

            // Assert
            result.Should().BeTrue();

            repositoryMock.Verify(
                r => r.DeleteAsync(existingTask, It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldThrow_WhenTaskNotFound()
        {
            // Arrange
            var taskId = Guid.NewGuid();

            repositoryMock
                .Setup(r => r.GetByIdAsync(taskId, It.IsAny<CancellationToken>()))
                .ReturnsAsync((KanbanTask?)null);

            // Act
            Func<Task> act = async () =>
                await service.DeleteAsync(taskId, CancellationToken.None);

            // Assert
            await act.Should()
                .ThrowAsync<KeyNotFoundException>()
                .WithMessage($"Task with id {taskId} not found");
        }
    }
}