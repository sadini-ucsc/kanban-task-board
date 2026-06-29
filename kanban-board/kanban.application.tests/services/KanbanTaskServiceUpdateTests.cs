using FluentAssertions;
using kanban.application.dtos;
using kanban.application.services;
using kanban.domain.enums;
using kanban.domain.models;
using kanban.infrastructure.repositories;
using Moq;
using Xunit;

namespace kanban.application.tests.services
{
    public class KanbanTaskServiceUpdateTests
    {
        private readonly Mock<IKanbanTaskRepository> repositoryMock;
        private readonly KanbanTaskService service;

        public KanbanTaskServiceUpdateTests()
        {
            repositoryMock = new Mock<IKanbanTaskRepository>();
            service = new KanbanTaskService(repositoryMock.Object);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateTask_AndReturnUpdatedDto()
        {
            // Arrange
            var taskId = Guid.NewGuid();

            var existingTask = new KanbanTask
            {
                Id = taskId,
                Title = "Old Title",
                Description = "Old Desc",
                Status = KanbanTaskStatus.Todo,
                IsDeleted = false,
                CreatedAt = DateTime.UtcNow.AddDays(-1),
                UpdatedAt = DateTime.UtcNow.AddDays(-1)
            };

            repositoryMock
                .Setup(r => r.GetByIdAsync(taskId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(existingTask);

            repositoryMock
                .Setup(r => r.UpdateAsync(It.IsAny<KanbanTask>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((KanbanTask t, CancellationToken _) => t);

            var request = new UpdateKanbanTaskRequest
            {
                Title = "New Title",
                Description = "New Desc",
                Status = KanbanTaskStatus.InProgress
            };

            // Act
            var result = await service.UpdateAsync(taskId, request, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result!.Title.Should().Be("New Title");
            result.Description.Should().Be("New Desc");
            result.Status.Should().Be(KanbanTaskStatus.InProgress);

            repositoryMock.Verify(
                r => r.UpdateAsync(It.IsAny<KanbanTask>(), It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_ShouldThrowException_WhenTaskNotFound()
        {
            // Arrange
            var taskId = Guid.NewGuid();

            repositoryMock
                .Setup(r => r.GetByIdAsync(taskId, It.IsAny<CancellationToken>()))
                .ReturnsAsync((KanbanTask?)null);

            var request = new UpdateKanbanTaskRequest
            {
                Title = "New Title",
                Description = "New Desc",
                Status = KanbanTaskStatus.Done
            };

            // Act
            Func<Task> act = async () =>
                await service.UpdateAsync(taskId, request, CancellationToken.None);

            // Assert
            await act.Should()
                .ThrowAsync<KeyNotFoundException>()
                .WithMessage($"Task with id {taskId} not found");
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateTimestamp()
        {
            // Arrange
            var taskId = Guid.NewGuid();

            KanbanTask? captured = null;

            var existingTask = new KanbanTask
            {
                Id = taskId,
                Title = "Old",
                Description = "Old",
                Status = KanbanTaskStatus.Todo,
                CreatedAt = DateTime.UtcNow.AddDays(-2),
                UpdatedAt = DateTime.UtcNow.AddDays(-2)
            };

            repositoryMock
                .Setup(r => r.GetByIdAsync(taskId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(existingTask);

            repositoryMock
                .Setup(r => r.UpdateAsync(It.IsAny<KanbanTask>(), It.IsAny<CancellationToken>()))
                .Callback<KanbanTask, CancellationToken>((t, _) => captured = t)
                .ReturnsAsync((KanbanTask t, CancellationToken _) => t);

            var request = new UpdateKanbanTaskRequest
            {
                Title = "Updated",
                Description = "Updated",
                Status = KanbanTaskStatus.Done
            };

            // Act
            await service.UpdateAsync(taskId, request, CancellationToken.None);

            // Assert
            captured.Should().NotBeNull();
            captured!.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(5));
        }
    }
}