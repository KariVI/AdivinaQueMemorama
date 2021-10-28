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
    public class Validation
    {
        public  bool validateEmail(string email)
        {
            bool value = true;
            try
            {
                MailAddress m = new MailAddress(email);

            }
            catch (FormatException ex)
            {
                value = false;
            }
            return value;
        }
    }
}

