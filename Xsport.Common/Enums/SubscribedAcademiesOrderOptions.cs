using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xsport.Common.Enums
{
    public enum SubscribedAcademiesOrderOptions:short
    {
        None = 0,
        SimpleOrder = 1,//by Id
        ByCoursePointsDes = 2,
        ByCoursePointsAsen = 3,
    }
}
