using AutoFixture;
using AutoFixture.Xunit2;

namespace UnitTests;

public class AutoMoqDataAttribute() : AutoDataAttribute(CreateFixture)
{
    private static IFixture CreateFixture() 
    {
        var fixture = new Fixture();
        return fixture;
    }
}
