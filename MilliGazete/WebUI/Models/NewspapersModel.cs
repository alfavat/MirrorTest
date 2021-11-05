using Entity.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Models
{
    public class NewspapersModel
    {
        public List<NewspaperDto> Newspapers { get; set; }
        public NewspaperDto Newspaper { get; set; }
        public PageDto Page { get; set; }

        
    }
}
