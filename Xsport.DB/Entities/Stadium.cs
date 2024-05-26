using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xsport.DB.Entities
{
    public class Stadium
    {
        public long StadiumId { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        
        public bool? IsComplete { get; set; }
        //foriegn keys
        public long? AcademyId { get; set; }
        //navigational props
        public ICollection<StadiumTranslation> StadiumTranslations { get; set; } = null!;
        public ICollection<Mutimedia> Mutimedias { get; set; } = null!;
        public ICollection<StadiumService> StadiumServices { get; set; } = null!;
        public ICollection<StadiumWorkingDay> StadiumWorkingDays { get; set; } = null!;
        public ICollection<StadiumReview> StadiumReviews { get; set; } = null!;
        public ICollection<StadiumFloor> StadiumFloors { get; set; } = null!;
        public Academy? Academy { get; set; } = null!;
    }
}
