using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xsport.Common.Models
{
    public class PagingInfo
    {
        [Required]
        public int PageSize { get; set; }
        [Required]
        public int PageNumber { get; set; } = 0;
    }
}
