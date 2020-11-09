    using Helpers;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Models;
    using Repository;
    using Services;

    namespace UnitTest
    {
        public class DependacyInjector
        {

            public IConfiguration Configuration { get; }

        public DependacyInjector(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        private static IServiceCollection _ServiceCollection;
          private static readonly ServiceProvider _ServiceProvider;


            public static IServiceCollection ServiceCollection
            {
                get
                {
                    if (_ServiceCollection == null)
                    {
                        _ServiceCollection = new ServiceCollection();

                   // ServiceCollection.Configure<ConfigurationModel>(Configuration.GetSection("Configuration"));

                    //Repository
                    ServiceCollection.AddScoped<IRepositoryBase<Promocion>, MongoDBRepository<Promocion>>();
                        //Service
                        ServiceCollection.AddTransient<IPromocionService, PromocionService>();

                    }
                    return _ServiceCollection;
                }
            }

            public static ServiceProvider ServiceProvider
            {
                get
                {
                    return _ServiceProvider ?? ServiceCollection.BuildServiceProvider();
                }
            }

        }
    }


