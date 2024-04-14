using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xsport.DTOs.AcademyDtos
{
    public class SuggestedAcademyDto
    {
        public long AcademyId { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Lat { get; set; }
        public decimal Long { get; set; }
        public string RegionName { get; set; } = "تضاف لاحقا";
        public decimal MinPrice { get; set; }
        public string OpenTime { get; set; } = null!;
        public string CloseTime { get; set; }=null!;
        //public bool IsOpen { get; set; }//TODO
        public double Evaluation { get; set; }
        public int NumReviews { get; set; }
        public string CoverPhoto { get; set; } = null!;
        public string CoverVideo { get; set; } = null!;
        public List<string> Photos { get; set; } = null!;
        public List<string> Videos { get; set; } = null!;
        public List<DropDownDto> AgeCategoriesDropdownItems { get; set; } = null!;

    }
    public class SuggestedAcademiesDto
    {
        public List<DropDownDto> GendersDropdownItems { get; set; } = null!;
        public List<DropDownDto> RelativesDropdownItems { get; set; } = null!;
        public List<SuggestedAcademyDto> SuggestedAcademies { get; set; } = null!;
    }
}
