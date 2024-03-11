using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xsport.DTOs.UserDtos
{
    public class EditPreferencesReqDto
    {
        public long PreferenceId { get; set; }
        public long ValueId { get; set; }
    }
    //public class PreferenceValue
    //{
    //    public long PreferenceId { get; set; }
    //    public long ValueId { get; set; }
    //}
}
