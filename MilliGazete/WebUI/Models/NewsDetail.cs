using Entity.Dtos;
using System.Collections.Generic;

namespace WebUI.Models
{
    public class NewsDetail: NewsDetailPageDto
    {
        public List<UserNewsCommentDto> CommentList { get; set; }
        public int CommentsCount { get; set; }
        public int RelatedNewsCount { get; set; }
    }
}
