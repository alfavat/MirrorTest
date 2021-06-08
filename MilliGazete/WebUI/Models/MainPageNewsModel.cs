using Entity.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Models
{
    public class MainPageNewsModel
    {
        public List<MainPageNewsDto> mainPageNews { get; set; }
        public List<WideHeadingNewsDto> mainPageWideNews { get; set; }
    }
}
