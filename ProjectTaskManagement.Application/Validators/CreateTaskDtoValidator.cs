
using FluentValidation;
using ProjectTaskManagement.Application.DTOs.Tasks;

namespace ProjectTaskManagement.Application.Validators
{
    public class CreateTaskDtoValidator : AbstractValidator<CreateTaskDto>
    {
        public CreateTaskDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required")
                .MaximumLength(200).WithMessage("Title must not exceed 200 characters");

            RuleFor(x => x.Description)
                .MaximumLength(1000).WithMessage("Description must not exceed 1000 characters");

            RuleFor(x => x.ProjectId)
                .NotEmpty().WithMessage("ProjectId is required");

            RuleFor(x => x.DueDate)
             .GreaterThan(_ => DateTime.UtcNow)
             .WithMessage("DueDate must be in the future")
             .When(x => x.DueDate.HasValue);
        }
    }
}
