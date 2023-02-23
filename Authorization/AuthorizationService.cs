#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using JWT.Algorithms;
using JWT.Builder;
using Newtonsoft.Json.Linq;

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
                                                                  { 4, "User_4_Pass_!" },
                                                              };

        public Dictionary<string, int[]> Battles { get; } = new();

        private bool CheckTokenCorrect(string token)
        {
            try
            {
                JwtBuilder.Create().WithAlgorithm(new NoneAlgorithm()).Decode(token);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

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
            return CheckTokenCorrect(token);
        }

        public string RegisterBattle(string token, int[] battleUserIds)
        {
            if (CheckAuthorizationTokenCorrect(token))
            {
                var id = Battles.Count + 1;
                Battles.Add(id.ToString(), battleUserIds);
                return id.ToString();
            }

            return null;
        }

        public string GetBattleAuthorizeToken(string token, string battleId)
        {
            try
            {
                var json = JwtBuilder.Create().WithAlgorithm(new NoneAlgorithm()).Decode(token);
                dynamic jwtData = JObject.Parse(json);
                if (Battles.TryGetValue(battleId, out var battleUserIds) &&
                    battleUserIds.Contains((int)jwtData.userId))
                {
                    return JwtBuilder.Create()
                                     .WithAlgorithm(new NoneAlgorithm())
                                     .AddClaim("exp", DateTimeOffset.UtcNow.AddHours(1).ToUnixTimeSeconds())
                                     .AddClaim("userId", (int)jwtData.userId)
                                     .AddClaim("battleId", battleId)
                                     .Encode();
                }
            }
            catch (Exception)
            {
                return null;
            }

            return null;
        }

        public bool CheckBattleAuthorizationTokenCorrect(string token)
        {
            return CheckTokenCorrect(token);
        }
    }
}