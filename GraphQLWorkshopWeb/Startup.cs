using GraphQL;
using GraphQL.Server.Transports.AspNetCore;
using GraphQL.Server.Transports.WebSockets;
using LibraryGraphQLSchema;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace GraphQLWorkshopWeb
{
    public class Startup
    {
        public IConfiguration Configuration { get; private set; }
        public Startup()
        {
            var builder = new ConfigurationBuilder().AddJsonFile("settings.json");
            Configuration = builder.Build();
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IConfiguration>(Configuration);
            services.AddSingleton<AuthorType>();
            services.AddSingleton<AuthorInputType>();
            services.AddSingleton<BookInputType>();
            services.AddSingleton<BookType>();
            services.AddSingleton<LibraryQuery>();
            services.AddSingleton<LibraryMutation>();
            services.AddSingleton<LibrarySchema>();
            services.AddSingleton<IDependencyResolver>(c => new FuncDependencyResolver(type => c.GetRequiredService(type)));
            services.AddGraphQLHttp();
            services.AddGraphQLWebSocket<LibrarySchema>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseWebSockets();
            app.UseGraphQLWebSocket<LibrarySchema>(new GraphQLWebSocketsOptions());
            app.UseGraphQLHttp<LibrarySchema>(new GraphQLHttpOptions());
        }
    }
}
