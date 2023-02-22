#region Usings

using FluentAssertions;
using NUnit.Framework;

#endregion

namespace Authorization.Tests
{
    public class WhenAuthorizing : TestBase
    {
        [TestCase("User_1_Pass_!", true)]
        [TestCase("Junk_!21*", false)]
        public void AuthorizationTokenCorrectWhenPassCorrect(string pass, bool expected)
        {
            var user = new User { Id = 1 };
            var authorizationService = new AuthorizationService();
            var authorizationToken = authorizationService.AuthorizeUser(user.Id, pass);

            authorizationService.CheckTokenCorrect(authorizationToken).Should().Be(expected);
        }

        [Test]
        public void METHOD()
        {
            var user = new User { Id = 1 };
            var battleUserIds = new[] { 1, 2, 3 };
            var authorizationService = new AuthorizationService();

            var authorizationToken = authorizationService.AuthorizeUser(user.Id, "User_1_Pass_!");
        }
    }
}