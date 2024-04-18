using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xsport.Common.Enums
{
    public enum SubscribedAcademiesFilterOptions : short
    {
        None = 0,
        ByActive = 1,
        FilterByAcademyName = 2,
        FilterBySubscriptionStartDate = 3,
        FilterBySubscriptionEndtDate = 4,
    }
}
