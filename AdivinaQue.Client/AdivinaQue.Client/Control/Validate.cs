using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdivinaQue.Client.Control
{
    public class Validate
    {

        public bool ValidationAlphanumeric(String field) {

            bool value = false;
            Regex regex = new Regex("^[a-zA-Z0-9]*$");
            if (regex.IsMatch(field)) {
                value = true;
            }
            return value;

        }

        public bool ValidationEmail(String email) {
            bool value = false;
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);
            if (match.Success) {
                value = true;
             }
            return value;
        }

        public bool ValidationString(String field)
        {
            bool value = false;
            Regex regex = new Regex("^[a-zA-Z ]*$");
            if (regex.IsMatch(field)) {
                value = true;
            }
            return value;
        }
    }
}
