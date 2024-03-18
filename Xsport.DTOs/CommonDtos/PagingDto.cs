using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xsport.DTOs.CommonDtos
{
    public class PagingDto
    {
        [Required]
        public int PageNumber { get; set; }
        [Required]
        public int PageSize { get; set; }
    }
}
