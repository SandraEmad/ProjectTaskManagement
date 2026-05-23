using FluentValidation;
using ProjectTaskManagement.Application.DTOs.Tasks;
using ProjectTaskManagement.Domain.Enums;

namespace ProjectTaskManagement.Application.Validators
{
    public class UpdateTaskStatusDtoValidator : AbstractValidator<UpdateTaskStatusDto>
    {
        public UpdateTaskStatusDtoValidator()
        {
            RuleFor(x => x.Status)
                .IsInEnum().WithMessage("Invalid status value");
        }
    }
}