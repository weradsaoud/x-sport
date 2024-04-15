using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xsport.Common.Enums
{
    public enum AccountStatusEnum : short
    {
        Unknown = 0,
        Ready = 1,
        ConfirmedButNoFavSports = 2,
        NotConfirmed = 3,
    }
}
