﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSIMS.Service
{
    interface ILoginService
    {
        bool VerifyPassword(string username, string password);
        void CreateNewSession(string username, string sessionId);
        bool AuthenticateSession(string username, string sessionId);
        void CancelSession(string username);
    }
}
