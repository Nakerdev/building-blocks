using FluentAssertions;
using NUnit.Framework.Internal;

namespace Providers.ValueObjects
{
    [TestFixture]
    public sealed class EmailTests
    {
        [Test]
        public void Creates_Email()
        {
            var result = Email.Create("name.surname@domain.com");

            result.IsRight.Should().BeTrue();
        }
    }
}
