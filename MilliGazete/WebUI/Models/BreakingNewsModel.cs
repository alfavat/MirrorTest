using Entity.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Models
{
    public class BreakingNewsModel
    {
        public List<BreakingNewsDto> BreakingNews { get; set; }
        public List<FlashNewsDto> FlashNews { get; set; }
    }
}
