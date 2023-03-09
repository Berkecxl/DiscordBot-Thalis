using Discord_Bot.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Discord_Bot.Helper.BaseHelper
{
    public class UserHelper : IUserHelper
    {
        public bool IsUserValid(string user)
        {
            int valid = user.IndexOf("@", 0, 2);
            if (valid != -1)
            {
                return true;
            }
           return false;
        
        }
    }
}
