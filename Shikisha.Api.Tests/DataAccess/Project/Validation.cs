using Shikisha.DataAccess.Validation;
using Models = Shikisha.DataAccess.DomainModels;

namespace Shikisha.Tests.DataAccess.Project
{
    public class Validation : ValidationBaseTests<Models.Project>
    {
        private readonly ValidatorBase<Models.Project> _validator = new ValidatorBase<Models.Project>();
    }
}