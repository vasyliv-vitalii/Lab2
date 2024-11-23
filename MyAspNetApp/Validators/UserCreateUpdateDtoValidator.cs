using FishingAndCyclingApp.DTOs;
using FluentValidation;

namespace FishingAndCyclingApp.Validators;

public class UserCreateUpdateDtoValidator : AbstractValidator<CreateUpdateUserDto>
{
    private UserCreateUpdateDtoValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username is required.")
            .MinimumLength(3).WithMessage("Username must be at least 3 characters long.")
            .MaximumLength(50).WithMessage("Username cannot exceed 50 characters.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("A valid email address is required.");

        RuleFor(x => x.Role)
            .NotEmpty().WithMessage("Role is required.")
            .Must(IsValidRole).WithMessage("Role must be either 'Admin' or 'User'.");
    }

    private bool IsValidRole(string role)
    {
        var validRoles = new[] { "Admin", "User" };
        return validRoles.Contains(role);
    }

    public static void ValidateDto(CreateUpdateUserDto dto)
    {
        var validator = new UserCreateUpdateDtoValidator();
        var result = validator.Validate(dto);

        if (!result.IsValid)
        {
            throw new ValidationException(string.Join("; ", result.Errors.Select(e => e.ErrorMessage)));
        }
    }
}