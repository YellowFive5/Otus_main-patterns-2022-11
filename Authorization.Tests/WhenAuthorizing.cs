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
            var authorizationService = new AuthorizationService();
            var user = new User { Id = userId };

            var authorizationToken = authorizationService.GetAuthorizeToken(user.Id, userPass);

            authorizationService.CheckAuthorizationTokenCorrect(authorizationToken).Should().Be(expected);
        }

        [TestCase(1, "User_1_Pass_!", true)]
        [TestCase(1, "Junk_!21*", false)]
        [TestCase(99, "26asd*!", false)]
        public void AuthorizedUserCanStartBattle(int userId, string userPass, bool expected)
        {
            var authorizationService = new AuthorizationService();
            var user = new User { Id = userId };
            var battleUserIds = new[] { 1, 2, 3 };
            var authorizationToken = authorizationService.GetAuthorizeToken(user.Id, userPass);

            var battleId = authorizationService.RegisterBattle(authorizationToken, battleUserIds);

            (battleId != null).Should().Be(expected);
            (authorizationService.Battles.Count() != 0).Should().Be(expected);
        }

        [TestCase(1, "User_1_Pass_!", true)]
        [TestCase(2, "User_2_Pass_!", true)]
        [TestCase(3, "User_3_Pass_!", true)]
        [TestCase(4, "User_4_Pass_!", false)]
        [TestCase(99, "26asd*!", false)]
        public void BattleAuthorizationTokenCorrectOnlyForAuthorizedInBattleUsers(int userId, string userPass, bool expected)
        {
            var authorizationService = new AuthorizationService();
            var serverUser = new User { Id = 1 };
            var battleUserIds = new[] { 1, 2, 3 };
            var authorizationToken = authorizationService.GetAuthorizeToken(serverUser.Id, "User_1_Pass_!");
            var battleId = authorizationService.RegisterBattle(authorizationToken, battleUserIds);
            var testUser = new User { Id = userId };
            authorizationToken = authorizationService.GetAuthorizeToken(testUser.Id, userPass);

            var battleAuthorizationToken = authorizationService.GetBattleAuthorizeToken(authorizationToken, battleId);

            authorizationService.CheckBattleAuthorizationTokenCorrect(battleAuthorizationToken).Should().Be(expected);
        }
    }
}