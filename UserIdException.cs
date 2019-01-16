using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment1
{
    class UserIdException : Exception
    {
        public UserIdException(string msg) : base(msg)
        {

        }
    }
}
