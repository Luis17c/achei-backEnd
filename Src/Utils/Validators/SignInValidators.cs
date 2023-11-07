using DTOs;
using FluentValidation;
using Models;

namespace Utils {
    namespace Validators {
        public class SignInValidator : AbstractValidator<UserSignInDTO> {
            public SignInValidator() {
                RuleFor(user => user.email).NotEmpty().WithMessage(
                    ValidatorsPolicy.notEmpty("email")
                ).MaximumLength(50).WithMessage(
                    ValidatorsPolicy.maximumLength("email", 50)
                ).EmailAddress().WithMessage(
                    ValidatorsPolicy.wrongFormat("email", "email")
                );

                RuleFor(user => user.password).NotEmpty().WithMessage(
                    ValidatorsPolicy.notEmpty("password")
                ).MaximumLength(50).WithMessage(
                    ValidatorsPolicy.maximumLength("password", 50)
                ).MinimumLength(8).WithMessage(
                    ValidatorsPolicy.minimumLength("password", 8)
                );
            }
        }
    }
}