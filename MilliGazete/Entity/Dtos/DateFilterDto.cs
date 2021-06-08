using Entity.Abstract;
using System;

namespace Entity.Dtos
{
    public class DateFilterDto : IDto
    {
        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }
    }
}
