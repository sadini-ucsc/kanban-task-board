using FluentValidation.TestHelper;
using kanban.application.dtos;
using kanban.application.validators;
using kanban.domain.enums;
using Xunit;

namespace kanban.application.tests.validators
{
    public class UpdateKanbanTaskRequestValidatorTests
    {
        private readonly UpdateKanbanTaskRequestValidator validator;

        public UpdateKanbanTaskRequestValidatorTests()
        {
            validator = new UpdateKanbanTaskRequestValidator();
        }

        [Fact]
        public void Should_Pass_When_Request_Is_Valid()
        {
            var request = new UpdateKanbanTaskRequest
            {
                Title = "Valid Title",
                Description = "Valid Description",
                Status = KanbanTaskStatus.Todo
            };

            var result = validator.TestValidate(request);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void Should_Fail_When_Title_Is_Empty()
        {
            var request = new UpdateKanbanTaskRequest
            {
                Title = "",
                Description = "Valid Description",
                Status = KanbanTaskStatus.Todo
            };

            var result = validator.TestValidate(request);

            result.ShouldHaveValidationErrorFor(x => x.Title);
        }

        [Fact]
        public void Should_Fail_When_Title_Exceeds_Max_Length()
        {
            var request = new UpdateKanbanTaskRequest
            {
                Title = new string('A', ValidationConstants.TitleMaxLength + 1),
                Description = "Valid Description",
                Status = KanbanTaskStatus.Todo
            };

            var result = validator.TestValidate(request);

            result.ShouldHaveValidationErrorFor(x => x.Title);
        }

        [Fact]
        public void Should_Fail_When_Description_Exceeds_Max_Length()
        {
            var request = new UpdateKanbanTaskRequest
            {
                Title = "Valid Title",
                Description = new string('A', ValidationConstants.DescriptionMaxLength + 1),
                Status = KanbanTaskStatus.Todo
            };

            var result = validator.TestValidate(request);

            result.ShouldHaveValidationErrorFor(x => x.Description);
        }

        [Fact]
        public void Should_Fail_When_Status_Is_Invalid()
        {
            var request = new UpdateKanbanTaskRequest
            {
                Title = "Valid Title",
                Description = "Valid Description",
                Status = (KanbanTaskStatus)999
            };

            var result = validator.TestValidate(request);

            result.ShouldHaveValidationErrorFor(x => x.Status);
        }
    }
}