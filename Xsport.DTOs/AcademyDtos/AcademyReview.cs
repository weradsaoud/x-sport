using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xsport.DTOs.AcademyDtos
{
    public class AcademyReviewDto
    {
        public long AcademyId { get; set; }
        public string AcademyName { get; set; } = null!;
        public string CoverPhoto { get; set; } = null!;
        public string CoverVideo { get; set; } = null!;
        public IEnumerable<string> Photos { get; set; } = null!;
        public IEnumerable<string> Videos { get; set; } = null!;
        public IEnumerable<ReviewDto> Reviews { get; set; } = null!;
    }
    public class ReviewDto
    {
        public long ReviewId { get; set; }
        public long UserId { get; set; }
        public string UserName { get; set; } = null!;
        public string UserImg { get; set; } = null!;
        public string ReviewContent { get; set; } = null!;
        public double Evaluation { get; set; }
        public string ReviewDateTime { get; set; } = null!;
    }
}
