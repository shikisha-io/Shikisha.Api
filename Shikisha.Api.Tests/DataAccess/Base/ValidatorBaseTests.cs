using FluentValidation;

namespace Shikisha.Tests.DataAccess
{
    public abstract class ValidatorBaseTests<TEntity>
    {
        protected AbstractValidator<TEntity> _validator;

        public ValidatorBaseTests(AbstractValidator<TEntity> validator)
        {
            _validator = validator;
        }

        // public Should_Have_Validation_Error()
        // {
        //     _validator.ShouldHaveValidationErrorFor(contact => contact.DateOfBirth, default(DateTime))
        //     .WithErrorCode(ErrorCode.StringMustNotBeNullOrEmpty.GetErrorCodeString())
        //     .WithErrorMessage("Invalid Date of Birth: String cannot be null or empty");
        // }
    }
}