using FluentValidation.TestHelper;
using Moq;
using Shikisha.DataAccess;
using Shikisha.DataAccess.Validation;
using Shikisha.Services.Interfaces;
using Shikisha.Utilities;
using Xunit;
using Models = Shikisha.DataAccess.DomainModels;

namespace Shikisha.Tests.DataAccess
{
    public abstract class ValidationBaseTests<TEntity> where TEntity : EntityBase, new()
    {
        private readonly ValidatorBase<TEntity> _validator = new ValidatorBase<TEntity>();

        #region Failure tests
        [Theory]
        [InlineData(null)] // No nulls
        [InlineData("")] // No empty
        [InlineData("    ")] // No whitespace
        public void Theory_EntityBaseValidation_FirstName_CannotBeBlank(string name) =>
            _validator.ShouldHaveValidationErrorFor(entity => entity.Name, name)
            .WithErrorCode(ErrorCode.StringMustNotBeNullOrEmpty.GetErrorCodeString())
            .WithErrorMessage("Invalid Name: String cannot be null or empty");

        [InlineData("abcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyz")]
        public void Theory_EntityBaseValidation_FirstName_CannotBeTooLong(string name) =>
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
        public void Theory_EntityBaseValidation_FirstName_Success(string name) =>
            _validator.ShouldNotHaveValidationErrorFor(entity => entity.Name, name);
        #endregion Success Tests
    }
}