using System;
using FluentValidation;
using Shikisha.Utilities;

namespace Shikisha.DataAccess.Validation
{
    /// <summary>
    /// Provides validation rules for base entities
    /// </summary>
    public class ValidatorBase<TEntity> : AbstractValidator<TEntity>
    where TEntity : EntityBase
    {
        public ValidatorBase()
        {
            RuleFor(x => x.Name)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NameValidation();

            RuleFor(x => x.Description)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNullOrEmpty("Description")
                .AddError(ErrorCode.StringMustNotBeNullOrEmpty, "Description")
                .MaximumLength(100)
                .AddError(ErrorCode.StringLengthTooLong, "Description");
        }
    }
}