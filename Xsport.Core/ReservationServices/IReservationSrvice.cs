using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xsport.DTOs.ReservationDtos;
using Xsport.DTOs.StadiumDtos;

namespace Xsport.Core.ReservationServices
{
    public interface IReservationSrvice
    {
        public Task<List<SuggestedStadiumDto>> GetSportStadiums(Criteria criteria, short currentLanguageId);
        public Task<List<ReservedDay>> GetReservedTimes(long stadiumFloorId, short currentLanguageId);
        public Task<ReservationDetails> Reserve(ReserveDto dto, long uId, short currentLangugeId);
    }
}
