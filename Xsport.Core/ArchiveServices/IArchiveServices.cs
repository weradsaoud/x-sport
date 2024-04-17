using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xsport.DTOs.ArchiveDtos;

namespace Xsport.Core.ArchiveServices
{
    public interface IArchiveServices
    {
        public Task<List<AcademyArchiveItem>> AcademiesSubscriptionArchive(long uId, short currentLanguageId);
    }
}
