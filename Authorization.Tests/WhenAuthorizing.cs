#region Usings

using System.Linq;
using FluentAssertions;
using NUnit.Framework;

#endregion

namespace Authorization.Tests
{
    public class WhenAuthorizing : TestBase
    {
        [TestCase(1, "User_1_Pass_!", true)]
        [TestCase(1, "Junk_!21*", false)]
        [TestCase(99, "26asd*!", false)]
        public void AuthorizationTokenCorrectWhenPassCorrect(int userId, string userPass, bool expected)
        {
            var user = new User { Id = userId };
            var authorizationService = new AuthorizationService();
            
            var authorizationToken = authorizationService.GetAuthorizeToken(user.Id, userPass);

            authorizationService.CheckAuthorizationTokenCorrect(authorizationToken).Should().Be(expected);
        }

        [TestCase(1, "User_1_Pass_!", true)]
        [TestCase(1, "Junk_!21*", false)]
        [TestCase(99, "26asd*!", false)]
        public void AuthorizedUserCanStartBattle(int userId, string userPass, bool expected)
        {
            var user = new User { Id = userId };
            var battleUserIds = new[] { 1, 2, 3 };
            var authorizationService = new AuthorizationService();
            var authorizationToken = authorizationService.GetAuthorizeToken(user.Id, userPass);

            var battleId = authorizationService.RegisterBattle(authorizationToken, battleUserIds);

            (battleId != null).Should().Be(expected);
            (authorizationService.Battles.Count() != 0).Should().Be(expected);
        }
    }
}