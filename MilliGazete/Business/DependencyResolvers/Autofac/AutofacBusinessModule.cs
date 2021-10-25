using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Helpers.Abstract;
using Business.Helpers.Concrete;
using Business.Managers.Abstract;
using Business.Managers.Concrete;
using Castle.DynamicProxy;
using Core.Utilities.File;
using Core.Utilities.Helper;
using Core.Utilities.Helper.Abstract;
using Core.Utilities.Helper.Concrete;
using Core.Utilities.Interceptors;
using Core.Utilities.Security.Jwt;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;

namespace Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PrayerTimeManager>().As<IPrayerTimeService>();
            builder.RegisterType<PrayerTimeAssistantManager>().As<IPrayerTimeAssistantService>();
            builder.RegisterType<EfPrayerTimeDal>().As<IPrayerTimeDal>();

            builder.RegisterType<NewsHitDetailManager>().As<INewsHitDetailService>();
            builder.RegisterType<NewsHitDetailAssistantManager>().As<INewsHitDetailAssistantService>();
            builder.RegisterType<EfNewsHitDetailDal>().As<INewsHitDetailDal>();

            builder.RegisterType<NewsHitManager>().As<INewsHitService>();
            builder.RegisterType<NewsHitAssistantManager>().As<INewsHitAssistantService>();
            builder.RegisterType<EfNewsHitDal>().As<INewsHitDal>();

            builder.RegisterType<AdvertisementManager>().As<IAdvertisementService>();
            builder.RegisterType<AdvertisementAssistantManager>().As<IAdvertisementAssistantService>();
            builder.RegisterType<EfAdvertisementDal>().As<IAdvertisementDal>();

            builder.RegisterType<MenuManager>().As<IMenuService>();
            builder.RegisterType<MenuAssistantManager>().As<IMenuAssistantService>();
            builder.RegisterType<EfMenuDal>().As<IMenuDal>();

            builder.RegisterType<NewsBookmarkManager>().As<INewsBookmarkService>();
            builder.RegisterType<NewsBookmarkAssistantManager>().As<INewsBookmarkAssistantService>();
            builder.RegisterType<EfNewsBookmarkDal>().As<INewsBookmarkDal>();

            builder.RegisterType<DashboardManager>().As<IDashboardService>();
            builder.RegisterType<DashboardAssistantManager>().As<IDashboardAssistantService>();

            builder.RegisterType<SearchPageManager>().As<ISearchPageService>();
            builder.RegisterType<SearchPageAssistantManager>().As<ISearchPageAssistantService>();

            builder.RegisterType<CategoryPageAssistantManager>().As<ICategoryPageAssistantService>();
            builder.RegisterType<CategoryPageManager>().As<ICategoryPageService>();

            builder.RegisterType<PageManager>().As<IPageService>();
            builder.RegisterType<PageAssistantManager>().As<IPageAssistantService>();
            builder.RegisterType<EfPageDal>().As<IPageDal>();

            builder.RegisterType<ArticleManager>().As<IArticleService>();
            builder.RegisterType<ArticleAssistantManager>().As<IArticleAssistantService>();
            builder.RegisterType<EfArticleDal>().As<IArticleDal>();

            builder.RegisterType<AuthorManager>().As<IAuthorService>();
            builder.RegisterType<AuthorAssistantManager>().As<IAuthorAssistantService>();
            builder.RegisterType<EfAuthorDal>().As<IAuthorDal>();

            builder.RegisterType<NewsCommentLikeManager>().As<INewsCommentLikeService>();
            builder.RegisterType<NewsCommentLikeAssistantManager>().As<INewsCommentLikeAssistantService>();
            builder.RegisterType<EfNewsCommentLikeDal>().As<INewsCommentLikeDal>();

            builder.RegisterType<NewsCommentsHelper>().As<INewsCommentsHelper>();
            builder.RegisterType<NewsCommentManager>().As<INewsCommentService>();
            builder.RegisterType<NewsCommentAssistantManager>().As<INewsCommentAssistantService>();
            builder.RegisterType<EfNewsCommentDal>().As<INewsCommentDal>();

            builder.RegisterType<MainPageManager>().As<IMainPageService>();
            builder.RegisterType<MainPageAssistantManager>().As<IMainPageAssistantService>();

            builder.RegisterType<NewsPositionManager>().As<INewsPositionService>();
            builder.RegisterType<NewsPositionAssistantManager>().As<INewsPositionAssistantService>();
            builder.RegisterType<EfNewsPositionDal>().As<INewsPositionDal>();

            builder.RegisterType<AgencyNewsHelper>().As<IAgencyNewsHelper>();
            builder.RegisterType<AgencyNewsManager>().As<IAgencyNewsService>();
            builder.RegisterType<AgencyNewsAssistantManager>().As<IAgencyNewsAssistantService>();
            builder.RegisterType<EfAgencyNewsDal>().As<IAgencyNewsDal>();

            builder.RegisterType<EntityManager>().As<IEntityService>();
            builder.RegisterType<EntityAssistantManager>().As<IEntityAssistantService>();
            builder.RegisterType<EfEntityDal>().As<IEntityDal>();

            //builder.RegisterType<EntityGroupManager>().As<IEntityGroupService>();
            //builder.RegisterType<EntityGroupAssistantManager>().As<IEntityGroupAssistantService>();
            builder.RegisterType<EfEntityGroupDal>().As<IEntityGroupDal>();

            builder.RegisterType<NewsHelper>().As<INewsHelper>();
            builder.RegisterType<NewsManager>().As<INewsService>();
            builder.RegisterType<NewsAssistantManager>().As<INewsAssistantService>();
            builder.RegisterType<EfNewsDal>().As<INewsDal>();

            //builder.RegisterType<NewsCategoryManager>().As<INewsCategoryService>();
            //builder.RegisterType<NewsCategoryAssistantManager>().As<INewsCategoryAssistantService>();
            builder.RegisterType<EfNewsCategoryDal>().As<INewsCategoryDal>();

            //builder.RegisterType<NewsCounterManager>().As<INewsCounterService>();
            //builder.RegisterType<NewsCounterAssistantManager>().As<INewsCounterAssistantService>();
            builder.RegisterType<EfNewsCounterDal>().As<INewsCounterDal>();

            //builder.RegisterType<NewsFileManager>().As<INewsFileService>();
            //builder.RegisterType<NewsFileAssistantManager>().As<INewsFileAssistantService>();
            builder.RegisterType<EfNewsFileDal>().As<INewsFileDal>();

            //builder.RegisterType<NewsPositionManager>().As<INewsPositionService>();
            //builder.RegisterType<NewsPositionAssistantManager>().As<INewsPositionAssistantService>();
            builder.RegisterType<EfNewsPositionDal>().As<INewsPositionDal>();

            //builder.RegisterType<NewsPropertyManager>().As<INewsPropertyService>();
            //builder.RegisterType<NewsPropertyAssistantManager>().As<INewsPropertyAssistantService>();
            builder.RegisterType<EfNewsPropertyDal>().As<INewsPropertyDal>();

            //builder.RegisterType<NewsRelatedNewsManager>().As<INewsRelatedNewsService>();
            //builder.RegisterType<NewsRelatedNewsAssistantManager>().As<INewsRelatedNewsAssistantService>();
            builder.RegisterType<EfNewsRelatedNewsDal>().As<INewsRelatedNewsDal>();

            //builder.RegisterType<NewsTagManager>().As<INewsTagService>();
            //builder.RegisterType<NewsTagAssistantManager>().As<INewsTagAssistantService>();
            builder.RegisterType<EfNewsTagDal>().As<INewsTagDal>();

            builder.RegisterType<OptionManager>().As<IOptionService>();
            builder.RegisterType<OptionAssistantManager>().As<IOptionAssistantService>();
            builder.RegisterType<EfOptionDal>().As<IOptionDal>();

            builder.RegisterType<TagManager>().As<ITagService>();
            builder.RegisterType<TagAssistantManager>().As<ITagAssistantService>();
            builder.RegisterType<EfTagDal>().As<ITagDal>();

            builder.RegisterType<UserCategoryRelationManager>().As<IUserCategoryRelationService>();
            builder.RegisterType<UserCategoryRelationAssistantManager>().As<IUserCategoryRelationAssistantService>();
            builder.RegisterType<EfUserCategoryRelationDal>().As<IUserCategoryRelationDal>();


            builder.RegisterType<LogManager>().As<ILogService>();
            builder.RegisterType<LogAssistantManager>().As<ILogAssistantService>();
            builder.RegisterType<EfLogDal>().As<ILogDal>();

            builder.RegisterType<BaseManager>().As<IBaseService>();

            builder.RegisterType<CategoryManager>().As<ICategoryService>();
            builder.RegisterType<CategoryAssistantManager>().As<ICategoryAssistantService>();
            builder.RegisterType<EfCategoryDal>().As<ICategoryDal>();

            builder.RegisterType<FileManager>().As<IFileService>();
            builder.RegisterType<FileAssistantManager>().As<IFileAssistantService>();
            builder.RegisterType<EfFileDal>().As<IFileDal>();

            builder.RegisterType<UserManager>().As<IUserService>();
            builder.RegisterType<UserAssistantManager>().As<IUserAssistantService>();
            builder.RegisterType<EfUserDal>().As<IUserDal>();

            builder.RegisterType<CurrencyManager>().As<ICurrencyService>();
            builder.RegisterType<CurrencyAssistantManager>().As<ICurrencyAssistantService>();
            builder.RegisterType<EfCurrencyDal>().As<ICurrencyDal>();

            builder.RegisterType<CityManager>().As<ICityService>();
            builder.RegisterType<CityAssistantManager>().As<ICityAssistantService>();
            builder.RegisterType<EfCityDal>().As<ICityDal>();

            builder.RegisterType<DistrictManager>().As<IDistrictService>();
            builder.RegisterType<DistrictAssistantManager>().As<IDistrictAssistantService>();
            builder.RegisterType<EfDistrictDal>().As<IDistrictDal>();

            builder.RegisterType<SubscriptionManager>().As<ISubscriptionService>();
            builder.RegisterType<SubscriptionAssistantManager>().As<ISubscriptionAssistantService>();
            builder.RegisterType<EfSubscriptionDal>().As<ISubscriptionDal>();

            builder.RegisterType<ContactManager>().As<IContactService>();
            builder.RegisterType<ContactAssistantManager>().As<IContactAssistantService>();
            builder.RegisterType<EfContactDal>().As<IContactDal>();

            builder.RegisterType<QuestionManager>().As<IQuestionService>();
            builder.RegisterType<QuestionAssistantManager>().As<IQuestionAssistantService>();
            builder.RegisterType<EfQuestionDal>().As<IQuestionDal>();

            builder.RegisterType<ReporterManager>().As<IReporterService>();
            builder.RegisterType<ReporterAssistantManager>().As<IReporterAssistantService>();
            builder.RegisterType<EfReporterDal>().As<IReporterDal>();

            builder.RegisterType<QuestionAnswerManager>().As<IQuestionAnswerService>();
            builder.RegisterType<QuestionAnswerAssistantManager>().As<IQuestionAnswerAssistantService>();
            builder.RegisterType<EfQuestionAnswerDal>().As<IQuestionAnswerDal>();

            builder.RegisterType<UserQuestionAnswerManager>().As<IUserQuestionAnswerService>();
            builder.RegisterType<UserQuestionAnswerAssistantManager>().As<IUserQuestionAnswerAssistantService>();
            builder.RegisterType<EfUserQuestionAnswerDal>().As<IUserQuestionAnswerDal>();

            builder.RegisterType<UserOperationClaimManager>().As<IUserOperationClaimService>();
            builder.RegisterType<UserOperationClaimAssistantManager>().As<IUserOperationClaimAssistantService>();
            builder.RegisterType<EfUserOperationClaimDal>().As<IUserOperationClaimDal>();

            builder.RegisterType<OperationClaimManager>().As<IOperationClaimService>();
            builder.RegisterType<OperationClaimAssistantManager>().As<IOperationClaimAssistantService>();
            builder.RegisterType<EfOperationClaimDal>().As<IOperationClaimDal>();

            builder.RegisterType<AuthManager>().As<IAuthService>();
            builder.RegisterType<JwtHelper>().As<ITokenHelper>();
            builder.RegisterType<MailHelper>().As<IMailHelper>();
            builder.RegisterType<DownloadHelper>().As<IDownloadHelper>();
            builder.RegisterType<CategoryHelper>().As<ICategoryHelper>();
            builder.RegisterType<UploadHelper>().As<IUploadHelper>();
            builder.RegisterType<FileHelper>().As<IFileHelper>();

            builder.RegisterType<EfUserPasswordRequestDal>().As<IUserPasswordRequestDal>();
            builder.RegisterType<OneSignalPushNotificationManager>().As<IPushNotificationService>();

            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()
                }).InstancePerLifetimeScope();
        }
    }
}
