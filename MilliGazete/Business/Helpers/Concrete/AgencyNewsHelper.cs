using Business.Helpers.Abstract;
using Entity.Dtos;
using Entity.Models;
using System.Collections.Generic;
using System.Linq;

namespace Business.Helpers.Concrete
{
    public class AgencyNewsHelper : IAgencyNewsHelper
    {
        public List<AgencyNewsFile> GetFileList(List<AgencyNewsImageDto> images, List<AgencyNewsVideoDto> videos)
        {
            var files = new List<AgencyNewsFile>();
            if (images != null && images.Any())
            {
                images.ForEach(item =>
                {
                    files.Add(new AgencyNewsFile
                    {
                        Description = item.Description,
                        FileId = null,
                        Link = item.Url,
                        FileType = "image"
                    });
                });
            }
            if (videos != null && videos.Any())
            {
                videos.ForEach(item =>
                {
                    files.Add(new AgencyNewsFile
                    {
                        FileId = null,
                        Link = item.Url,
                        FileType = "video"
                    });
                });
            }
            return files;
        }
    }
}
