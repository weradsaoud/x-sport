using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xsport.Common.Enums;
using Xsport.DB.Entities;
using Xsport.DTOs.AcademyDtos;
using Xsport.DTOs.AdminDtos;

namespace Xsport.DB.RepositoryInterfaces
{
    public interface IAcademyRepository : IRepositoryBase<Academy>
    {
        public Task<bool> AddWorkingDays(AddWorkingDaysDto dto);

    }
}
