using FluentAssertions;
using LanguageExt.UnsafeValueAccess;
using NUnit.Framework.Internal;
using Providers.Business.ValueObjects;

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

        [Test]
        public void Emails_With_Same_Struct_Should_Be_Equals()
        {
            var email = Email.Create("name.surname@domain.com");
            var other = Email.Create("name.surname@domain.com");

            email.IsRight.Should().BeTrue();
            other.IsRight.Should().BeTrue();
            (email.ValueUnsafe() == other.ValueUnsafe()).Should().BeTrue();
            (email.ValueUnsafe() != other.ValueUnsafe()).Should().BeFalse();
            (email.ValueUnsafe().Equals(other.ValueUnsafe())).Should().BeTrue();
        }

        [Test]
        public void Emails_With_Different_Struct_Should_Be_Different()
        {
            var email = Email.Create("name.surname@domain.com");
            var other = Email.Create("name.surname@company.es");

            email.IsRight.Should().BeTrue();
            other.IsRight.Should().BeTrue();
            (email.ValueUnsafe() == other.ValueUnsafe()).Should().BeFalse();
            (email.ValueUnsafe() != other.ValueUnsafe()).Should().BeTrue();
            (email.ValueUnsafe().Equals(other.ValueUnsafe())).Should().BeFalse();
        }
    }
}
