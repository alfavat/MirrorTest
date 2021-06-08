using System.Collections.Generic;

namespace Entity.Dtos
{
    public class AgencyNewsCopyFileDto
    {
        public int AgencyNewsFileId { get; set; }
        public List<int> NewsFileTypeEntityIdList { get; set; }
    }
}
