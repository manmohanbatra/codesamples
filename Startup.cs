using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReadersApi.Providers;
using Swashbuckle.AspNetCore.Swagger;

namespace ReadersApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IConfigManager, ConfigManager>();
            services.AddSingleton<ITokenManager, TokenManager>();
            services.AddSingleton<IReaderRepo, ReaderRepo>();
            services.AddScoped<ITemplateHelper, TemplateHelper>();
            services.AddSingleton<IMailHelper, MailHelper>();

            // //services.AddSingleton<IUserRepo, ClassReaderAdapter>();
            // services.AddSingleton<IUserRepo, ObjectReaderAdapter>();

            //build the service pipeline and fetch the service instance of ConfigManager to pass to the JWT Authentication Middleware
            var sp = services.BuildServiceProvider();
            var config = sp.GetRequiredService<IConfigManager>();

            services.AddBearerAuthentication(config);

            // services.AddDbContext<MyContext>(builder =>
            // {
            //     builder.UseSqlServer(config.ConnectionString);
            // });


            // services.ConfigureOptions<ConfigureJwtBearerOptions>();
            // services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();

            services.AddMvc();

            // services.AddSwaggerGen(c =>
            // {
            //     c.SwaggerDoc("v1", new Info { Title = "Reader API", Version = "V1" });
            // });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseAuthentication();
            app.UseMvcWithDefaultRoute();
            app.UseStaticFiles();
            // app.UseSwagger();

            // app.UseSwaggerUI(c =>
            // {
            //     c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            // });
        }
    }
}
