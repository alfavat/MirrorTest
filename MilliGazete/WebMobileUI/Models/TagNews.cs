using Entity.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMobileUI.Models
{
    public class TagNews
    {
        public string Tag { get; set; }
        public List<MainPageTagNewsDto> Data { get; set; }
    }
}
