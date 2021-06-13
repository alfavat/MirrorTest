using Entity.Models;
using System;

namespace Entity.Dtos
{
    public class SubscriptionDto
    {
        public int Id { get; set; }
        public int CityId { get; set; }
        public int DistrictId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public CityDto City { get; set; }
        public DistrictDto District { get; set; }
    }
}
