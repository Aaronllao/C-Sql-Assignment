using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Assignment1
{
    class FormatManager
    {
        public Boolean RoomIsValid(string abc)
        {
            if(abc == "A" || abc == "B" || abc == "C" || abc == "D")
            {
                return true;
            }
            else
            {
                var rx = new RoomIdException("Room id is invalid, please try again");
                throw rx;
            }
        }

        public Boolean StaffIdIsValid(string abc)
        {
            if (abc.StartsWith("e"))
            {
                return true;
            }
            else
            {
                var ex = new UserIdException("Staff id is invalid, please try again");
                throw ex;
            }
        }

        public Boolean StudentIdIsValid(string abc)
        {
            if (abc.StartsWith("s"))
            {
                return true;
            }
            else
            {
                var ex = new UserIdException("Student id is invalid, please try again");
                throw ex;
            }
        }

        public Boolean DateIsValid(string dateString)
        {
            DateTime dtDate;
            if(DateTime.TryParseExact(dateString, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dtDate))
            {
                return true;
            }
            else
            {
                var ex = new DateException("Date format is wrong, please try again");
                throw ex;
            }
        }

        public Boolean TimeIsValid(string time)
        {
            DateTime dtTime;
            if(DateTime.TryParseExact(time, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out dtTime))
            {
                return true;
            }
            else
            {
                var ex = new DateException("Date format is wrong, please try again");
                throw ex;
            }
        }
    }
}
