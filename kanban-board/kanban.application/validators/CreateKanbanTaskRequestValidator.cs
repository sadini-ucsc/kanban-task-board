using FluentValidation;
using kanban.application.dtos;

namespace kanban.application.validators
{
    public class CreateKanbanTaskRequestValidator : AbstractValidator<CreateKanbanTaskRequest>
    {
        public CreateKanbanTaskRequestValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(ValidationConstants.TitleMaxLength);

            RuleFor(x => x.Description)
                .MaximumLength(ValidationConstants.DescriptionMaxLength);
        }
    }
}
