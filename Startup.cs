using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Antiforgery;
using WebApp.Filters;
//using Microsoft.AspNetCore.Razor.TagHelpers;
//using WebApp.TagHelpers;
namespace WebApp
{
    public class Startup
    {
        public Startup(IConfiguration config)
        {
            Configuration = config;
        }

        public IConfiguration Configuration { get; set; }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddControllersWithViews();
            services.AddDbContext<DataContext>(opts => {
                opts.UseSqlServer(Configuration[
                "ConnectionStrings:ProductConnection"]);
                opts.EnableSensitiveDataLogging(true);
               // services.AddControllersWithViews();
            });
            //services.AddControllers();
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddRazorPages().AddRazorRuntimeCompilation();

            //services.AddDistributedMemoryCache();
            //services.AddSession(options => {
            //    options.Cookie.IsEssential = true;
            //});
            //services.Configure<RazorPagesOptions>(opts => {
            //    opts.Conventions.AddPageRoute("/Index",
            //    "/extra/page/{id:long?}");
            //});
            services.AddSingleton<CitiesData>();
            //services.AddTransient<ITagHelperComponent,
            //TimeTagHelperComponent > ();
            //services.AddTransient<ITagHelperComponent,
            // TableFooterTagHelperComponent>();

            //services.AddControllers().AddNewtonsoftJson();
            //services.AddControllers().AddNewtonsoftJson().AddXmlSerializerFormatters();
            //services.Configure<MvcNewtonsoftJsonOptions>(opts => {
            //    opts.SerializerSettings.NullValueHandling
            //    = Newtonsoft.Json.NullValueHandling.Ignore;
            //});
            //services.Configure<MvcOptions>(opts => {
            //    opts.RespectBrowserAcceptHeader = true;
            //    opts.ReturnHttpNotAcceptable = true;
            //});
            //services.AddSwaggerGen(options => {
            //    options.SwaggerDoc("v1",
            //    new OpenApiInfo { Title = "WebApp", Version = "v1" });
            //});
            //services.AddControllers();
            //services.Configure<JsonOptions>(opts => {
            //    opts.JsonSerializerOptions.IgnoreNullValues = true;
            //});
            services.Configure<AntiforgeryOptions>(opts => {
                opts.HeaderName = "X-XSRF-TOKEN";
            });
            services.AddScoped<GuidResponseAttribute>();

            services.Configure<MvcOptions>(opts => opts.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(value => "Please enter a value"));
           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DataContext context, IAntiforgery antiforgery)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            //app.UseSession();

            app.UseRouting();
            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
               // endpoints.MapControllerRoute("forms","controllers/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapDefaultControllerRoute();
                endpoints.MapRazorPages();
                //app.UseMiddleware<TestMiddleware>();



                //app.UseEndpoints(endpoints => {
                //    endpoints.MapGet("/", async context => {
                //        await context.Response.WriteAsync("Hello World!");
                //    });
                //endpoints.MapWebService();

                endpoints.MapControllerRoute("Default", "controllers/{controller=Home}/{action=Index}/{id?}");




            });
            //app.UseSwagger();
            //app.UseSwaggerUI(options =>
            //{
            //    options.SwaggerEndpoint("/swagger/v1/swagger.json",
            //    "WebApp");
            //});
            SeedData.SeedDatabase(context);
        }
      
    }
}
