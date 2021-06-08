using Entity.Dtos;
using Entity.Models;
using System.Collections.Generic;

namespace Business.Helpers.Abstract
{
    public interface IAgencyNewsHelper
    {
        List<AgencyNewsFile> GetFileList(List<AgencyNewsImageDto> images, List<AgencyNewsVideoDto> videos);
    }
}
