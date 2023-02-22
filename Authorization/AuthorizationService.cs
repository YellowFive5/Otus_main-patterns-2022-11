#region Usings

using System;
using System.Collections.Generic;

#endregion

namespace Authorization
{
    public class AuthorizationService
    {
        private Dictionary<int, string> userPasses = new()
                                                     {
                                                         { 1, "User_1_Pass_!" },
                                                         { 2, "User_2_Pass_!" },
                                                         { 3, "User_3_Pass_!" },
                                                     };

        public string AuthorizeUser(int userId, string userPass)
        {
            throw new NotImplementedException();
        }

        public bool CheckTokenCorrect(string token)
        {
            throw new NotImplementedException();
        }
    }
}