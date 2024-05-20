using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xsport.DB.Entities;
using Xsport.DTOs.StadiumDtos.DashboardDtos;

namespace Xsport.Core.DashboardServices.StadiumServices
{
    public interface IDashboardStadiumServices
    {
        Task<List<StadiumCreationProcess>> StartProcess(string userId);
        Task<StadiumCreationProcess> SaveStep(StadiumStepDataModel model);
        Task<long> AddStadiumStepOne(StadiumStepOneDto step1DTO);
        Task AddStadiumStepTwo(long stadiumId, StadiumStepTwoDto step2DTO);
        Task AddStadiumStepThree(long stadiumId, StadiumStepThreeDto step3DTO);

    }
}
