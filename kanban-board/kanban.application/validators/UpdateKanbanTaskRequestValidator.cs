using FluentValidation;
using kanban.application.dtos;

namespace kanban.application.validators
{
    public class UpdateKanbanTaskRequestValidator : AbstractValidator<UpdateKanbanTaskRequest>
    {
        public UpdateKanbanTaskRequestValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(ValidationConstants.TitleMaxLength);

            RuleFor(x => x.Description)
                .MaximumLength(ValidationConstants.DescriptionMaxLength);

            RuleFor(x => x.Status)
                .IsInEnum();
        }
    }
}
