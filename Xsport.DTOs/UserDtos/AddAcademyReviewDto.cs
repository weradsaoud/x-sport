using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xsport.DTOs.UserDtos
{
    public class AddAcademyReviewDto
    {
        public long AcademyId { get; set; }
        public string ReviewText { get; set; } = null!;
        public double Evaluation { get; set; }
    }
}
