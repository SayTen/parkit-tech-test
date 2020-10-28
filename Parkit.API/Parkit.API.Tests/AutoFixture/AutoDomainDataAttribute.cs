using AutoFixture;
using AutoFixture.AutoNSubstitute;
using AutoFixture.Xunit2;

namespace Parkit.API.Tests.AutoFixture
{
    public class AutoDomainDataAttribute : AutoDataAttribute
    {

        public AutoDomainDataAttribute()
            : base(() => new Fixture()
                .Customize(new AutoNSubstituteCustomization()))
        {
        }
    }
}
