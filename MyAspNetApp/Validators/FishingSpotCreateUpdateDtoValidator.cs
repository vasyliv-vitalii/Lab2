using FishingAndCyclingApp.DTOs;
using FluentValidation;

namespace MyAspNetApp.Validators
{
    public class FishingSpotCreateUpdateDtoValidator : AbstractValidator<CreateUpdateFishingSpotDto>
    {
        public FishingSpotCreateUpdateDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MinimumLength(3).WithMessage("Name must be at least 3 characters long.")
                .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");

            RuleFor(x => x.Coordinates)
                .NotEmpty().WithMessage("Coordinates are required.")
                .Matches(@"^-?\d+(\.\d+)?,-?\d+(\.\d+)?$").WithMessage("Coordinates must be in the format 'latitude,longitude'.");

            RuleFor(x => x.FishTypes)
                .Must(fishTypes => fishTypes != null && fishTypes.Count > 0).WithMessage("At least one fish type is required.")
                .ForEach(fishType => fishType.NotEmpty().WithMessage("Fish type cannot be empty."));

            RuleFor(x => x.Rating)
                .InclusiveBetween(0, 5).WithMessage("Rating must be between 0 and 5.");
        }

        public static void ValidateDto(CreateUpdateFishingSpotDto dto)
        {
            var validator = new FishingSpotCreateUpdateDtoValidator();
            var result = validator.Validate(dto);

            if (!result.IsValid)
            {
                throw new ValidationException(string.Join("; ", result.Errors.Select(e => e.ErrorMessage)));
            }
        }
    }
}
