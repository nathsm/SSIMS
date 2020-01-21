﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSIMS.Models
{
    public class UserAccount
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int AccessType { get; set; }

        public UserAccount()
        {
        }

        public UserAccount(int userID, string userName, string password, int accessType)
        {
            UserID = userID;
            UserName = userName;
            Password = password;
            AccessType = accessType;
        }
    }
}