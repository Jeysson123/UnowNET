using System.Threading.Tasks;
using FluentValidation;
using Models.Entities;

namespace Services.Validators
{
    public class TaskToDoValidator : AbstractValidator<TaskToDo>
    {
        public TaskToDoValidator()
        {
            RuleFor(task => task.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MinimumLength(5).WithMessage("Description must have 5 or more characters.")
                .MaximumLength(15).WithMessage("Description can't contains more than 15 characters.");
        }
    }
}

