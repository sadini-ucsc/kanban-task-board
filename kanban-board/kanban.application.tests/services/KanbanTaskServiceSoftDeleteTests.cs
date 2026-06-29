using FluentAssertions;
using kanban.application.services;
using kanban.domain.enums;
using kanban.domain.models;
using kanban.infrastructure.repositories;
using Moq;
using Xunit;

namespace kanban.application.tests.services
{
    public class KanbanTaskServiceSoftDeleteTests
    {
        private readonly Mock<IKanbanTaskRepository> repositoryMock;
        private readonly KanbanTaskService service;

        public KanbanTaskServiceSoftDeleteTests()
        {
            repositoryMock = new Mock<IKanbanTaskRepository>();
            service = new KanbanTaskService(repositoryMock.Object);
        }

        [Fact]
        public async Task SoftDeleteAsync_ShouldMarkTaskAsDeleted_AndReturnTrue()
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
                CreatedAt = DateTime.UtcNow.AddDays(-1),
                UpdatedAt = DateTime.UtcNow.AddDays(-1)
            };

            KanbanTask? captured = null;

            repositoryMock
                .Setup(r => r.GetByIdAsync(taskId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(existingTask);

            repositoryMock
                .Setup(r => r.UpdateAsync(It.IsAny<KanbanTask>(), It.IsAny<CancellationToken>()))
                .Callback<KanbanTask, CancellationToken>((t, _) =>
                {
                    captured = t;
                })
                .ReturnsAsync((KanbanTask t, CancellationToken _) => t);

            // Act
            var result = await service.SoftDeleteAsync(taskId, CancellationToken.None);

            // Assert
            result.Should().BeTrue();

            captured.Should().NotBeNull();
            captured!.IsDeleted.Should().BeTrue();
            captured.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(5));

            repositoryMock.Verify(
                r => r.UpdateAsync(It.IsAny<KanbanTask>(), It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [Fact]
        public async Task SoftDeleteAsync_ShouldThrow_WhenTaskNotFound()
        {
            // Arrange
            var taskId = Guid.NewGuid();

            repositoryMock
                .Setup(r => r.GetByIdAsync(taskId, It.IsAny<CancellationToken>()))
                .ReturnsAsync((KanbanTask?)null);

            // Act
            Func<Task> act = async () =>
                await service.SoftDeleteAsync(taskId, CancellationToken.None);

            // Assert
            await act.Should()
                .ThrowAsync<KeyNotFoundException>()
                .WithMessage($"Task with id {taskId} not found");
        }
    }
}