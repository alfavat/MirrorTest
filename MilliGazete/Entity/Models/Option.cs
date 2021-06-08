using System;
using System.Collections.Generic;

namespace Entity.Models
{
    public partial class Option
    {
        public int Id { get; set; }
        public string WebsiteTitle { get; set; }
        public string WebsiteSlogan { get; set; }
        public string SeoKeywords { get; set; }
        public string SeoDescription { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string Fax { get; set; }
        public string Address { get; set; }
        public int? PageRefreshPeriod { get; set; }
        public string LiveVideoLink { get; set; }
        public string Facebook { get; set; }
        public string Instagram { get; set; }
        public string Youtube { get; set; }
        public string Twitter { get; set; }
        public string Whatsapp { get; set; }
        public bool LiveVideoActive { get; set; }
        public string AdPhone { get; set; }
        public string AdEmail { get; set; }
    }
}
