using DTOs;
using FluentValidation;

namespace Utils {
    namespace Validators {
        public class CreateAddressValidator : AbstractValidator<CreateAddressDTO> {
            public CreateAddressValidator() {
                RuleFor(Address => Address.city).NotEmpty().WithMessage(
                    ValidatorsPolicy.notEmpty("city")
                ).MaximumLength(50).WithMessage(
                    ValidatorsPolicy.maximumLength("city", 50)
                );
                RuleFor(Address => Address.street)
                .MaximumLength(200).WithMessage(
                    ValidatorsPolicy.maximumLength("street", 200)
                );
                RuleFor(Address => Address.number)
                .MaximumLength(200).WithMessage(
                    ValidatorsPolicy.maximumLength("number", 200)
                );
                RuleFor(Address => Address.complement)
                .MaximumLength(200).WithMessage(
                    ValidatorsPolicy.maximumLength("complement", 200)
                );
            }
        }
    }
}