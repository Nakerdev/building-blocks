using FluentAssertions;
using LanguageExt.UnsafeValueAccess;
using NUnit.Framework.Internal;
using Providers.Business.ValueObjects;
using ValueObjects;

namespace ValueObjectsTests
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
            email.ValueUnsafe().Equals(other.ValueUnsafe()).Should().BeTrue();
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
            email.ValueUnsafe().Equals(other.ValueUnsafe()).Should().BeFalse();
        }

        [TestCase("")]
        [TestCase(" ")]
        [TestCase("  ")]
        public void Does_Not_Create_Email_When_Value_Is_Not_Provided(string value)
        {
            var result = Email.Create(value);

            result.IsLeft.Should().BeTrue();
            result.IfLeft(error => error.Should().Be(ValidationError.Required));
        }

        [Test]
        public void Does_Not_Create_Email_When_Value_Exceeds_Max_Allowed_Length()
        {
            var result = Email.Create(new string('a', 256));

            result.IsLeft.Should().BeTrue();
            result.IfLeft(error => error.Should().Be(ValidationError.MaximumLengthExceeded));
        }

        [Test]
        public void Does_Not_Create_Email_When_Is_Not_Valid_Email()
        {
            var result = Email.Create("not-valid#email.com");

            result.IsLeft.Should().BeTrue();
            result.IfLeft(error => error.Should().Be(ValidationError.InvalidFormat));
        }
    }
}
