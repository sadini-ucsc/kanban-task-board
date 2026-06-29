using FluentValidation.TestHelper;
using kanban.application.dtos;
using kanban.application.validators;
using Xunit;

namespace kanban.application.tests.validators
{
    public class CreateKanbanTaskRequestValidatorTests
    {
        private readonly CreateKanbanTaskRequestValidator validator;

        public CreateKanbanTaskRequestValidatorTests()
        {
            validator = new CreateKanbanTaskRequestValidator();
        }

        [Fact]
        public void Should_Pass_When_Request_Is_Valid()
        {
            var request = new CreateKanbanTaskRequest
            {
                Title = "Valid Title",
                Description = "Valid Description"
            };

            var result = validator.TestValidate(request);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void Should_Fail_When_Title_Is_Empty()
        {
            var request = new CreateKanbanTaskRequest
            {
                Title = "",
                Description = "Valid Description"
            };

            var result = validator.TestValidate(request);

            result.ShouldHaveValidationErrorFor(x => x.Title);
        }

        [Fact]
        public void Should_Fail_When_Title_Exceeds_Max_Length()
        {
            var request = new CreateKanbanTaskRequest
            {
                Title = new string('A', ValidationConstants.TitleMaxLength + 1),
                Description = "Valid Description"
            };

            var result = validator.TestValidate(request);

            result.ShouldHaveValidationErrorFor(x => x.Title);
        }

        [Fact]
        public void Should_Fail_When_Description_Exceeds_Max_Length()
        {
            var request = new CreateKanbanTaskRequest
            {
                Title = "Valid Title",
                Description = new string('A', ValidationConstants.DescriptionMaxLength + 1)
            };

            var result = validator.TestValidate(request);

            result.ShouldHaveValidationErrorFor(x => x.Description);
        }
    }
}