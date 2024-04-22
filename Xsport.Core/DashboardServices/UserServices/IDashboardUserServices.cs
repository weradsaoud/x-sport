using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xsport.DTOs.UserDtos;

namespace Xsport.Core.DashboardServices.UserServices
{
    public interface IDashboardUserServices
    {
        public Task<bool> Register(UserRegistrationDto dto);
    }
}
