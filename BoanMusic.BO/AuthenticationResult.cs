using System;
using System.Collections.Generic;
using System.Text;

namespace BoanMusicApp.BO
{
    public class AuthenticationResult
    {
        public bool IsAuthenticated { get; set; }
        public string User_Type { get; set; }
        public int User_ID { get; set; }
        public string Name { get; set; }
    }


}
