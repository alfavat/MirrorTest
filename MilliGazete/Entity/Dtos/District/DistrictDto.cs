using Entity.Models;
using System;

namespace Entity.Dtos
{
    public class DistrictDto
    {
        public int Id { get; set; }
        public int CityId { get; set; }
        public string CityName { get; set; }
        public string Name { get; set; }
    }
}
