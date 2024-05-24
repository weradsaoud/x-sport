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
        Task<List<StadiumProcessCreationDto>> GetStadiumCreatopnProcesses(long userId);
        Task<StadiumStepDataModel> SaveStep(long userId, StadiumStepDataModel model);
        Task<long> CompleteCreationProcess(long userId, long processId);
        //Task<long> AddStadiumStepOne(StadiumStepOneDto step1DTO);
        //Task AddStadiumStepTwo(long stadiumId, StadiumStepTwoDto step2DTO);
        //Task AddStadiumStepThree(long stadiumId, StadiumStepThreeDto step3DTO);

    }
}
