using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xsport.Common.Enums
{
    public enum SuggestedAcademiesOrderOptions:short
    {
        SimpleOrder = 1,
        MinPriceUp = 2, 
        MinPriceDown = 3,
        NameAsc = 4,
        NameDesc = 5,
        EvaluationUp = 6, 
        EvaluationDown = 7,
        NumOfReviewsUp = 8,
        NumOfReviewsDown = 9,
    }
}
