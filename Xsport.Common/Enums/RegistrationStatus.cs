using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xsport.Common.Enums
{
    public enum RegistrationStatusEnum : short
    {
        unKown = 0,
        registeredWithEmailAndPassword = 1,
        registredWithGoogle = 2,
        comletedRegistrationWithGoogle = 3,
        profilePictureSkipped = 4,
        RegistrationFinished = 5,
    }
}
