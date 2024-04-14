using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xsport.Common.Enums
{
    public enum SuggestedStadiumsFilterOptions  : short
    {
        None = 0,
        ByName = 1,
        ByEvaluationUp = 2,
        ByEvaluationDown = 3,
        ByEvaluation = 4,
        NumOfReviewsUp = 5,
        NumOfReviewsDown = 6,
        NumOfReviews = 7
    }
}
