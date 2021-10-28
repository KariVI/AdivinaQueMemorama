using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdivinaQue.Client.Control
{
    public class Validate
    {
        public  bool validateEmail(string email)
        {
            bool value = false;
            string pattern = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";

            //check first string
            if (Regex.IsMatch(email, pattern))
            {
                value = true;

            }
                return value;
        }

        public bool validateAlphanumericString(string alphanumeric)
        {
            bool value = false;
            Regex regex = new Regex("^[a-zA-Z0-9]*$");
            if (regex.IsMatch(alphanumeric))
            {
                value = true;
            }
            return value;
        }
    }
}

