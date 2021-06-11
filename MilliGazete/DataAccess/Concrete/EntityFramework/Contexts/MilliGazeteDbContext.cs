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

        public virtual DbSet<Advertisement> Advertisements { get; set; }
        public virtual DbSet<AgencyNews> AgencyNews { get; set; }
        public virtual DbSet<AgencyNewsFile> AgencyNewsFiles { get; set; }
        public virtual DbSet<Article> Articles { get; set; }
        public virtual DbSet<ArticleTag> ArticleTags { get; set; }
        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Currency> Currencies { get; set; }
        public virtual DbSet<Entity.Models.Entity> Entities { get; set; }
        public virtual DbSet<EntityGroup> EntityGroups { get; set; }
        public virtual DbSet<File> Files { get; set; }
        public virtual DbSet<Log> Logs { get; set; }
        public virtual DbSet<Menu> Menus { get; set; }
        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<NewsBookmark> NewsBookmarks { get; set; }
        public virtual DbSet<NewsCategory> NewsCategories { get; set; }
        public virtual DbSet<NewsComment> NewsComments { get; set; }
        public virtual DbSet<NewsCommentLike> NewsCommentLikes { get; set; }
        public virtual DbSet<NewsCounter> NewsCounters { get; set; }
        public virtual DbSet<NewsFile> NewsFiles { get; set; }
        public virtual DbSet<NewsHit> NewsHits { get; set; }
        public virtual DbSet<NewsHitDetail> NewsHitDetails { get; set; }
        public virtual DbSet<NewsPosition> NewsPositions { get; set; }
        public virtual DbSet<NewsProperty> NewsProperties { get; set; }
        public virtual DbSet<NewsRelatedNews> NewsRelatedNews { get; set; }
        public virtual DbSet<NewsTag> NewsTags { get; set; }
        public virtual DbSet<OperationClaim> OperationClaims { get; set; }
        public virtual DbSet<Option> Options { get; set; }
        public virtual DbSet<Page> Pages { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserCategoryRelation> UserCategoryRelations { get; set; }
        public virtual DbSet<UserOperationClaim> UserOperationClaims { get; set; }
        public virtual DbSet<UserPasswordRequest> UserPasswordRequests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "English_United States.1252");

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
                    .HasColumnType("character varying")
                    .HasColumnName("description");

                entity.Property(e => e.GoogleId)
                    .HasColumnType("character varying")
                    .HasColumnName("google_id");

                entity.Property(e => e.Height).HasColumnName("height");

                entity.Property(e => e.Key)
                    .HasColumnType("character varying")
                    .HasColumnName("key");

                entity.Property(e => e.Width).HasColumnName("width");
            });

            modelBuilder.Entity<AgencyNews>(entity =>
            {
                entity.ToTable("agency_news");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Category)
                    .HasColumnType("character varying")
                    .HasColumnName("category");

                entity.Property(e => e.City)
                    .HasColumnType("character varying")
                    .HasColumnName("city");

                entity.Property(e => e.Code)
                    .HasColumnType("character varying")
                    .HasColumnName("code");

                entity.Property(e => e.Country)
                    .HasColumnType("character varying")
                    .HasColumnName("country");

                entity.Property(e => e.Description)
                    .HasColumnType("character varying")
                    .HasColumnName("description");

                entity.Property(e => e.ImageUpdateDate)
                    .HasColumnType("date")
                    .HasColumnName("image_update_date");

                entity.Property(e => e.LastMinute)
                    .HasColumnType("character varying")
                    .HasColumnName("last_minute");

                entity.Property(e => e.NewsAgencyEntityId).HasColumnName("news_agency_entity_id");

                entity.Property(e => e.NewsId).HasColumnName("news_id");

                entity.Property(e => e.ParentCategory)
                    .HasColumnType("character varying")
                    .HasColumnName("parent_category");

                entity.Property(e => e.PublishDate).HasColumnName("publish_date");

                entity.Property(e => e.Title)
                    .HasColumnType("character varying")
                    .HasColumnName("title");

                entity.Property(e => e.UpdateDate).HasColumnName("update_date");

                entity.Property(e => e.VideoUpdateDate)
                    .HasColumnType("date")
                    .HasColumnName("video_update_date");
            });

            modelBuilder.Entity<AgencyNewsFile>(entity =>
            {
                entity.ToTable("agency_news_file");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.AgencyNewsId).HasColumnName("agency_news_id");

                entity.Property(e => e.Description)
                    .HasColumnType("character varying")
                    .HasColumnName("description");

                entity.Property(e => e.FileId).HasColumnName("file_id");

                entity.Property(e => e.FileType)
                    .HasMaxLength(50)
                    .HasColumnName("file_type");

                entity.Property(e => e.Link)
                    .HasColumnType("character varying")
                    .HasColumnName("link");

                entity.HasOne(d => d.AgencyNews)
                    .WithMany(p => p.AgencyNewsFiles)
                    .HasForeignKey(d => d.AgencyNewsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("agency_news_relation");

                entity.HasOne(d => d.File)
                    .WithMany(p => p.AgencyNewsFiles)
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
                    .HasColumnType("character varying")
                    .HasColumnName("html_content");

                entity.Property(e => e.ReadCount).HasColumnName("read_count");

                entity.Property(e => e.SeoDescription)
                    .HasColumnType("character varying")
                    .HasColumnName("seo_description");

                entity.Property(e => e.SeoKeywords)
                    .HasColumnType("character varying")
                    .HasColumnName("seo_keywords");

                entity.Property(e => e.SeoTitle)
                    .HasMaxLength(250)
                    .HasColumnName("seo_title");

                entity.Property(e => e.ShortDescription)
                    .HasColumnType("character varying")
                    .HasColumnName("short_description");

                entity.Property(e => e.Title)
                    .HasMaxLength(250)
                    .HasColumnName("title");

                entity.Property(e => e.Url)
                    .HasMaxLength(250)
                    .HasColumnName("url");

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.Articles)
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
                    .WithMany(p => p.ArticleTags)
                    .HasForeignKey(d => d.ArticleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("article_id_fkey");

                entity.HasOne(d => d.Tag)
                    .WithMany(p => p.ArticleTags)
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
                    .HasColumnType("date")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.Deleted).HasColumnName("deleted");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .HasColumnName("email");

                entity.Property(e => e.FeaturedImageFileId).HasColumnName("featured_image_file_id");

                entity.Property(e => e.HtmlBiography)
                    .HasColumnType("character varying")
                    .HasColumnName("html_biography");

                entity.Property(e => e.Instagram)
                    .HasColumnType("character varying")
                    .HasColumnName("instagram");

                entity.Property(e => e.NameSurename)
                    .HasMaxLength(50)
                    .HasColumnName("name_surename");

                entity.Property(e => e.PhotoFileId).HasColumnName("photo_file_id");

                entity.Property(e => e.SeoDescription)
                    .HasColumnType("character varying")
                    .HasColumnName("seo_description");

                entity.Property(e => e.SeoKeywords)
                    .HasColumnType("character varying")
                    .HasColumnName("seo_keywords");

                entity.Property(e => e.SeoTitle)
                    .HasColumnType("character varying")
                    .HasColumnName("seo_title");

                entity.Property(e => e.Twitter)
                    .HasColumnType("character varying")
                    .HasColumnName("twitter");

                entity.Property(e => e.Url)
                    .HasMaxLength(250)
                    .HasColumnName("url");

                entity.Property(e => e.Web)
                    .HasColumnType("character varying")
                    .HasColumnName("web");

                entity.HasOne(d => d.FeaturedImageFile)
                    .WithMany(p => p.AuthorFeaturedImageFiles)
                    .HasForeignKey(d => d.FeaturedImageFileId)
                    .HasConstraintName("featured_image_file_id_fkey");

                entity.HasOne(d => d.PhotoFile)
                    .WithMany(p => p.AuthorPhotoFiles)
                    .HasForeignKey(d => d.PhotoFileId)
                    .HasConstraintName("photo_file_id_fkey");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("category");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn()
                    .HasIdentityOptions(15L, null, null, null, null, null);

                entity.Property(e => e.Active).HasColumnName("active");

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("category_name");

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
                    .HasColumnType("character varying")
                    .HasColumnName("seo_description");

                entity.Property(e => e.SeoKeywords)
                    .HasColumnType("character varying")
                    .HasColumnName("seo_keywords");

                entity.Property(e => e.StyleCode)
                    .IsRequired()
                    .HasMaxLength(250)
                    .HasColumnName("style_code");

                entity.Property(e => e.Symbol)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("symbol");

                entity.Property(e => e.Url)
                    .IsRequired()
                    .HasMaxLength(250)
                    .HasColumnName("url");

                entity.HasOne(d => d.FeaturedImageFile)
                    .WithMany(p => p.Categories)
                    .HasForeignKey(d => d.FeaturedImageFileId)
                    .HasConstraintName("featured_image_file_id_fkey");

                entity.HasOne(d => d.HeadingPositionEntity)
                    .WithMany(p => p.Categories)
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
                    .HasColumnType("character varying")
                    .HasColumnName("currency_name");

                entity.Property(e => e.CurrencyValue).HasColumnName("currency_value");

                entity.Property(e => e.DailyChangePercent).HasColumnName("daily_change_percent");

                entity.Property(e => e.DailyChangeStatus).HasColumnName("daily_change_status");

                entity.Property(e => e.LastUpdateDate).HasColumnName("last_update_date");

                entity.Property(e => e.ShortKey)
                    .HasColumnType("character varying")
                    .HasColumnName("short_key");

                entity.Property(e => e.Symbol)
                    .HasColumnType("character varying")
                    .HasColumnName("symbol");
            });

            modelBuilder.Entity<Entity.Models.Entity>(entity =>
            {
                entity.ToTable("entity");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn()
                    .HasIdentityOptions(24L, null, null, null, null, null);

                entity.Property(e => e.EntityGroupId).HasColumnName("entity_group_id");

                entity.Property(e => e.EntityName)
                    .HasColumnType("character varying")
                    .HasColumnName("entity_name");

                entity.HasOne(d => d.EntityGroup)
                    .WithMany(p => p.Entities)
                    .HasForeignKey(d => d.EntityGroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("entity_entity_group_id_fkey");
            });

            modelBuilder.Entity<EntityGroup>(entity =>
            {
                entity.ToTable("entity_group");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn()
                    .HasIdentityOptions(7L, null, null, null, null, null);

                entity.Property(e => e.EntityGroupName)
                    .HasColumnType("character varying")
                    .HasColumnName("entity_group_name");
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
                    .HasColumnType("character varying")
                    .HasColumnName("file_name");

                entity.Property(e => e.FileSizeKb)
                    .HasColumnName("file_size_kb")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.FileType)
                    .HasColumnType("character varying")
                    .HasColumnName("file_type");

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
                    .WithMany(p => p.Files)
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
                    .HasColumnType("character varying")
                    .HasColumnName("audit");

                entity.Property(e => e.ClassName)
                    .HasColumnType("character varying")
                    .HasColumnName("class_name");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.Detail)
                    .HasColumnType("character varying")
                    .HasColumnName("detail");

                entity.Property(e => e.MethodName)
                    .HasColumnType("character varying")
                    .HasColumnName("method_name");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Logs)
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
                    .HasMaxLength(250)
                    .HasColumnName("title");

                entity.Property(e => e.Url)
                    .HasColumnType("character varying")
                    .HasColumnName("url");

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
                    .HasColumnType("character varying")
                    .HasColumnName("html_content");

                entity.Property(e => e.InnerTitle)
                    .HasColumnType("character varying")
                    .HasColumnName("inner_title");

                entity.Property(e => e.IsDraft).HasColumnName("is_draft");

                entity.Property(e => e.IsLastNews).HasColumnName("is_last_news");

                entity.Property(e => e.NewsAgencyEntityId).HasColumnName("news_agency_entity_id");

                entity.Property(e => e.NewsTypeEntityId).HasColumnName("news_type_entity_id");

                entity.Property(e => e.PublishDate)
                    .HasColumnType("date")
                    .HasColumnName("publish_date")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.PublishTime)
                    .HasColumnType("time without time zone")
                    .HasColumnName("publish_time")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.SeoDescription)
                    .HasColumnType("character varying")
                    .HasColumnName("seo_description");

                entity.Property(e => e.SeoKeywords)
                    .HasColumnType("character varying")
                    .HasColumnName("seo_keywords");

                entity.Property(e => e.SeoTitle)
                    .HasMaxLength(250)
                    .HasColumnName("seo_title");

                entity.Property(e => e.ShortDescription)
                    .HasColumnType("character varying")
                    .HasColumnName("short_description");

                entity.Property(e => e.SocialDescription)
                    .HasColumnType("character varying")
                    .HasColumnName("social_description");

                entity.Property(e => e.SocialTitle)
                    .HasMaxLength(250)
                    .HasColumnName("social_title");

                entity.Property(e => e.Title)
                    .HasMaxLength(250)
                    .HasColumnName("title");

                entity.Property(e => e.UpdateUserId).HasColumnName("update_user_id");

                entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");

                entity.Property(e => e.Url)
                    .HasMaxLength(250)
                    .HasColumnName("url");

                entity.HasOne(d => d.AddUser)
                    .WithMany(p => p.NewsAddUsers)
                    .HasForeignKey(d => d.AddUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("news_add_user_id_fkey");

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.News)
                    .HasForeignKey(d => d.AuthorId)
                    .HasConstraintName("author_id_fkey");

                entity.HasOne(d => d.NewsAgencyEntity)
                    .WithMany(p => p.NewsNewsAgencyEntities)
                    .HasForeignKey(d => d.NewsAgencyEntityId)
                    .HasConstraintName("news_news_agency_entity_id_fkey");

                entity.HasOne(d => d.NewsTypeEntity)
                    .WithMany(p => p.NewsNewsTypeEntities)
                    .HasForeignKey(d => d.NewsTypeEntityId)
                    .HasConstraintName("news_news_type_entity_id_fkey");

                entity.HasOne(d => d.UpdateUser)
                    .WithMany(p => p.NewsUpdateUsers)
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
                    .WithMany(p => p.NewsBookmarks)
                    .HasForeignKey(d => d.NewsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bookmark_news_id_fkey");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.NewsBookmarks)
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
                    .WithMany(p => p.NewsCategories)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("news_category_category_id_fkey");

                entity.HasOne(d => d.News)
                    .WithMany(p => p.NewsCategories)
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

                entity.Property(e => e.AnonymousUsername)
                    .HasMaxLength(50)
                    .HasColumnName("anonymous_username");

                entity.Property(e => e.Approved).HasColumnName("approved");

                entity.Property(e => e.Content)
                    .HasColumnType("character varying")
                    .HasColumnName("content");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.Deleted).HasColumnName("deleted");

                entity.Property(e => e.InitialLikeCount).HasColumnName("initial_like_count");

                entity.Property(e => e.IpAddress)
                    .HasMaxLength(50)
                    .HasColumnName("ip_address");

                entity.Property(e => e.NewsId).HasColumnName("news_id");

                entity.Property(e => e.Title)
                    .HasMaxLength(200)
                    .HasColumnName("title");

                entity.Property(e => e.TotalLikeCount).HasColumnName("total_like_count");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.News)
                    .WithMany(p => p.NewsComments)
                    .HasForeignKey(d => d.NewsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("news_comment_id_fkey");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.NewsComments)
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
                    .HasColumnType("date")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.IsLike)
                    .IsRequired()
                    .HasColumnName("is_like")
                    .HasDefaultValueSql("true");

                entity.Property(e => e.NewsCommentId).HasColumnName("news_comment_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.NewsComment)
                    .WithMany(p => p.NewsCommentLikes)
                    .HasForeignKey(d => d.NewsCommentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("news_comment_like_id_fkey");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.NewsCommentLikes)
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
                    .WithMany(p => p.NewsCounters)
                    .HasForeignKey(d => d.CounterEntityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("news_counter_counter_entity_id_fkey");

                entity.HasOne(d => d.News)
                    .WithMany(p => p.NewsCounters)
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
                    .HasColumnType("character varying")
                    .HasColumnName("description");

                entity.Property(e => e.FileId).HasColumnName("file_id");

                entity.Property(e => e.NewsFileTypeEntityId).HasColumnName("news_file_type_entity_id");

                entity.Property(e => e.NewsId).HasColumnName("news_id");

                entity.Property(e => e.Order)
                    .HasColumnName("order")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Title)
                    .HasColumnType("character varying")
                    .HasColumnName("title");

                entity.Property(e => e.VideoCoverFileId).HasColumnName("video_cover_file_id");

                entity.HasOne(d => d.File)
                    .WithMany(p => p.NewsFileFiles)
                    .HasForeignKey(d => d.FileId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("news_file_file_id_fkey");

                entity.HasOne(d => d.NewsFileTypeEntity)
                    .WithMany(p => p.NewsFiles)
                    .HasForeignKey(d => d.NewsFileTypeEntityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("news_file_news_file_type_entity_id_fkey");

                entity.HasOne(d => d.News)
                    .WithMany(p => p.NewsFiles)
                    .HasForeignKey(d => d.NewsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("news_file_news_id_fkey");

                entity.HasOne(d => d.VideoCoverFile)
                    .WithMany(p => p.NewsFileVideoCoverFiles)
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
                    .WithMany(p => p.NewsHits)
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
                    .HasMaxLength(50)
                    .HasColumnName("ip_address");

                entity.Property(e => e.NewsHitComeFromEntityId).HasColumnName("news_hit_come_from_entity_id");

                entity.Property(e => e.NewsId).HasColumnName(" news_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.News)
                    .WithMany(p => p.NewsHitDetails)
                    .HasForeignKey(d => d.NewsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("news_id_fkey");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.NewsHitDetails)
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
                    .WithMany(p => p.NewsPositions)
                    .HasForeignKey(d => d.NewsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("news_position_news_id_fkey");

                entity.HasOne(d => d.PositionEntity)
                    .WithMany(p => p.NewsPositions)
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
                    .WithMany(p => p.NewsProperties)
                    .HasForeignKey(d => d.NewsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("news_properties_news_id_fkey");

                entity.HasOne(d => d.PropertyEntity)
                    .WithMany(p => p.NewsProperties)
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
                    .WithMany(p => p.NewsTags)
                    .HasForeignKey(d => d.NewsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("news_tag_news_id_fkey");

                entity.HasOne(d => d.Tag)
                    .WithMany(p => p.NewsTags)
                    .HasForeignKey(d => d.TagId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("news_tag_tag_id_fkey");
            });

            modelBuilder.Entity<OperationClaim>(entity =>
            {
                entity.ToTable("operation_claim");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn()
                    .HasIdentityOptions(6L, null, null, null, null, null);

                entity.Property(e => e.ClaimName)
                    .HasColumnType("character varying")
                    .HasColumnName("claim_name");
            });

            modelBuilder.Entity<Option>(entity =>
            {
                entity.ToTable("option");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.AdEmail).HasColumnType("character varying");

                entity.Property(e => e.AdPhone).HasColumnType("character varying");

                entity.Property(e => e.Address)
                    .HasMaxLength(250)
                    .HasColumnName("address");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .HasColumnName("email");

                entity.Property(e => e.Facebook)
                    .HasColumnType("character varying")
                    .HasColumnName("facebook");

                entity.Property(e => e.Fax)
                    .HasMaxLength(20)
                    .HasColumnName("fax");

                entity.Property(e => e.Instagram)
                    .HasColumnType("character varying")
                    .HasColumnName("instagram");

                entity.Property(e => e.LiveVideoActive).HasColumnName("live_video_active");

                entity.Property(e => e.LiveVideoLink)
                    .HasColumnType("character varying")
                    .HasColumnName("live_video_link");

                entity.Property(e => e.PageRefreshPeriod).HasColumnName("page_refresh_period");

                entity.Property(e => e.SeoDescription)
                    .HasMaxLength(250)
                    .HasColumnName("seo_description");

                entity.Property(e => e.SeoKeywords)
                    .HasMaxLength(250)
                    .HasColumnName("seo_keywords");

                entity.Property(e => e.Telephone)
                    .HasMaxLength(20)
                    .HasColumnName("telephone");

                entity.Property(e => e.Twitter)
                    .HasColumnType("character varying")
                    .HasColumnName("twitter");

                entity.Property(e => e.WebsiteSlogan)
                    .HasMaxLength(250)
                    .HasColumnName("website_slogan");

                entity.Property(e => e.WebsiteTitle)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("website_title");

                entity.Property(e => e.Whatsapp)
                    .HasColumnType("character varying")
                    .HasColumnName("whatsapp");

                entity.Property(e => e.Youtube)
                    .HasColumnType("character varying")
                    .HasColumnName("youtube");
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
                    .HasColumnType("character varying")
                    .HasColumnName("html_content");

                entity.Property(e => e.SeoDescription)
                    .HasColumnType("character varying")
                    .HasColumnName("seo_description");

                entity.Property(e => e.SeoKeywords)
                    .HasColumnType("character varying")
                    .HasColumnName("seo_keywords");

                entity.Property(e => e.SeoTitle)
                    .HasColumnType("character varying")
                    .HasColumnName("seo_title");

                entity.Property(e => e.Title)
                    .HasMaxLength(250)
                    .HasColumnName("title");

                entity.Property(e => e.Url)
                    .HasMaxLength(250)
                    .HasColumnName("url");

                entity.HasOne(d => d.FeaturedImageFile)
                    .WithMany(p => p.Pages)
                    .HasForeignKey(d => d.FeaturedImageFileId)
                    .HasConstraintName("featured_image_file_id_fkey");
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.ToTable("tag");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn()
                    .HasIdentityOptions(19L, null, null, null, null, null);

                entity.Property(e => e.Active).HasColumnName("active");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.Deleted).HasColumnName("deleted");

                entity.Property(e => e.TagName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("tag_name");

                entity.Property(e => e.Url)
                    .HasMaxLength(250)
                    .HasColumnName("url");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn()
                    .HasIdentityOptions(44L, null, null, null, null, null);

                entity.Property(e => e.Active).HasColumnName("active");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.Deleted).HasColumnName("deleted");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("email");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .HasColumnName("first_name");

                entity.Property(e => e.IsEmployee).HasColumnName("is_employee");

                entity.Property(e => e.LastLoginDate).HasColumnName("last_login_date");

                entity.Property(e => e.LastLoginIpAddress)
                    .HasColumnType("character varying")
                    .HasColumnName("last_login_ip_address");

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .HasColumnName("last_name");

                entity.Property(e => e.PasswordHash).HasColumnName("password_hash");

                entity.Property(e => e.PasswordSalt).HasColumnName("password_salt");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("user_name");
            });

            modelBuilder.Entity<UserCategoryRelation>(entity =>
            {
                entity.ToTable("user_category_relation");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn()
                    .HasIdentityOptions(10L, null, null, null, null, null);

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.UserCategoryRelations)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("user_category_relation_category_id_fkey");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserCategoryRelations)
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
                    .WithMany(p => p.UserOperationClaims)
                    .HasForeignKey(d => d.OperationClaimId)
                    .HasConstraintName("user_operation_claim_operation_claim_id_fkey");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserOperationClaims)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("user_operation_claim_user_id_fkey");
            });

            modelBuilder.Entity<UserPasswordRequest>(entity =>
            {
                entity.ToTable("user_password_request");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn()
                    .HasIdentityOptions(5L, null, null, null, null, null);

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.ExpirationDate).HasColumnName("expiration_date");

                entity.Property(e => e.Token)
                    .HasColumnType("character varying")
                    .HasColumnName("token");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserPasswordRequests)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("user_password_request_user_id_fkey");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
