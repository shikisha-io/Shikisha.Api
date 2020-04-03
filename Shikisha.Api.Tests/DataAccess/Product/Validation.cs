using Shikisha.DataAccess.Validation;
using Models = Shikisha.DataAccess.DomainModels;

namespace Shikisha.Tests.DataAccess.Product
{
    public class Validation : ValidationBaseTests<Models.Product>
    {
        private readonly ValidatorBase<Models.Product> _validator = new ValidatorBase<Models.Product>();
    }
}