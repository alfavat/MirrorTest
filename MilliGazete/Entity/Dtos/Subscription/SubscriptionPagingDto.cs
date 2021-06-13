using System;

namespace Entity.Dtos
{
    public class SubscriptionPagingDto : PagingDto
    {
        public int? CityId { get; set; }
        public int? DistrictId { get; set; }
        public DateTime? FromCreatedAt { get; set; }
        public DateTime? ToCreatedAt { get; set; }
    }
}
