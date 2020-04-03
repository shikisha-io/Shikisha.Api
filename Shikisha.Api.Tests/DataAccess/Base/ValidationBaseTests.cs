using FluentValidation.TestHelper;
using Shikisha.DataAccess;
using Shikisha.DataAccess.Validation;
using Shikisha.Utilities;
using Xunit;
using Models = Shikisha.DataAccess.DomainModels;

namespace Shikisha.Tests.DataAccess
{
    public abstract class ValidationBaseTests<TEntity> where TEntity : EntityBase
    {
        private readonly ValidatorBase<Models.Product> _validator = new ValidatorBase<Models.Product>();

        #region Failure tests
        [Theory]
        [InlineData(null)] // No nulls
        [InlineData("")] // No empty
        [InlineData("    ")] // No whitespace
        public void Theory_ContactValidation_FirstName_CannotBeBlank(string name) =>
            _validator.ShouldHaveValidationErrorFor(entity => entity.Name, name)
            .WithErrorCode(ErrorCode.StringMustNotBeNullOrEmpty.GetErrorCodeString())
            .WithErrorMessage("Invalid Name: String cannot be null or empty");

        [InlineData("abcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyz")]
        public void Theory_ContactValidation_FirstName_CannotBeTooLong(string name) =>
            _validator.ShouldHaveValidationErrorFor(entity => entity.Name, name)
            .WithErrorCode(ErrorCode.StringLengthTooLong.GetErrorCodeString())
            .WithErrorMessage("Invalid Name: String length too long");
        #endregion Failure tests

        #region Success Tests
        [Theory]
        [InlineData("John")]
        [InlineData("Heung-min")]
        [InlineData("Heung Min")]
        [InlineData("D'Angelo")]
        public void Theory_ContactValidation_FirstName_Success(string name) =>
            _validator.ShouldNotHaveValidationErrorFor(entity => entity.Name, name);
        #endregion Success Tests
    }
}