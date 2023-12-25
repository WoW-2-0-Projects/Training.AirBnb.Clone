using AirBnB.Application.Common.Settings;
using AirBnB.Domain.Entities;
using AirBnB.Domain.Enums;
using FluentValidation;
using Microsoft.Extensions.Options;

namespace AirBnB.Infrastructure.Common.Validators
{
    /// <summary>
    /// Validator for the 'Role' entity, implementing rules for validation.
    /// </summary>
    public class RoleValidation : AbstractValidator<Role>
    {
        /// <summary>
        /// Constructor for RoleValidation initializing with validation settings.
        /// </summary>
        /// <param name="validationSettings">The options containing validation settings.</param>
        public RoleValidation(IOptions<ValidationSettings> validationSettings)
        {
            var validationSettingsValue = validationSettings.Value;

            // Define validation rules for different scenarios (e.g., entity creation)
            RuleSet(
                EntityEvent.OnCreate.ToString(), // Rule set for entity creation
                () =>
                {
                    // Ensure the 'Id' property is not empty when creating a role
                    RuleFor(code => code.Id).NotEqual(Guid.Empty);

                    // Ensure the 'Type' property is not empty when creating a role
                    RuleFor(role => role.Type)
                        .NotEmpty();
                }
            );
        }
    }
}
