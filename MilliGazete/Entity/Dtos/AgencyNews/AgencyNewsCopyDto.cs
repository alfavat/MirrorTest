using System.Collections.Generic;

namespace Entity.Dtos
{
    public class AgencyNewsCopyDto
    {
        public string Code { get; set; }// it could be Guid or NewsId or ....
        public int AgencyNewsId { get; set; }
        public int? NewsTypeEntityId { get; set; }
        public List<NewsCategoryAddDto> NewsCategoryList { get; set; }
        public List<AgencyNewsCopyFileDto> NewsFileList { get; set; }
        public List<int> NewsPositionEntityIdList { get; set; }
    }
}