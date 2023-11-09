using DTOs;
using FluentValidation;

namespace Utils {
    namespace Validators {
        public class CreateCompanyValidator : AbstractValidator<CreateCompanyDTO> {
            public CreateCompanyValidator() {
                RuleFor(company => company.name).NotEmpty().WithMessage(
                    ValidatorsPolicy.notEmpty("name")
                ).MaximumLength(50).WithMessage(
                    ValidatorsPolicy.maximumLength("name", 50)
                );
                RuleFor(company => company.description)
                .MaximumLength(400).WithMessage(
                    ValidatorsPolicy.maximumLength("description", 400)
                );
            }
        }
    }
}