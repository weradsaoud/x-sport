using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xsport.DB.Entities
{
    public class AcademyReview
    {
        public long AcademyReviewId { get; set; }
        public string Description { get; set; } = null!;
        public double Evaluation { get; set; }
        public DateTime ReviewDateTime { get; set; }

        //foriegn keys
        public long XsportUserId { get; set; }
        public long AcademyId { get; set; }

        //navigational props
        public XsportUser XsportUser { get; set; } = null!;
        public Academy Academy { get; set; } = null!;
    }
}
