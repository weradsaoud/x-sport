using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xsport.DB;
using Xsport.DTOs.UserDtos;

namespace Xsport.Core.SportServices
{
    public class SportServices : ISportServices
    {
        private readonly AppDbContext _db;
        public SportServices(AppDbContext db)
        {
            _db = db;
        }
        public async Task<List<SportDto>> GetSports(long CurrentLanguageId)
        {
            try
            {
                return await _db.Sports.Select(s => new SportDto()
                {
                    SportId = s.SportId,
                    SportName = s.SportTranslations.SingleOrDefault(t => t.LanguageId == CurrentLanguageId).Name ?? string.Empty
                }).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
