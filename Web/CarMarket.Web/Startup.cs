namespace CarMarket.Web
{
    using System;

    using AutoMapper;

    using CarMarket.Data;
    using CarMarket.Data.Common.Repositories;
    using CarMarket.Data.Models;
    using CarMarket.Data.Repositories;
    using CarMarket.Data.Seeding;
    using CarMarket.Services.Cloudinary;
    using CarMarket.Services.Data;
    using CarMarket.Services.Data.Interfaces;
    using CarMarket.Web.Middlewares;
    using CloudinaryDotNet;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(this.configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>(IdentityOptionsProvider.GetIdentityOptions)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.Configure<CookiePolicyOptions>(
                options =>
                    {
                        options.CheckConsentNeeded = context => true;
                        options.MinimumSameSitePolicy = SameSiteMode.None;
                    });

            services.AddControllersWithViews(
                options =>
                    {
                        options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
                    });

            services.AddAntiforgery(
                options =>
                {
                    options.HeaderName = "X-CSRF-TOKEN";
                });

            services.AddRazorPages();

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = $"/Identity/Account/Login";
                options.LogoutPath = $"/Identity/Account/Logout";
                options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
            });

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddSingleton(this.configuration);

            // Data repository
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));

            // Cloudinary
            services.AddSingleton<Cloudinary>(x => CloudinaryFactory.GetInstance(this.configuration));
            services.AddTransient<ICloudinaryService, CloudinaryService>();

            // Application services
            services.AddTransient<IBodiesService, BodiesService>();
            services.AddTransient<IColorsService, ColorsService>();
            services.AddTransient<IConditionsService, ConditionsService>();
            services.AddTransient<IFuelsService, FuelsService>();
            services.AddTransient<IImagesService, ImagesService>();
            services.AddTransient<IListingsService, ListingsService>();
            services.AddTransient<IMakesService, MakesService>();
            services.AddTransient<IModelsService, ModelsService>();
            services.AddTransient<ITransmissionsService, TransmissionsService>();
            services.AddTransient<ISearchService, SearchService>();
            services.AddTransient<IBookmarksService, BookmarksService>();
            services.AddTransient<IUsersService, UsersService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Seed data on application startup
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                dbContext.Database.Migrate();
                new ApplicationDbContextSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter().GetResult();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            //app.Use(async (context, next) =>
            //{
            //    await next();
            //    if (context.Response.StatusCode == 404)
            //    {
            //        context.Request.Path = "/Home/NotFoundError";
            //        await next();
            //    }
            //});

            app.UseCustomExceptionHandler();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(
                endpoints =>
                    {
                        endpoints.MapControllerRoute("areaRoute", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                        endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
                        endpoints.MapRazorPages();
                    });
        }
    }
}
