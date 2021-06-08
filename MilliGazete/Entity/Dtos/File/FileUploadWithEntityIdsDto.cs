using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
namespace Entity.Dtos
{
    public class FileUploadWithEntityIdsDto
    {
        public IFormFile File { get; set; }
        public List<int> FileTypeEntityIdList { get; set; }
    }
}
