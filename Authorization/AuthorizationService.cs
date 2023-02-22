#region Usings

using System;
using System.Collections.Generic;
using JWT.Algorithms;
using JWT.Builder;

#endregion

namespace Authorization
{
    public class AuthorizationService
    {
        private readonly Dictionary<int, string> userPasses = new()
                                                              {
                                                                  { 1, "User_1_Pass_!" },
                                                                  { 2, "User_2_Pass_!" },
                                                                  { 3, "User_3_Pass_!" },
                                                              };

        public Dictionary<int, int[]> Battles { get; } = new();

        public string GetAuthorizeToken(int userId, string userPass)
        {
            if (userPasses.TryGetValue(userId, out var pass)
                && pass == userPass)
            {
                return JwtBuilder.Create()
                                 .WithAlgorithm(new NoneAlgorithm())
                                 .AddClaim("exp", DateTimeOffset.UtcNow.AddHours(1).ToUnixTimeSeconds())
                                 .AddClaim("userId", userId)
                                 .Encode();
            }

            return null;
        }

        public bool CheckAuthorizationTokenCorrect(string token)
        {
            try
            {
                var json = JwtBuilder.Create()
                                     .WithAlgorithm(new NoneAlgorithm())
                                     .Decode(token);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public string RegisterBattle(string authorizationToken, int[] battleUserIds)
        {
            if (CheckAuthorizationTokenCorrect(authorizationToken))
            {
                var id = Battles.Count + 1;
                Battles.Add(id, battleUserIds);
                return id.ToString();
            }

            return null;
        }
    }
}