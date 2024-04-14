using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xsport.DTOs.CourseDtos.MNGDtos;

namespace Xsport.Core.MNGServices.CourseMNGServices
{
    public interface ICourseMNGService
    {
        public Task<long> CreateCourse(CourseDto dto);
    }
}
