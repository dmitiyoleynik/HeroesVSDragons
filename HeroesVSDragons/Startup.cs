using DragonLibrary_.Services;
using DragonLibrary_.Models;
using GraphQL;
using GraphQL.Server;
using GraphQL.Server.Ui.GraphiQL;
using GraphQL.Server.Ui.Playground;
using GraphQL.Server.Ui.Voyager;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Microsoft.Extensions.Configuration;

namespace HeroesVSDragons
{
    public class Startup
    {
        public Startup(Microsoft.AspNetCore.Hosting.IHostingEnvironment environment)
        {
            Environment = environment;
        }

        public Microsoft.AspNetCore.Hosting.IHostingEnvironment Environment { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<IISServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).
                AddJwtBearer(options=> 
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                        ValidateIssuerSigningKey = true,
                    };
                });
            ILogger logger = new LoggerConfiguration()
                    .ReadFrom.Configuration(new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json")
                    .Build())
                .CreateLogger();

            services.AddSingleton(logger);
            services.AddSingleton<IJWTService,JWTService>();
            services.AddSingleton<IHeroService, HeroService>();
            services.AddSingleton<IDragonService, DragonService>();
            services.AddSingleton<IHitService,HitService>();
            services.AddTransient<HeroType>();
            services.AddTransient<DragonType>();
            services.AddTransient<HitType>();
            services.AddTransient<HeroSchema>();
            services.AddTransient<DragonSchema>();
            services.AddTransient<HitSchema>();
            services.AddTransient<HeroQuery>();
            services.AddTransient<DragonQuery>();
            services.AddTransient<HitQuery>();
            services.AddTransient<HeroMutation>();
            services.AddTransient<DragonMutation>();

            services.AddSingleton<IDependencyResolver>(
                c => new FuncDependencyResolver(type => c.GetRequiredService(type)));
            services.AddGraphQL(options => {
                options.EnableMetrics = true;
                options.ExposeExceptions = Environment.IsDevelopment();
            })
            .AddWebSockets()
            .AddDataLoader();
            ////
            services.AddControllersWithViews();
            ////
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            ///////
            app.UseAuthentication();
            app.UseAuthorization();
            ///////
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseWebSockets();
            app.UseGraphQLWebSockets<DragonSchema>("/graphql/dragon");
            app.UseGraphQL<DragonSchema>("/graphql/dragon");
            app.UseGraphQLWebSockets<HeroSchema>("/graphql/hero");
            app.UseGraphQL<HeroSchema>("/graphql/hero");
            app.UseGraphQLWebSockets<HitSchema>("/graphql/hit");
            app.UseGraphQL<HitSchema>("/graphql/hit");

            app.UseGraphQLPlayground(new GraphQLPlaygroundOptions()
            {
                Path = "/ui/playground"
            });
            app.UseGraphiQLServer(new GraphiQLOptions
            {
                GraphiQLPath = "/ui/graphiql/hit",
                GraphQLEndPoint = "/graphql/hit"
            });
            app.UseGraphQLVoyager(new GraphQLVoyagerOptions()
            {
                GraphQLEndPoint = "/graphql",
                Path = "/ui/voyager"
            });
        }
    }
}
