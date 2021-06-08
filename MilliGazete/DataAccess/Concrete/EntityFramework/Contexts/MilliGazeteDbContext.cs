using Entity.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework.Contexts
{
     
    public partial class MilliGazeteDbContext : DbContext
    {
        public MilliGazeteDbContext(DbContextOptions<MilliGazeteDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Advertisement> Advertisement { get; set; }
        public virtual DbSet<AgencyNews> AgencyNews { get; set; }
        public virtual DbSet<AgencyNewsFile> AgencyNewsFile { get; set; }
        public virtual DbSet<Article> Article { get; set; }
        public virtual DbSet<ArticleTag> ArticleTag { get; set; }
        public virtual DbSet<Author> Author { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Currency> Currency { get; set; }
        public virtual DbSet<Entity.Models.Entity> Entity { get; set; }
        public virtual DbSet<EntityGroup> EntityGroup { get; set; }
        public virtual DbSet<File> File { get; set; }
        public virtual DbSet<Log> Log { get; set; }
        public virtual DbSet<Menu> Menu { get; set; }
        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<NewsBookmark> NewsBookmark { get; set; }
        public virtual DbSet<NewsCategory> NewsCategory { get; set; }
        public virtual DbSet<NewsComment> NewsComment { get; set; }
        public virtual DbSet<NewsCommentLike> NewsCommentLike { get; set; }
        public virtual DbSet<NewsCounter> NewsCounter { get; set; }
        public virtual DbSet<NewsFile> NewsFile { get; set; }
        public virtual DbSet<NewsHit> NewsHit { get; set; }
        public virtual DbSet<NewsHitDetail> NewsHitDetail { get; set; }
        public virtual DbSet<NewsPosition> NewsPosition { get; set; }
        public virtual DbSet<NewsProperty> NewsProperty { get; set; }
        public virtual DbSet<NewsRelatedNews> NewsRelatedNews { get; set; }
        public virtual DbSet<NewsTag> NewsTag { get; set; }
        public virtual DbSet<OperationClaim> OperationClaim { get; set; }
        public virtual DbSet<Option> Option { get; set; }
        public virtual DbSet<Page> Page { get; set; }
        public virtual DbSet<Tag> Tag { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserCategoryRelation> UserCategoryRelation { get; set; }
        public virtual DbSet<UserOperationClaim> UserOperationClaim { get; set; }
        public virtual DbSet<UserPasswordRequest> UserPasswordRequest { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Advertisement>(entity =>
            {
                entity.ToTable("advertisement");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Active).HasColumnName("active");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.Deleted).HasColumnName("deleted");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasColumnType("character varying");

                entity.Property(e => e.GoogleId)
                    .HasColumnName("google_id")
                    .HasColumnType("character varying");

                entity.Property(e => e.Height).HasColumnName("height");

                entity.Property(e => e.Key)
                    .HasColumnName("key")
                    .HasColumnType("character varying");

                entity.Property(e => e.Width).HasColumnName("width");
            });

            modelBuilder.Entity<AgencyNews>(entity =>
            {
                entity.ToTable("agency_news");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Category)
                    .HasColumnName("category")
                    .HasColumnType("character varying");

                entity.Property(e => e.City)
                    .HasColumnName("city")
                    .HasColumnType("character varying");

                entity.Property(e => e.Code)
                    .HasColumnName("code")
                    .HasColumnType("character varying");

                entity.Property(e => e.Country)
                    .HasColumnName("country")
                    .HasColumnType("character varying");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasColumnType("character varying");

                entity.Property(e => e.ImageUpdateDate)
                    .HasColumnName("image_update_date")
                    .HasColumnType("date");

                entity.Property(e => e.LastMinute)
                    .HasColumnName("last_minute")
                    .HasColumnType("character varying");

                entity.Property(e => e.NewsAgencyEntityId).HasColumnName("news_agency_entity_id");

                entity.Property(e => e.NewsId).HasColumnName("news_id");

                entity.Property(e => e.ParentCategory)
                    .HasColumnName("parent_category")
                    .HasColumnType("character varying");

                entity.Property(e => e.PublishDate).HasColumnName("publish_date");

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .HasColumnType("character varying");

                entity.Property(e => e.UpdateDate).HasColumnName("update_date");

                entity.Property(e => e.VideoUpdateDate)
                    .HasColumnName("video_update_date")
                    .HasColumnType("date");
            });

            modelBuilder.Entity<AgencyNewsFile>(entity =>
            {
                entity.ToTable("agency_news_file");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.AgencyNewsId).HasColumnName("agency_news_id");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasColumnType("character varying");

                entity.Property(e => e.FileId).HasColumnName("file_id");

                entity.Property(e => e.FileType)
                    .HasColumnName("file_type")
                    .HasMaxLength(50);

                entity.Property(e => e.Link)
                    .HasColumnName("link")
                    .HasColumnType("character varying");

                entity.HasOne(d => d.AgencyNews)
                    .WithMany(p => p.AgencyNewsFile)
                    .HasForeignKey(d => d.AgencyNewsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("agency_news_relation");

                entity.HasOne(d => d.File)
                    .WithMany(p => p.AgencyNewsFile)
                    .HasForeignKey(d => d.FileId)
                    .HasConstraintName("agency_news_file_relation");
            });

            modelBuilder.Entity<Article>(entity =>
            {
                entity.ToTable("article");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Approved).HasColumnName("approved");

                entity.Property(e => e.AuthorId).HasColumnName("author_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.Deleted).HasColumnName("deleted");

                entity.Property(e => e.HtmlContent)
                    .HasColumnName("html_content")
                    .HasColumnType("character varying");

                entity.Property(e => e.ReadCount).HasColumnName("read_count");

                entity.Property(e => e.SeoDescription)
                    .HasColumnName("seo_description")
                    .HasColumnType("character varying");

                entity.Property(e => e.SeoKeywords)
                    .HasColumnName("seo_keywords")
                    .HasColumnType("character varying");

                entity.Property(e => e.SeoTitle)
                    .HasColumnName("seo_title")
                    .HasMaxLength(250);

                entity.Property(e => e.ShortDescription)
                    .HasColumnName("short_description")
                    .HasColumnType("character varying");

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .HasMaxLength(250);

                entity.Property(e => e.Url)
                    .HasColumnName("url")
                    .HasMaxLength(250);

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.Article)
                    .HasForeignKey(d => d.AuthorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("article_auhtor_id_fkey");
            });

            modelBuilder.Entity<ArticleTag>(entity =>
            {
                entity.ToTable("article_tag");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.ArticleId).HasColumnName("article_id");

                entity.Property(e => e.TagId).HasColumnName("tag_id");

                entity.HasOne(d => d.Article)
                    .WithMany(p => p.ArticleTag)
                    .HasForeignKey(d => d.ArticleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("article_id_fkey");

                entity.HasOne(d => d.Tag)
                    .WithMany(p => p.ArticleTag)
                    .HasForeignKey(d => d.TagId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("tag_id_fkey");
            });

            modelBuilder.Entity<Author>(entity =>
            {
                entity.ToTable("author");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("date")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.Deleted).HasColumnName("deleted");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(50);

                entity.Property(e => e.FeaturedImageFileId).HasColumnName("featured_image_file_id");

                entity.Property(e => e.HtmlBiography)
                    .HasColumnName("html_biography")
                    .HasColumnType("character varying");

                entity.Property(e => e.Instagram)
                    .HasColumnName("instagram")
                    .HasColumnType("character varying");

                entity.Property(e => e.NameSurename)
                    .HasColumnName("name_surename")
                    .HasMaxLength(50);

                entity.Property(e => e.PhotoFileId).HasColumnName("photo_file_id");

                entity.Property(e => e.SeoDescription)
                    .HasColumnName("seo_description")
                    .HasColumnType("character varying");

                entity.Property(e => e.SeoKeywords)
                    .HasColumnName("seo_keywords")
                    .HasColumnType("character varying");

                entity.Property(e => e.SeoTitle)
                    .HasColumnName("seo_title")
                    .HasColumnType("character varying");

                entity.Property(e => e.Twitter)
                    .HasColumnName("twitter")
                    .HasColumnType("character varying");

                entity.Property(e => e.Url)
                    .HasColumnName("url")
                    .HasMaxLength(250);

                entity.Property(e => e.Web)
                    .HasColumnName("web")
                    .HasColumnType("character varying");

                entity.HasOne(d => d.FeaturedImageFile)
                    .WithMany(p => p.AuthorFeaturedImageFile)
                    .HasForeignKey(d => d.FeaturedImageFileId)
                    .HasConstraintName("featured_image_file_id_fkey");

                entity.HasOne(d => d.PhotoFile)
                    .WithMany(p => p.AuthorPhotoFile)
                    .HasForeignKey(d => d.PhotoFileId)
                    .HasConstraintName("photo_file_id_fkey");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("category");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasIdentityOptions(15L, null, null, null, null, null)
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Active).HasColumnName("active");

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasColumnName("category_name")
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.Deleted).HasColumnName("deleted");

                entity.Property(e => e.FeaturedImageFileId).HasColumnName("featured_image_file_id");

                entity.Property(e => e.HeadingPositionEntityId).HasColumnName("heading_position_entity_id");

                entity.Property(e => e.IsStatic)
                    .HasColumnName("is_static")
                    .HasDefaultValueSql("false");

                entity.Property(e => e.ParentCategoryId).HasColumnName("parent_category_id");

                entity.Property(e => e.SeoDescription)
                    .HasColumnName("seo_description")
                    .HasColumnType("character varying");

                entity.Property(e => e.SeoKeywords)
                    .HasColumnName("seo_keywords")
                    .HasColumnType("character varying");

                entity.Property(e => e.StyleCode)
                    .IsRequired()
                    .HasColumnName("style_code")
                    .HasMaxLength(250);

                entity.Property(e => e.Symbol)
                    .IsRequired()
                    .HasColumnName("symbol")
                    .HasMaxLength(50);

                entity.Property(e => e.Url)
                    .IsRequired()
                    .HasColumnName("url")
                    .HasMaxLength(250);

                entity.HasOne(d => d.FeaturedImageFile)
                    .WithMany(p => p.Category)
                    .HasForeignKey(d => d.FeaturedImageFileId)
                    .HasConstraintName("featured_image_file_id_fkey");

                entity.HasOne(d => d.HeadingPositionEntity)
                    .WithMany(p => p.Category)
                    .HasForeignKey(d => d.HeadingPositionEntityId)
                    .HasConstraintName("category__entity_id_fkey");

                entity.HasOne(d => d.ParentCategory)
                    .WithMany(p => p.InverseParentCategory)
                    .HasForeignKey(d => d.ParentCategoryId)
                    .HasConstraintName("category_parent_category_id_fkey");
            });

            modelBuilder.Entity<Currency>(entity =>
            {
                entity.ToTable("currency");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.CurrencyName)
                    .IsRequired()
                    .HasColumnName("currency_name")
                    .HasColumnType("character varying");

                entity.Property(e => e.CurrencyValue).HasColumnName("currency_value");

                entity.Property(e => e.DailyChangePercent).HasColumnName("daily_change_percent");

                entity.Property(e => e.DailyChangeStatus).HasColumnName("daily_change_status");

                entity.Property(e => e.LastUpdateDate).HasColumnName("last_update_date");

                entity.Property(e => e.ShortKey)
                    .HasColumnName("short_key")
                    .HasColumnType("character varying");

                entity.Property(e => e.Symbol)
                    .HasColumnName("symbol")
                    .HasColumnType("character varying");
            });

            modelBuilder.Entity<Entity.Models.Entity>(entity =>
            {
                entity.ToTable("entity");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasIdentityOptions(24L, null, null, null, null, null)
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.EntityGroupId).HasColumnName("entity_group_id");

                entity.Property(e => e.EntityName)
                    .HasColumnName("entity_name")
                    .HasColumnType("character varying");

                entity.HasOne(d => d.EntityGroup)
                    .WithMany(p => p.Entity)
                    .HasForeignKey(d => d.EntityGroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("entity_entity_group_id_fkey");
            });

            modelBuilder.Entity<EntityGroup>(entity =>
            {
                entity.ToTable("entity_group");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasIdentityOptions(7L, null, null, null, null, null)
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.EntityGroupName)
                    .HasColumnName("entity_group_name")
                    .HasColumnType("character varying");
            });

            modelBuilder.Entity<File>(entity =>
            {
                entity.ToTable("file");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.Deleted).HasColumnName("deleted");

                entity.Property(e => e.FileName)
                    .HasColumnName("file_name")
                    .HasColumnType("character varying");

                entity.Property(e => e.FileSizeKb)
                    .HasColumnName("file_size_kb")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.FileType)
                    .HasColumnName("file_type")
                    .HasColumnType("character varying");

                entity.Property(e => e.Height)
                    .HasColumnName("height")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.IsCdnFile)
                    .HasColumnName("is_cdn_file")
                    .HasDefaultValueSql("false");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.VideoSound)
                    .IsRequired()
                    .HasColumnName("video_sound")
                    .HasDefaultValueSql("true");

                entity.Property(e => e.Width)
                    .HasColumnName("width")
                    .HasDefaultValueSql("0");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.File)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("file_user_id_fkey");
            });

            modelBuilder.Entity<Log>(entity =>
            {
                entity.ToTable("log");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Audit)
                    .HasColumnName("audit")
                    .HasColumnType("character varying");

                entity.Property(e => e.ClassName)
                    .HasColumnName("class_name")
                    .HasColumnType("character varying");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.Detail)
                    .HasColumnName("detail")
                    .HasColumnType("character varying");

                entity.Property(e => e.MethodName)
                    .HasColumnName("method_name")
                    .HasColumnType("character varying");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Log)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("log_user_id_fkey");
            });

            modelBuilder.Entity<Menu>(entity =>
            {
                entity.ToTable("menu");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Active).HasColumnName("active");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.Deleted).HasColumnName("deleted");

                entity.Property(e => e.ParentMenuId).HasColumnName("parent_menu_id");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasMaxLength(250);

                entity.Property(e => e.Url)
                    .HasColumnName("url")
                    .HasColumnType("character varying");

                entity.HasOne(d => d.ParentMenu)
                    .WithMany(p => p.InverseParentMenu)
                    .HasForeignKey(d => d.ParentMenuId)
                    .HasConstraintName("parent_fkey");
            });

            modelBuilder.Entity<News>(entity =>
            {
                entity.ToTable("news");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Active).HasColumnName("active");

                entity.Property(e => e.AddUserId).HasColumnName("add_user_id");

                entity.Property(e => e.Approved)
                    .IsRequired()
                    .HasColumnName("approved")
                    .HasDefaultValueSql("true");

                entity.Property(e => e.AuthorId).HasColumnName("author_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.Deleted).HasColumnName("deleted");

                entity.Property(e => e.HistoryNo).HasColumnName("history_no");

                entity.Property(e => e.HtmlContent)
                    .HasColumnName("html_content")
                    .HasColumnType("character varying");

                entity.Property(e => e.InnerTitle)
                    .HasColumnName("inner_title")
                    .HasColumnType("character varying");

                entity.Property(e => e.IsDraft).HasColumnName("is_draft");

                entity.Property(e => e.IsLastNews).HasColumnName("is_last_news");

                entity.Property(e => e.NewsAgencyEntityId).HasColumnName("news_agency_entity_id");

                entity.Property(e => e.NewsTypeEntityId).HasColumnName("news_type_entity_id");

                entity.Property(e => e.PublishDate)
                    .HasColumnName("publish_date")
                    .HasColumnType("date")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.PublishTime)
                    .HasColumnName("publish_time")
                    .HasColumnType("time without time zone")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.SeoDescription)
                    .HasColumnName("seo_description")
                    .HasColumnType("character varying");

                entity.Property(e => e.SeoKeywords)
                    .HasColumnName("seo_keywords")
                    .HasColumnType("character varying");

                entity.Property(e => e.SeoTitle)
                    .HasColumnName("seo_title")
                    .HasMaxLength(250);

                entity.Property(e => e.ShortDescription)
                    .HasColumnName("short_description")
                    .HasColumnType("character varying");

                entity.Property(e => e.SocialDescription)
                    .HasColumnName("social_description")
                    .HasColumnType("character varying");

                entity.Property(e => e.SocialTitle)
                    .HasColumnName("social_title")
                    .HasMaxLength(250);

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .HasMaxLength(250);

                entity.Property(e => e.UpdateUserId).HasColumnName("update_user_id");

                entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");

                entity.Property(e => e.Url)
                    .HasColumnName("url")
                    .HasMaxLength(250);

                entity.HasOne(d => d.AddUser)
                    .WithMany(p => p.NewsAddUser)
                    .HasForeignKey(d => d.AddUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("news_add_user_id_fkey");

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.News)
                    .HasForeignKey(d => d.AuthorId)
                    .HasConstraintName("author_id_fkey");

                entity.HasOne(d => d.NewsAgencyEntity)
                    .WithMany(p => p.NewsNewsAgencyEntity)
                    .HasForeignKey(d => d.NewsAgencyEntityId)
                    .HasConstraintName("news_news_agency_entity_id_fkey");

                entity.HasOne(d => d.NewsTypeEntity)
                    .WithMany(p => p.NewsNewsTypeEntity)
                    .HasForeignKey(d => d.NewsTypeEntityId)
                    .HasConstraintName("news_news_type_entity_id_fkey");

                entity.HasOne(d => d.UpdateUser)
                    .WithMany(p => p.NewsUpdateUser)
                    .HasForeignKey(d => d.UpdateUserId)
                    .HasConstraintName("news_update_user_id_fkey");
            });

            modelBuilder.Entity<NewsBookmark>(entity =>
            {
                entity.ToTable("news_bookmark");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.NewsId).HasColumnName("news_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.News)
                    .WithMany(p => p.NewsBookmark)
                    .HasForeignKey(d => d.NewsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bookmark_news_id_fkey");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.NewsBookmark)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bookmark_user_id_fkey");
            });

            modelBuilder.Entity<NewsCategory>(entity =>
            {
                entity.ToTable("news_category");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.NewsId).HasColumnName("news_id");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.NewsCategory)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("news_category_category_id_fkey");

                entity.HasOne(d => d.News)
                    .WithMany(p => p.NewsCategory)
                    .HasForeignKey(d => d.NewsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("news_category_news_id_fkey");
            });

            modelBuilder.Entity<NewsComment>(entity =>
            {
                entity.ToTable("news_comment");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Approved).HasColumnName("approved");

                entity.Property(e => e.Content)
                    .HasColumnName("content")
                    .HasColumnType("character varying");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.Deleted).HasColumnName("deleted");

                entity.Property(e => e.NewsId).HasColumnName("news_id");

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .HasMaxLength(200);

                entity.Property(e => e.TotalLikeCount).HasColumnName("total_like_count");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.News)
                    .WithMany(p => p.NewsComment)
                    .HasForeignKey(d => d.NewsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("news_comment_id_fkey");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.NewsComment)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("user_comment_id_fkey");
            });

            modelBuilder.Entity<NewsCommentLike>(entity =>
            {
                entity.ToTable("news_comment_like");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("date")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.IsLike)
                    .IsRequired()
                    .HasColumnName("is_like")
                    .HasDefaultValueSql("true");

                entity.Property(e => e.NewsCommentId).HasColumnName("news_comment_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.NewsComment)
                    .WithMany(p => p.NewsCommentLike)
                    .HasForeignKey(d => d.NewsCommentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("news_comment_like_id_fkey");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.NewsCommentLike)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("user_comment_like_id_fkey");
            });

            modelBuilder.Entity<NewsCounter>(entity =>
            {
                entity.ToTable("news_counter");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.CounterEntityId).HasColumnName("counter_entity_id");

                entity.Property(e => e.NewsId).HasColumnName("news_id");

                entity.Property(e => e.Value)
                    .HasColumnName("value")
                    .HasDefaultValueSql("0");

                entity.HasOne(d => d.CounterEntity)
                    .WithMany(p => p.NewsCounter)
                    .HasForeignKey(d => d.CounterEntityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("news_counter_counter_entity_id_fkey");

                entity.HasOne(d => d.News)
                    .WithMany(p => p.NewsCounter)
                    .HasForeignKey(d => d.NewsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("news_counter_news_id_fkey");
            });

            modelBuilder.Entity<NewsFile>(entity =>
            {
                entity.ToTable("news_file");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasColumnType("character varying");

                entity.Property(e => e.FileId).HasColumnName("file_id");

                entity.Property(e => e.NewsFileTypeEntityId).HasColumnName("news_file_type_entity_id");

                entity.Property(e => e.NewsId).HasColumnName("news_id");

                entity.Property(e => e.Order)
                    .HasColumnName("order")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .HasColumnType("character varying");

                entity.Property(e => e.VideoCoverFileId).HasColumnName("video_cover_file_id");

                entity.HasOne(d => d.File)
                    .WithMany(p => p.NewsFileFile)
                    .HasForeignKey(d => d.FileId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("news_file_file_id_fkey");

                entity.HasOne(d => d.NewsFileTypeEntity)
                    .WithMany(p => p.NewsFile)
                    .HasForeignKey(d => d.NewsFileTypeEntityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("news_file_news_file_type_entity_id_fkey");

                entity.HasOne(d => d.News)
                    .WithMany(p => p.NewsFile)
                    .HasForeignKey(d => d.NewsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("news_file_news_id_fkey");

                entity.HasOne(d => d.VideoCoverFile)
                    .WithMany(p => p.NewsFileVideoCoverFile)
                    .HasForeignKey(d => d.VideoCoverFileId)
                    .HasConstraintName("news_file_video_cover_file_id_fkey");
            });

            modelBuilder.Entity<NewsHit>(entity =>
            {
                entity.ToTable("news_hit");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.NewsHitComeFromEntityId).HasColumnName("news_hit_come_from_entity_id");

                entity.Property(e => e.NewsId).HasColumnName("news_id");

                entity.HasOne(d => d.News)
                    .WithMany(p => p.NewsHit)
                    .HasForeignKey(d => d.NewsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("news_id_fkey");
            });

            modelBuilder.Entity<NewsHitDetail>(entity =>
            {
                entity.ToTable("news_hit_detail");

                entity.HasComment(" burada hangi user, ne zaman, hangi ip ile, hangi habere işlem yapmış bunu saklayalım");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.IpAddress)
                    .HasColumnName("ip_address")
                    .HasMaxLength(50);

                entity.Property(e => e.NewsHitComeFromEntityId).HasColumnName("news_hit_come_from_entity_id");

                entity.Property(e => e.NewsId).HasColumnName(" news_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.News)
                    .WithMany(p => p.NewsHitDetail)
                    .HasForeignKey(d => d.NewsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("news_id_fkey");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.NewsHitDetail)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("user_id_fkey");
            });

            modelBuilder.Entity<NewsPosition>(entity =>
            {
                entity.ToTable("news_position");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.NewsId).HasColumnName("news_id");

                entity.Property(e => e.Order)
                    .HasColumnName("order")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.PositionEntityId).HasColumnName("position_entity_id");

                entity.Property(e => e.Value)
                    .HasColumnName("value")
                    .HasDefaultValueSql("false");

                entity.HasOne(d => d.News)
                    .WithMany(p => p.NewsPosition)
                    .HasForeignKey(d => d.NewsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("news_position_news_id_fkey");

                entity.HasOne(d => d.PositionEntity)
                    .WithMany(p => p.NewsPosition)
                    .HasForeignKey(d => d.PositionEntityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("news_position_position_entity_id_fkey");
            });

            modelBuilder.Entity<NewsProperty>(entity =>
            {
                entity.ToTable("news_property");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.NewsId).HasColumnName("news_id");

                entity.Property(e => e.PropertyEntityId).HasColumnName("property_entity_id");

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasColumnName("value")
                    .HasDefaultValueSql("true");

                entity.HasOne(d => d.News)
                    .WithMany(p => p.NewsProperty)
                    .HasForeignKey(d => d.NewsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("news_properties_news_id_fkey");

                entity.HasOne(d => d.PropertyEntity)
                    .WithMany(p => p.NewsProperty)
                    .HasForeignKey(d => d.PropertyEntityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("news_properties_property_entity_id_fkey");
            });

            modelBuilder.Entity<NewsRelatedNews>(entity =>
            {
                entity.ToTable("news_related_news");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.NewsId).HasColumnName("news_id");

                entity.Property(e => e.RelatedNewsId).HasColumnName("related_news_id");

                entity.HasOne(d => d.News)
                    .WithMany(p => p.NewsRelatedNewsNews)
                    .HasForeignKey(d => d.NewsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("news_related_news_news_id_fkey");

                entity.HasOne(d => d.RelatedNews)
                    .WithMany(p => p.NewsRelatedNewsRelatedNews)
                    .HasForeignKey(d => d.RelatedNewsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("news_related_news_related_news_id_fkey");
            });

            modelBuilder.Entity<NewsTag>(entity =>
            {
                entity.ToTable("news_tag");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.NewsId).HasColumnName("news_id");

                entity.Property(e => e.TagId).HasColumnName("tag_id");

                entity.HasOne(d => d.News)
                    .WithMany(p => p.NewsTag)
                    .HasForeignKey(d => d.NewsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("news_tag_news_id_fkey");

                entity.HasOne(d => d.Tag)
                    .WithMany(p => p.NewsTag)
                    .HasForeignKey(d => d.TagId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("news_tag_tag_id_fkey");
            });

            modelBuilder.Entity<OperationClaim>(entity =>
            {
                entity.ToTable("operation_claim");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasIdentityOptions(6L, null, null, null, null, null)
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.ClaimName)
                    .HasColumnName("claim_name")
                    .HasColumnType("character varying");
            });

            modelBuilder.Entity<Option>(entity =>
            {
                entity.ToTable("option");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.AdEmail).HasColumnType("character varying");

                entity.Property(e => e.AdPhone).HasColumnType("character varying");

                entity.Property(e => e.Address)
                    .HasColumnName("address")
                    .HasMaxLength(250);

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(50);

                entity.Property(e => e.Facebook)
                    .HasColumnName("facebook")
                    .HasColumnType("character varying");

                entity.Property(e => e.Fax)
                    .HasColumnName("fax")
                    .HasMaxLength(20);

                entity.Property(e => e.Instagram)
                    .HasColumnName("instagram")
                    .HasColumnType("character varying");

                entity.Property(e => e.LiveVideoActive).HasColumnName("live_video_active");

                entity.Property(e => e.LiveVideoLink)
                    .HasColumnName("live_video_link")
                    .HasColumnType("character varying");

                entity.Property(e => e.PageRefreshPeriod).HasColumnName("page_refresh_period");

                entity.Property(e => e.SeoDescription)
                    .HasColumnName("seo_description")
                    .HasMaxLength(250);

                entity.Property(e => e.SeoKeywords)
                    .HasColumnName("seo_keywords")
                    .HasMaxLength(250);

                entity.Property(e => e.Telephone)
                    .HasColumnName("telephone")
                    .HasMaxLength(20);

                entity.Property(e => e.Twitter)
                    .HasColumnName("twitter")
                    .HasColumnType("character varying");

                entity.Property(e => e.WebsiteSlogan)
                    .HasColumnName("website_slogan")
                    .HasMaxLength(250);

                entity.Property(e => e.WebsiteTitle)
                    .IsRequired()
                    .HasColumnName("website_title")
                    .HasMaxLength(100);

                entity.Property(e => e.Whatsapp)
                    .HasColumnName("whatsapp")
                    .HasColumnType("character varying");

                entity.Property(e => e.Youtube)
                    .HasColumnName("youtube")
                    .HasColumnType("character varying");
            });

            modelBuilder.Entity<Page>(entity =>
            {
                entity.ToTable("page");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.Deleted).HasColumnName("deleted");

                entity.Property(e => e.FeaturedImageFileId).HasColumnName("featured_image_file_id");

                entity.Property(e => e.HtmlContent)
                    .HasColumnName("html_content")
                    .HasColumnType("character varying");

                entity.Property(e => e.SeoDescription)
                    .HasColumnName("seo_description")
                    .HasColumnType("character varying");

                entity.Property(e => e.SeoKeywords)
                    .HasColumnName("seo_keywords")
                    .HasColumnType("character varying");

                entity.Property(e => e.SeoTitle)
                    .HasColumnName("seo_title")
                    .HasColumnType("character varying");

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .HasMaxLength(250);

                entity.Property(e => e.Url)
                    .HasColumnName("url")
                    .HasMaxLength(250);

                entity.HasOne(d => d.FeaturedImageFile)
                    .WithMany(p => p.Page)
                    .HasForeignKey(d => d.FeaturedImageFileId)
                    .HasConstraintName("featured_image_file_id_fkey");
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.ToTable("tag");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasIdentityOptions(19L, null, null, null, null, null)
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Active).HasColumnName("active");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.Deleted).HasColumnName("deleted");

                entity.Property(e => e.TagName)
                    .IsRequired()
                    .HasColumnName("tag_name")
                    .HasMaxLength(100);

                entity.Property(e => e.Url)
                    .HasColumnName("url")
                    .HasMaxLength(250);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasIdentityOptions(44L, null, null, null, null, null)
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Active).HasColumnName("active");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.Deleted).HasColumnName("deleted");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(50);

                entity.Property(e => e.FirstName)
                    .HasColumnName("first_name")
                    .HasMaxLength(50);

                entity.Property(e => e.IsEmployee).HasColumnName("is_employee");

                entity.Property(e => e.LastLoginDate).HasColumnName("last_login_date");

                entity.Property(e => e.LastLoginIpAddress)
                    .HasColumnName("last_login_ip_address")
                    .HasColumnType("character varying");

                entity.Property(e => e.LastName)
                    .HasColumnName("last_name")
                    .HasMaxLength(50);

                entity.Property(e => e.PasswordHash).HasColumnName("password_hash");

                entity.Property(e => e.PasswordSalt).HasColumnName("password_salt");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasColumnName("user_name")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<UserCategoryRelation>(entity =>
            {
                entity.ToTable("user_category_relation");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasIdentityOptions(10L, null, null, null, null, null)
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.UserCategoryRelation)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("user_category_relation_category_id_fkey");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserCategoryRelation)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("user_category_relation_user_id_fkey");
            });

            modelBuilder.Entity<UserOperationClaim>(entity =>
            {
                entity.ToTable("user_operation_claim");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.OperationClaimId).HasColumnName("operation_claim_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.OperationClaim)
                    .WithMany(p => p.UserOperationClaim)
                    .HasForeignKey(d => d.OperationClaimId)
                    .HasConstraintName("user_operation_claim_operation_claim_id_fkey");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserOperationClaim)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("user_operation_claim_user_id_fkey");
            });

            modelBuilder.Entity<UserPasswordRequest>(entity =>
            {
                entity.ToTable("user_password_request");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasIdentityOptions(5L, null, null, null, null, null)
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.ExpirationDate).HasColumnName("expiration_date");

                entity.Property(e => e.Token)
                    .HasColumnName("token")
                    .HasColumnType("character varying");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserPasswordRequest)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("user_password_request_user_id_fkey");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
