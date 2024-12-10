using FluentValidation;
using MyAspNetApp.DTOs;

namespace MyAspNetApp.Validators
{
    public class BikeRouteCreateUpdateDtoValidator: AbstractValidator<CreateUpdateBikeRouteDto>
    {
        public BikeRouteCreateUpdateDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MinimumLength(3).WithMessage("Name must be at least 3 characters long.")
                .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");

            RuleFor(x => x.Distance)
                .GreaterThan(0).WithMessage("Distance must be greater than 0.")
                .LessThanOrEqualTo(10000).WithMessage("Distance cannot exceed 10,000 km.");

            RuleFor(x => x.Difficulty)
                .NotEmpty().WithMessage("Difficulty is required.")
                .Must(IsValidDifficulty).WithMessage("Difficulty must be one of the following: Easy, Medium, Hard.");
        }

        private bool IsValidDifficulty(string difficulty)
        {
            var validDifficulties = new[] { "Easy", "Medium", "Hard" };
            return validDifficulties.Contains(difficulty);
        }

        public static void ValidateDto(CreateUpdateBikeRouteDto dto)
        {
            var validator = new BikeRouteCreateUpdateDtoValidator();
            var result = validator.Validate(dto);

            if (!result.IsValid)
            {
                throw new ValidationException(string.Join("; ", result.Errors.Select(e => e.ErrorMessage)));
            }
        }
    }
}
