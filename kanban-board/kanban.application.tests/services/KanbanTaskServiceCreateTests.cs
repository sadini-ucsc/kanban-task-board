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
    public class KanbanTaskServiceCreateTests
    {
        private readonly Mock<IKanbanTaskRepository> repositoryMock;
        private readonly KanbanTaskService service;

        public KanbanTaskServiceCreateTests()
        {
            repositoryMock = new Mock<IKanbanTaskRepository>();
            service = new KanbanTaskService(repositoryMock.Object);
        }

        [Fact]
        public async Task CreateAsync_ShouldCreateTask_WithDefaultValues()
        {
            // Arrange
            var request = new CreateKanbanTaskRequest
            {
                Title = "New Task",
                Description = "New Description"
            };

            KanbanTask? capturedEntity = null;

            repositoryMock
                .Setup(r => r.CreateAsync(It.IsAny<KanbanTask>(), It.IsAny<CancellationToken>()))
                .Callback<KanbanTask, CancellationToken>((task, _) =>
                {
                    capturedEntity = task;
                })
                .ReturnsAsync((KanbanTask t, CancellationToken _) => t);

            // Act
            var result = await service.CreateAsync(request, CancellationToken.None);

            // Assert - DTO result
            result.Should().NotBeNull();
            result.Title.Should().Be("New Task");
            result.Description.Should().Be("New Description");
            result.Status.Should().Be(KanbanTaskStatus.Todo);

            // Assert - entity sent to repository
            capturedEntity.Should().NotBeNull();
            capturedEntity!.Title.Should().Be("New Task");
            capturedEntity.Description.Should().Be("New Description");
            capturedEntity.Status.Should().Be(KanbanTaskStatus.Todo);
            capturedEntity.IsDeleted.Should().BeFalse();
            capturedEntity.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(5));
            capturedEntity.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(5));
        }

        [Fact]
        public async Task CreateAsync_ShouldCallRepository_Once()
        {
            // Arrange
            var request = new CreateKanbanTaskRequest
            {
                Title = "Task",
                Description = "Desc"
            };

            repositoryMock
                .Setup(r => r.CreateAsync(It.IsAny<KanbanTask>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((KanbanTask t, CancellationToken _) => t);

            // Act
            await service.CreateAsync(request, CancellationToken.None);

            // Assert
            repositoryMock.Verify(
                r => r.CreateAsync(It.IsAny<KanbanTask>(), It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [Fact]
        public async Task CreateAsync_ShouldGenerateNewId()
        {
            // Arrange
            var request = new CreateKanbanTaskRequest
            {
                Title = "Task",
                Description = "Desc"
            };

            KanbanTask? captured = null;

            repositoryMock
                .Setup(r => r.CreateAsync(It.IsAny<KanbanTask>(), It.IsAny<CancellationToken>()))
                .Callback<KanbanTask, CancellationToken>((t, _) => captured = t)
                .ReturnsAsync((KanbanTask t, CancellationToken _) => t);

            // Act
            await service.CreateAsync(request, CancellationToken.None);

            // Assert
            captured.Should().NotBeNull();
            captured!.Id.Should().NotBe(Guid.Empty);
        }
    }
}