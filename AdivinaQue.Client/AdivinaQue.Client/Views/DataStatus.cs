using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdivinaQue.Client.Views
{
    public enum DataStatus
    {
        Correct = 0,
        UserNameInvalid,
        NameInvalid,
        PasswordInvalid,
        UserNameDuplicate,
        ShortPassword,
        EmailIncorrect
    }
}
