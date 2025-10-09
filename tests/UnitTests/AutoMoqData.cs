using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;

namespace UnitTests;

public class AutoMoqDataAttribute() : AutoDataAttribute(CreateFixture)
{
    private static IFixture CreateFixture() 
    {
        var fixture = new Fixture();
        fixture.Customize(new AutoMoqCustomization());
        return fixture;
    }
}
