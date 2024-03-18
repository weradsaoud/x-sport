using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xsport.Common.Enums
{
    public enum SuggestedAcademiesFilterOptions:short
    {
        None = 0,
        ByName = 1,
        ByPriceUp = 2,
        ByPriceDown = 3,
        ByPrice = 4,
        ByEvaluationUp = 5,
        ByEvaluationDown = 6,
        ByEvaluation = 7,
        NumOfReviewsUp = 8,
        NumOfReviewsDown = 9,
        NumOfReviews = 10,
    }
}
