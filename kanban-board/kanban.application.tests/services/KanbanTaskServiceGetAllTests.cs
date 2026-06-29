using FluentAssertions;
using kanban.application.services;
using kanban.domain.enums;
using kanban.domain.models;
using kanban.infrastructure.repositories;
using Moq;
using Xunit;

namespace kanban.application.tests.services
{
    public class KanbanTaskServiceGetAllTests
    {
        private readonly Mock<IKanbanTaskRepository> repositoryMock;
        private readonly KanbanTaskService service;

        public KanbanTaskServiceGetAllTests()
        {
            repositoryMock = new Mock<IKanbanTaskRepository>();
            service = new KanbanTaskService(repositoryMock.Object);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllTasks()
        {
            // Arrange
            var tasks = new List<KanbanTask>
            {
                new KanbanTask
                {
                    Id = Guid.NewGuid(),
                    Title = "Task 1",
                    Description = "Desc 1",
                    Status = KanbanTaskStatus.Todo
                },
                new KanbanTask
                {
                    Id = Guid.NewGuid(),
                    Title = "Task 2",
                    Description = "Desc 2",
                    Status = KanbanTaskStatus.Done
                }
            };

            repositoryMock
                .Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(tasks);

            // Act
            var result = await service.GetAllAsync(CancellationToken.None);

            // Assert
            result.Should().HaveCount(2);
            result.Select(x => x.Title).Should().Contain(new[] { "Task 1", "Task 2" });
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnEmptyList_WhenNoTasks()
        {
            // Arrange
            repositoryMock
                .Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<KanbanTask>());

            // Act
            var result = await service.GetAllAsync(CancellationToken.None);

            // Assert
            result.Should().BeEmpty();
        }
    }
}