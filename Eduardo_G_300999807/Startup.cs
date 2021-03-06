﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eduardo_G_300999807.Models;
using Eduardo_G_300999807.Models.Interfaces;
using Eduardo_G_300999807.Models.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Eduardo_G_300999807
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]));
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=SOCCER_MANAGER;Trusted_Connection=True;MultipleActiveResultSets=true"));
            //services.AddDbContext<AppIdentityDbContext>(options => options.UseSqlServer(Configuration["ConnectionString:IdentityConnection"]));
            services.AddDbContext<AppIdentityDbContext>(options => options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=Identity;Trusted_Connection=True;MultipleActiveResultSets=true"));
            services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AppIdentityDbContext>().AddDefaultTokenProviders();
            services.AddTransient<IClubRepository, EFClubRepository>();
            services.AddTransient<IPlayerRepository, EFPlayerRepository>();
            services.AddTransient<IFixtureRepository, EFFixtureRepository>();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider services)
        {
            app.UseStatusCodePages();
            app.UseAuthentication();
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();            
            SeedData.Populate(app);
            IdentitySeedData.EnsurePopulated(app);
        }
    }
}
