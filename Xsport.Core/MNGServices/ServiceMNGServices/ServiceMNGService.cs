using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xsport.Common.Enums;
using Xsport.Common.Utils;
using Xsport.DB;
using Xsport.DB.Entities;
using Xsport.DTOs.ServiceDtos.MNGDtos;

namespace Xsport.Core.MNGServices.ServiceMNGServices
{
    public class ServiceMNGService : IServiceMNGService
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private IRepositoryManager _repManager { get; set; }
        public ServiceMNGService(
            IWebHostEnvironment _webHostEnvironment,
            IRepositoryManager repManager,
            IHttpContextAccessor _httpContextAccessor)
        {
            webHostEnvironment = _webHostEnvironment;
            _repManager = repManager;
            httpContextAccessor = _httpContextAccessor;
        }
        public async Task<long> CreateService(PostServiceDto dto)
        {
            try
            {
                string imgPath = (dto.Img == null) ? string.Empty
                : await Utils.UploadImageFileAsync(dto.Img, 10, webHostEnvironment);
                Service service = new Service()
                {
                    ServiceTranslations = new List<ServiceTranslation>()
                    {
                        new ServiceTranslation()
                        {
                            LanguageId = (long)LanguagesEnum.Arabic,
                            Name = dto.ArName,
                        },
                        new ServiceTranslation()
                        {
                            LanguageId = (long)LanguagesEnum.English,
                            Name = dto.EnName,
                        }
                    },
                    ImgPath = imgPath
                };
                await _repManager.ServiceRepository.CreateAsync(service);
                await _repManager.ServiceRepository.SaveChangesAsync();
                return service.ServiceId;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<List<GetServiceDto>> GetServices()
        {
            try
            {
                string domainName = httpContextAccessor.HttpContext?.Request.Scheme 
                    + "://" + httpContextAccessor.HttpContext?.Request.Host.Value;
                return await _repManager.ServiceRepository.FindAll(false).Select(s => new GetServiceDto()
                {
                    Id = s.ServiceId,
                    ArName = s.ServiceTranslations
                    .Single(t => t.LanguageId == (long)LanguagesEnum.Arabic).Name,
                    EnName = s.ServiceTranslations
                    .Single(t => t.LanguageId == (long)LanguagesEnum.English).Name,
                    ImgPath = string.IsNullOrEmpty(s.ImgPath) ? "" : domainName + "/Images/" + s.ImgPath
                }).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}
